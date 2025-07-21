using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
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
    public enum FormMode
    {
        Create,
        RemoveConfig,
        RemoveUserOnly
    }
    public partial class CreateUserForm : DevExpress.XtraEditors.XtraForm
    {
        public string SelectedInstance { get; private set; }
        public string SelectedDatabase { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        private readonly FormMode _currentMode;

        public CreateUserForm(FormMode mode)
        {
            InitializeComponent();
            _currentMode = mode;
        }

        private void CreateUserForm_Load(object sender, EventArgs e)
        {
            switch (_currentMode)
            {
                case FormMode.Create:
                    this.Text = "Yeni Kullanıcı Oluştur ve Yapılandır";
                    btnCreate.Text = "Oluştur ve Yapılandır";
                    layoutControlItemTxtUser.Visibility = LayoutVisibility.Always;
                    layoutControlItemCmbUser.Visibility = LayoutVisibility.Never;
                    layoutControlItemPassword.Visibility = LayoutVisibility.Always;
                    layoutControlItemDatabase.Visibility = LayoutVisibility.Always;
                    break;

                case FormMode.RemoveConfig:
                    this.Text = "Ağ Yapılandırmasını Kaldır";
                    btnCreate.Text = "Ağ Yapılandırmasını Kaldır";
                    layoutControlItemTxtUser.Visibility = LayoutVisibility.Never;
                    layoutControlItemCmbUser.Visibility = LayoutVisibility.Never;
                    layoutControlItemPassword.Visibility = LayoutVisibility.Never;
                    layoutControlItemDatabase.Visibility = LayoutVisibility.Never; 
                    break;

                case FormMode.RemoveUserOnly:
                    this.Text = "Sadece Kullanıcıyı Sil";
                    btnCreate.Text = "Seçili Kullanıcıyı Sil";
                    layoutControlItemTxtUser.Visibility = LayoutVisibility.Never;
                    layoutControlItemCmbUser.Visibility = LayoutVisibility.Always;
                    layoutControlItemPassword.Visibility = LayoutVisibility.Never;
                    layoutControlItemDatabase.Visibility = LayoutVisibility.Always;
                    break;
            }

            try
            {
                var instances = SqlConfigurationHelper.GetSqlInstances();
                cmbInstances.Properties.Items.AddRange(instances);
                if (instances.Count > 0) cmbInstances.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("SQL Server örnekleri alınırken bir hata oluştu: " + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void cmbInstances_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDatabases.Properties.Items.Clear();
            cmbDatabases.EditValue = null;
            cmbUsername.Properties.Items.Clear();
            cmbUsername.EditValue = null;

            string selectedInstance = cmbInstances.SelectedItem.ToString();
            try
            {
                var databases = SqlConfigurationHelper.GetDatabases(selectedInstance);
                cmbDatabases.Properties.Items.AddRange(databases);
                if (databases.Count > 0) cmbDatabases.SelectedIndex = 0;

                if (_currentMode != FormMode.Create)
                {
                    var logins = SqlConfigurationHelper.GetSqlLogins(selectedInstance);
                    cmbUsername.Properties.Items.AddRange(logins);
                    if (logins.Count > 0) cmbUsername.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"'{selectedInstance}' örneğine bağlanırken hata oluştu:\n{ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string usernameValue = "";
            bool skipValidation = false;

            switch (_currentMode)
            {
                case FormMode.Create:
                    usernameValue = txtUsername.Text;
                    if (string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        XtraMessageBox.Show("Lütfen şifre alanını doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
                case FormMode.RemoveUserOnly:
                    if (cmbUsername.SelectedItem != null)
                    {
                        usernameValue = cmbUsername.SelectedItem.ToString();
                    }
                    break;
                case FormMode.RemoveConfig:
                    skipValidation = true;
                    break;
            }

            if (!skipValidation)
            {
                if (cmbInstances.SelectedItem == null || cmbDatabases.SelectedItem == null || string.IsNullOrWhiteSpace(usernameValue))
                {
                    XtraMessageBox.Show("Lütfen tüm gerekli alanları eksiksiz doldurun/seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else 
            {
                if (cmbInstances.SelectedItem == null)
                {
                    XtraMessageBox.Show("Lütfen bir SQL Server Örneği seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            SelectedInstance = cmbInstances.SelectedItem.ToString();
            SelectedDatabase = cmbDatabases.SelectedItem?.ToString() ?? "";
            Username = usernameValue;
            Password = txtPassword.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}