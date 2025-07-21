using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectInstaller
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        private enum AccessType { LanOnly, WanAccess }
        private enum RemovalType { FullConfig, UserOnly }

        public Form1()
        {
            InitializeComponent();
            this.Text = "SQL Server Hızlı Kurulum Aracı";
        }

        private async void btnLanOnly_Click(object sender, EventArgs e)
        {
            await StartConfiguration(AccessType.LanOnly);
        }

        private async void btnWanAccess_Click(object sender, EventArgs e)
        {
            await StartConfiguration(AccessType.WanAccess);
        }

        private void btnRemoveConfig_Click(object sender, EventArgs e)
        {
            var confirmation = XtraMessageBox.Show(
                "Bu işlem, seçilecek kullanıcıyı ve ilgili SQL Server ağ yapılandırmasını (TCP/IP, Güvenlik Duvarı) kalıcı olarak kaldıracaktır.\n\nDevam etmek istediğinizden emin misiniz?",
                "Yapılandırmayı Kaldırma Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                StartRemovalProcess(RemovalType.FullConfig);
            }
        }

        private void btnRemoveUserOnly_Click(object sender, EventArgs e)
        {
            var confirmation = XtraMessageBox.Show(
                "Bu işlem, seçilecek kullanıcıyı ve oturumunu SQL Server'dan kalıcı olarak silecektir. Ağ ayarları etkilenmeyecektir.\n\nDevam etmek istediğinizden emin misiniz?",
                "Kullanıcı Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmation == DialogResult.Yes)
            {
                StartRemovalProcess(RemovalType.UserOnly);
            }
        }

        private async Task StartConfiguration(AccessType accessType)
        {
            memoLog.Text = "";
            Log("Yapılandırma işlemi başlatıldı...");

            using (var userForm = new CreateUserForm(FormMode.Create))
            {
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string publicIp = "N/A";
                    string localIp = "N/A";

                    try
                    {
                        Log("Yerel IP adresi alınıyor...");
                        localIp = SqlConfigurationHelper.GetLocalIpAddress();
                        Log($"Yerel IP Adresiniz Bulundu: {localIp}");

                        if (accessType == AccessType.WanAccess)
                        {
                            Log("Genel (Public) IP adresi alınıyor...");
                            publicIp = await SqlConfigurationHelper.GetPublicIpAddressAsync();
                            Log($"Genel IP Adresiniz Bulundu: {publicIp}");
                        }

                        // Adım 1: Mixed Mode Authentication ayarlanıyor
                        SqlConfigurationHelper.SetMixedModeAuthentication(userForm.SelectedInstance, Log);

                        // Adım 2: Kullanıcı oluşturma
                        SqlConfigurationHelper.CreateSqlUser(userForm.SelectedInstance, userForm.SelectedDatabase, userForm.Username, userForm.Password);
                        Log($"'{userForm.Username}' kullanıcısı başarıyla oluşturuldu.");

                        // Adım 3: TCP/IP açılıyor, port 1433'e sabitleniyor ve servis yeniden başlatılıyor
                        SqlConfigurationHelper.ConfigureTcpIpAndRestartService(userForm.SelectedInstance, Log);

                        // Adım 4: Güvenlik duvarı kuralları ekleniyor
                        Log("Güvenlik duvarı kuralları oluşturuluyor...");
                        SqlConfigurationHelper.OpenFirewallPort("SQL Server (TCP-1433)", 1433, Log);

                        // DÜZELTME: EKSİK OLAN UDP KURALI EKLENDİ!
                        SqlConfigurationHelper.OpenFirewallPort("SQL Server Browser (UDP-1434)", 1434, Log, "UDP");

                        Log("\n>>> BİLGİSAYAR AYARLARI BAŞARIYLA TAMAMLANDI! <<<");

                        // ... (Geri kalan bilgilendirme kısmı aynı kalabilir) ...
                        Log("\n--- Bağlantı Bilgileri ---");
                        Log($"Yerel IP Adresiniz: {localIp}");
                        if (accessType == AccessType.WanAccess) Log($"Genel (Public) IP Adresiniz: {publicIp}");
                        Log($"Veritabanı: {userForm.SelectedDatabase}");
                        Log($"Kullanıcı Adı: {userForm.Username}");
                        Log($"Şifre: {userForm.Password}");
                        Log("------------------------------------");

                        Log("\n--- NASIL BAĞLANILIR? ---");
                        Log("1) YEREL AĞDAN (LAN) BAĞLANTI İÇİN (Aynı Wi-Fi/Kablolu Ağ):");
                        memoLog.AppendText($"   Data Source={localIp},1433;Initial Catalog={userForm.SelectedDatabase};User ID={userForm.Username};Password={userForm.Password};TrustServerCertificate=True;\n");

                        if (accessType == AccessType.WanAccess)
                        {
                            memoLog.AppendText(Environment.NewLine);
                            Log("2) İNTERNET ÜZERİNDEN (WAN) BAĞLANTI İÇİN:");
                            Log("   ADIM 1: Modeminizden 'Port Yönlendirme' (Port Forwarding) yapmalısınız.");
                            Log($"           Kural: Dış TCP Port 1433 -> {localIp} adresine yönlendirilecek.");
                            Log("   ADIM 2: Aşağıdaki bağlantı dizesini kullanın:");
                            memoLog.AppendText($"   Data Source={publicIp},1433;Initial Catalog={userForm.SelectedDatabase};User ID={userForm.Username};Password={userForm.Password};TrustServerCertificate=True;\n\n");
                            Log("   UYARI: İnternet IP'niz zamanla değişebilir. Kalıcı çözüm için 'Dynamic DNS' servislerini araştırın.");
                        }

                        XtraMessageBox.Show("Yapılandırma başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Log($"\n!!! BİR HATA OLUŞTU: {ex.Message}");
                        XtraMessageBox.Show($"Yapılandırma sırasında kritik bir hata oluştu:\n\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }
        private void StartRemovalProcess(RemovalType removalType)
        {
            memoLog.Text = "";
            Log("Kaldırma işlemi başlatıldı...");

            FormMode formMode = removalType == RemovalType.FullConfig ? FormMode.RemoveConfig : FormMode.RemoveUserOnly;

            using (var userForm = new CreateUserForm(formMode))
            {
                if (removalType == RemovalType.FullConfig)
                {
                    userForm.Text = "Ağ Yapılandırması Kaldırılacak Sunucuyu Seçin";
                }

                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        string instance = userForm.SelectedInstance;

                        // GÜNCELLENDİ: "Yapılandırmayı Kaldır" seçildiğinde tüm ayarları geri al
                        if (removalType == RemovalType.FullConfig)
                        {
                            Log("Güvenlik duvarı kuralları kaldırılıyor...");
                            SqlConfigurationHelper.RemoveFirewallRule("SQL Server (TCP-1433)", Log);
                            SqlConfigurationHelper.RemoveFirewallRule("SQL Server Browser (UDP-1434)", Log);

                            Log("SQL Server kimlik doğrulama modu geri alınıyor...");
                            SqlConfigurationHelper.SetWindowsAuthOnlyMode(instance, Log);

                            Log("SQL Server ağ ayarları varsayılana döndürülüyor...");
                            SqlConfigurationHelper.RevertTcpIpAndRestartService(instance, Log);

                            Log("Tüm sunucu yapılandırması başarıyla varsayılan haline getirildi.");
                        }

                        // Sadece kullanıcı silme modundaysa kullanıcıyı sil
                        if (removalType == RemovalType.UserOnly)
                        {
                            string db = userForm.SelectedDatabase;
                            string user = userForm.Username;
                            Log($"'{user}' kullanıcısı ve oturumu siliniyor...");
                            SqlConfigurationHelper.DropSqlUserAndLogin(instance, db, user);
                            Log("Kullanıcı ve oturum başarıyla silindi.");
                        }

                        Log("\n>>> SEÇİLEN İŞLEM BAŞARIYLA TAMAMLANDI! <<<");
                        XtraMessageBox.Show("Seçilen işlem başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        Log($"\n!!! BİR HATA OLUŞTU: {ex.Message}");
                        XtraMessageBox.Show($"Kaldırma sırasında kritik bir hata oluştu:\n\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    Log("Kaldırma işlemi iptal edildi.");
                }
            }
        }
        private void Log(string message)
        {
            memoLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
    }
}
