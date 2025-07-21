namespace ProjectInstaller
{
    partial class CreateUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.cmbInstances = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.cmbDatabases = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemDatabase = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemTxtUser = new DevExpress.XtraLayout.LayoutControlItem();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItemPassword = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.cmbUsername = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlItemCmbUser = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInstances.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDatabases.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDatabase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTxtUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCmbUser)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cmbUsername);
            this.layoutControl1.Controls.Add(this.btnCreate);
            this.layoutControl1.Controls.Add(this.txtPassword);
            this.layoutControl1.Controls.Add(this.txtUsername);
            this.layoutControl1.Controls.Add(this.cmbDatabases);
            this.layoutControl1.Controls.Add(this.cmbInstances);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(777, 367);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.layoutControlItemDatabase,
            this.layoutControlItemTxtUser,
            this.layoutControlItemPassword,
            this.layoutControlItem5,
            this.layoutControlItemCmbUser});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(777, 367);
            this.Root.TextVisible = false;
            // 
            // cmbInstances
            // 
            this.cmbInstances.Location = new System.Drawing.Point(220, 13);
            this.cmbInstances.Name = "cmbInstances";
            this.cmbInstances.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbInstances.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbInstances.Size = new System.Drawing.Size(544, 32);
            this.cmbInstances.StyleController = this.layoutControl1;
            this.cmbInstances.TabIndex = 4;
            this.cmbInstances.SelectedIndexChanged += new System.EventHandler(this.cmbInstances_SelectedIndexChanged);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.cmbInstances;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(755, 36);
            this.layoutControlItem1.Text = "SQL Server Örneği:";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(194, 25);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 180);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(755, 131);
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.Location = new System.Drawing.Point(220, 49);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDatabases.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbDatabases.Size = new System.Drawing.Size(544, 32);
            this.cmbDatabases.StyleController = this.layoutControl1;
            this.cmbDatabases.TabIndex = 5;
            // 
            // layoutControlItemDatabase
            // 
            this.layoutControlItemDatabase.Control = this.cmbDatabases;
            this.layoutControlItemDatabase.Location = new System.Drawing.Point(0, 36);
            this.layoutControlItemDatabase.Name = "layoutControlItemDatabase";
            this.layoutControlItemDatabase.Size = new System.Drawing.Size(755, 36);
            this.layoutControlItemDatabase.Text = "Veritabanı:";
            this.layoutControlItemDatabase.TextSize = new System.Drawing.Size(194, 25);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(220, 121);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(544, 32);
            this.txtUsername.StyleController = this.layoutControl1;
            this.txtUsername.TabIndex = 6;
            // 
            // layoutControlItemTxtUser
            // 
            this.layoutControlItemTxtUser.Control = this.txtUsername;
            this.layoutControlItemTxtUser.Location = new System.Drawing.Point(0, 108);
            this.layoutControlItemTxtUser.Name = "layoutControlItemTxtUser";
            this.layoutControlItemTxtUser.Size = new System.Drawing.Size(755, 36);
            this.layoutControlItemTxtUser.Text = "Yeni Kullanıcı Adı:";
            this.layoutControlItemTxtUser.TextSize = new System.Drawing.Size(194, 25);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(220, 157);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.UseSystemPasswordChar = true;
            this.txtPassword.Size = new System.Drawing.Size(544, 32);
            this.txtPassword.StyleController = this.layoutControl1;
            this.txtPassword.TabIndex = 7;
            // 
            // layoutControlItemPassword
            // 
            this.layoutControlItemPassword.Control = this.txtPassword;
            this.layoutControlItemPassword.Location = new System.Drawing.Point(0, 144);
            this.layoutControlItemPassword.Name = "layoutControlItemPassword";
            this.layoutControlItemPassword.Size = new System.Drawing.Size(755, 36);
            this.layoutControlItemPassword.Text = "Şifre:";
            this.layoutControlItemPassword.TextSize = new System.Drawing.Size(194, 25);
            // 
            // btnCreate
            // 
            this.btnCreate.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnCreate.Appearance.Options.UseBackColor = true;
            this.btnCreate.Location = new System.Drawing.Point(13, 324);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(751, 30);
            this.btnCreate.StyleController = this.layoutControl1;
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "Oluştur ve Yapılandır";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCreate;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 311);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(755, 34);
            this.layoutControlItem5.TextVisible = false;
            // 
            // cmbUsername
            // 
            this.cmbUsername.Location = new System.Drawing.Point(220, 85);
            this.cmbUsername.Name = "cmbUsername";
            this.cmbUsername.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUsername.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbUsername.Size = new System.Drawing.Size(544, 32);
            this.cmbUsername.StyleController = this.layoutControl1;
            this.cmbUsername.TabIndex = 9;
            // 
            // layoutControlItemCmbUser
            // 
            this.layoutControlItemCmbUser.Control = this.cmbUsername;
            this.layoutControlItemCmbUser.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItemCmbUser.Name = "layoutControlItemCmbUser";
            this.layoutControlItemCmbUser.Size = new System.Drawing.Size(755, 36);
            this.layoutControlItemCmbUser.Text = "Silmek İstenilen Kullanıcı:";
            this.layoutControlItemCmbUser.TextSize = new System.Drawing.Size(194, 25);
            // 
            // CreateUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 367);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CreateUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayar Yapılandırması";
            this.Load += new System.EventHandler(this.CreateUserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInstances.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDatabases.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemDatabase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemTxtUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemPassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItemCmbUser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.ComboBoxEdit cmbDatabases;
        private DevExpress.XtraEditors.ComboBoxEdit cmbInstances;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemDatabase;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemTxtUser;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemPassword;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.ComboBoxEdit cmbUsername;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItemCmbUser;
    }
}