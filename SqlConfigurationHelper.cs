using Microsoft.Win32;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Net.Http;

namespace ProjectInstaller
{
    public static class SqlConfigurationHelper
    {
        /// <summary>
        /// Sistemde kurulu olan SQL Server örneklerini (instances) bulur.
        /// </summary>
        public static List<string> GetSqlInstances()
        {
            var instances = new List<string>();
            var registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                var instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        string fullInstanceName = Environment.MachineName;
                        if (instanceName.ToUpper() != "MSSQLSERVER")
                        {
                            fullInstanceName += @"\" + instanceName;
                        }
                        instances.Add(fullInstanceName);
                    }
                }
            }
            return instances;
        }
        /// <summary>
        /// Belirtilen SQL örneğindeki kullanıcı tarafından oluşturulmuş SQL oturumlarını listeler.
        /// Sistem hesapları filtrelenir.
        /// </summary>
        public static List<string> GetSqlLogins(string instanceName)
        {
            var logins = new List<string>();
            string connStr = $"Data Source={instanceName};Initial Catalog=master;Integrated Security=True;Connect Timeout=5";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT name FROM sys.sql_logins WHERE type = 'S' AND is_disabled = 0 AND name NOT LIKE '##%' AND name <> 'sa'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logins.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            return logins;
        }
        /// <summary>
        /// Belirtilen SQL örneğindeki kullanıcı veritabanlarını listeler.
        /// </summary>
        public static List<string> GetDatabases(string instanceName)
        {
            var databases = new List<string>();
            string connStr = $"Data Source={instanceName};Initial Catalog=master;Integrated Security=True;Connect Timeout=5";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT name FROM sys.databases WHERE database_id > 4", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            databases.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            return databases;
        }

        /// <summary>
        /// SQL Server üzerinde yeni bir Login ve veritabanında yeni bir User oluşturur.
        /// </summary>
        public static void CreateSqlUser(string instanceName, string dbName, string username, string password)
        {
            string connStr = $"Data Source={instanceName};Initial Catalog=master;Integrated Security=True;";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                string createLoginQuery = $"IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = '{username}') BEGIN CREATE LOGIN [{username}] WITH PASSWORD = N'{password}', DEFAULT_DATABASE=[{dbName}], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF END";
                using (var cmd = new SqlCommand(createLoginQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.ChangeDatabase(dbName);
                string createUserQuery = $"IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = '{username}') BEGIN CREATE USER [{username}] FOR LOGIN [{username}] END";
                using (var cmd = new SqlCommand(createUserQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                string addRoleQuery = $"ALTER ROLE db_owner ADD MEMBER [{username}]";
                using (var cmd = new SqlCommand(addRoleQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// SQL Server TCP/IP protokolünü aktifleştirir ve ilgili servisi yeniden başlatır.
        /// </summary>
        public static void EnableTcpIpAndRestartService(string instanceName, Action<string> logAction)
        {
            string instanceId = GetInstanceIdFromInstanceName(instanceName);
            if (string.IsNullOrEmpty(instanceId))
            {
                throw new Exception($"'{instanceName}' için örnek ID'si (Instance ID) bulunamadı. Registry kontrolü başarısız.");
            }

            string tcpKeyPath = $@"SOFTWARE\Microsoft\Microsoft SQL Server\{instanceId}\MSSQLServer\SuperSocketNetLib\Tcp";
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(tcpKeyPath, true))
            {
                if (key == null) throw new Exception("TCP/IP için Registry anahtarı bulunamadı.");
                key.SetValue("Enabled", 1, RegistryValueKind.DWord);
                logAction("SQL Server TCP/IP protokolü aktifleştirildi.");
            }

            string serviceName = GetSqlServiceName(instanceName);
            logAction($"'{serviceName}' servisi yeniden başlatılıyor...");
            ServiceController service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
            }
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            logAction($"'{serviceName}' servisi başarıyla yeniden başlatıldı.");
        }

        /// <summary>
        /// Windows Güvenlik Duvarı'nda belirtilen port için gelen kuralı oluşturur.
        /// </summary>
        public static void OpenFirewallPort(string ruleName, int port, Action<string> logAction)
        {
            Type fwPolicy2Type = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(fwPolicy2Type);

            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name == ruleName)
                {
                    logAction($"'{ruleName}' isminde bir güvenlik duvarı kuralı zaten mevcut.");
                    return;
                }
            }

            Type fwRuleType = Type.GetTypeFromProgID("HNetCfg.FWRule");
            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(fwRuleType);

            newRule.Name = ruleName;
            newRule.Description = "SQL Server için TCP port erişimi (Otomatik oluşturuldu).";
            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            newRule.LocalPorts = port.ToString();
            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            newRule.Enabled = true;
            newRule.Grouping = "SQL Server Otomasyon";
            newRule.Profiles = fwPolicy2.CurrentProfileTypes;

            fwPolicy2.Rules.Add(newRule);
            logAction($"'{ruleName}' güvenlik duvarı kuralı (Port: {port}) başarıyla oluşturuldu.");
        }

        private static string GetInstanceIdFromInstanceName(string instanceName)
        {
            string instanceOnlyName = instanceName.Contains("\\") ? instanceName.Split('\\')[1] : "MSSQLSERVER";
            string keyPath = @"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL";
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(keyPath))
            {
                return key?.GetValue(instanceOnlyName)?.ToString();
            }
        }

        private static string GetSqlServiceName(string instanceName)
        {
            if (!instanceName.Contains("\\"))
            {
                return "MSSQLSERVER";
            }
            return "MSSQL$" + instanceName.Split('\\')[1];
        }
        /// <summary>
        /// Windows Güvenlik Duvarı'ndaki belirtilen kuralı kaldırır.
        /// </summary>
        public static void RemoveFirewallRule(string ruleName, Action<string> logAction)
        {
            try
            {
                Type fwPolicy2Type = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
                INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(fwPolicy2Type);

                bool ruleFound = false;
                foreach (INetFwRule rule in fwPolicy2.Rules)
                {
                    if (rule.Name == ruleName)
                    {
                        fwPolicy2.Rules.Remove(ruleName);
                        logAction($"'{ruleName}' ismindeki güvenlik duvarı kuralı başarıyla kaldırıldı.");
                        ruleFound = true;
                        break;
                    }
                }

                if (!ruleFound)
                {
                    logAction($"'{ruleName}' isminde bir güvenlik duvarı kuralı bulunamadı, silme işlemi atlandı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güvenlik duvarı kuralı kaldırılırken hata: " + ex.Message);
            }
        }

        /// <summary>
        /// SQL Server TCP/IP protokolünü devre dışı bırakır ve servisi yeniden başlatır.
        /// </summary>
        public static void DisableTcpIpAndRestartService(string instanceName, Action<string> logAction)
        {
            string instanceId = GetInstanceIdFromInstanceName(instanceName);
            if (string.IsNullOrEmpty(instanceId))
            {
                throw new Exception($"'{instanceName}' için örnek ID'si (Instance ID) bulunamadı.");
            }

            string tcpKeyPath = $@"SOFTWARE\Microsoft\Microsoft SQL Server\{instanceId}\MSSQLServer\SuperSocketNetLib\Tcp";
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (var key = hklm.OpenSubKey(tcpKeyPath, true))
            {
                if (key == null) throw new Exception("TCP/IP için Registry anahtarı bulunamadı.");
                key.SetValue("Enabled", 0, RegistryValueKind.DWord);
                logAction("SQL Server TCP/IP protokolü devre dışı bırakıldı.");
            }

            string serviceName = GetSqlServiceName(instanceName);
            logAction($"'{serviceName}' servisi yeniden başlatılıyor...");
            ServiceController service = new ServiceController(serviceName);
            if (service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
            }
            service.Start();
            service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
            logAction($"'{serviceName}' servisi başarıyla yeniden başlatıldı.");
        }
        /// <summary>
        /// Programın çalıştığı ağın Public IP adresini asenkron olarak alır.
        /// </summary>
        /// <returns>Public IP adresini string olarak döndürür.</returns>
        public static async Task<string> GetPublicIpAddressAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    return await client.GetStringAsync("https://api.ipify.org");
                }
            }
            catch (Exception)
            {
                return "IP Adresi Otomatik Bulunamadı";
            }
        }
        /// <summary>
        /// Belirtilen SQL kullanıcısını (User) ve oturumunu (Login) siler.
        /// </summary>
        public static void DropSqlUserAndLogin(string instanceName, string dbName, string username)
        {
            string connStr = $"Data Source={instanceName};Initial Catalog=master;Integrated Security=True;";
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();

                conn.ChangeDatabase(dbName);
                string dropUserQuery = $"IF EXISTS (SELECT name FROM sys.database_principals WHERE name = '{username}') BEGIN DROP USER [{username}] END";
                using (var cmd = new SqlCommand(dropUserQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.ChangeDatabase("master");
                string dropLoginQuery = $"IF EXISTS (SELECT name FROM sys.sql_logins WHERE name = '{username}') BEGIN DROP LOGIN [{username}] END";
                using (var cmd = new SqlCommand(dropLoginQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
