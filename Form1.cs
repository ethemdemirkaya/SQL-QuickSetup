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

                    try
                    {
                        if (accessType == AccessType.WanAccess)
                        {
                            Log("Genel (Public) IP adresi alınıyor...");
                            publicIp = await SqlConfigurationHelper.GetPublicIpAddressAsync();
                            Log($"Genel IP Adresiniz Bulundu: {publicIp}");
                        }

                        SqlConfigurationHelper.CreateSqlUser(userForm.SelectedInstance, userForm.SelectedDatabase, userForm.Username, userForm.Password);
                        Log($"'{userForm.Username}' kullanıcısı başarıyla oluşturuldu.");

                        SqlConfigurationHelper.EnableTcpIpAndRestartService(userForm.SelectedInstance, Log);

                        if (accessType == AccessType.WanAccess)
                        {
                            SqlConfigurationHelper.OpenFirewallPort("SQL Server (TCP-1433)", 1433, Log);
                        }

                        Log("\n>>> BİLGİSAYAR AYARLARI BAŞARIYLA TAMAMLANDI! <<<");

                        Log("\n--- Bağlantı Bilgileri ---");
                        Log($"Sunucu Adı (Data Source): {userForm.SelectedInstance}");
                        Log($"Veritabanı (Initial Catalog): {userForm.SelectedDatabase}");
                        Log($"Kullanıcı Adı (User ID): {userForm.Username}");
                        Log($"Şifre (Password): {userForm.Password}");
                        Log("------------------------------------");

                        Log("\n--- NASIL BAĞLANILIR? ---");
                        Log("1) YEREL AĞDAN (LAN) BAĞLANTI İÇİN KULLANILACAK BAĞLANTI DİZESİ:");
                        memoLog.AppendText($"   Data Source={userForm.SelectedInstance};Initial Catalog={userForm.SelectedDatabase};User ID={userForm.Username};Password={userForm.Password};\n");

                        if (accessType == AccessType.WanAccess)
                        {
                            memoLog.AppendText(Environment.NewLine);
                            Log("2) İNTERNET ÜZERİNDEN (WAN) BAĞLANTI İÇİN YAPILMASI GEREKENLER:");
                            Log("   ADIM 1: Modem/Router arayüzünüze girip 'Port Yönlendirme' (Port Forwarding) yapmalısınız.");
                            Log($"           Kural: Dış TCP Port 1433 -> Bu bilgisayarın yerel IP'sine yönlendirilecek.");
                            Log("   ADIM 2: Aşağıdaki bağlantı dizesini kullanın. (Public IP adresiniz otomatik olarak eklendi)");
                            memoLog.AppendText($"   Data Source={publicIp},1433;Initial Catalog={userForm.SelectedDatabase};User ID={userForm.Username};Password={userForm.Password};\n\n");
                            Log("   ÖNEMLİ UYARI: İnternet IP adresiniz zamanla değişebilir (Dinamik IP). Bağlantınız koparsa, bu programı tekrar çalıştırıp yeni IP'nizi öğrenebilirsiniz.");
                            Log("                Kalıcı bir çözüm için 'Dynamic DNS' (DDNS) servislerini (örn: No-IP, Dynu) araştırmanız önerilir.");
                        }

                        XtraMessageBox.Show("Yapılandırma başarıyla tamamlandı!\nBağlantı bilgileri ve nasıl bağlanılacağı işlem günlüğüne yazıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                else
                {
                    Log("Yapılandırma işlemi iptal edildi.");
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
                        string db = userForm.SelectedDatabase;
                        string user = userForm.Username;

                        if (removalType == RemovalType.FullConfig)
                        {
                            Log("Güvenlik duvarı kuralı kaldırılıyor...");
                            SqlConfigurationHelper.RemoveFirewallRule("SQL Server (TCP-1433)", Log);

                            Log("SQL Server ağ ayarları geri alınıyor...");
                            SqlConfigurationHelper.DisableTcpIpAndRestartService(instance, Log);
                            Log("Ağ yapılandırması başarıyla kaldırıldı.");
                        }

                        if (removalType == RemovalType.UserOnly)
                        {
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
