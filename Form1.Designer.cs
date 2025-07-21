namespace ProjectInstaller
{
    partial class Form1
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.memoLog = new DevExpress.XtraEditors.MemoEdit();
            this.btnLanOnly = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnWanAccess = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnRemoveConfig = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnRemoveUserOnly = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnRemoveUserOnly);
            this.layoutControl1.Controls.Add(this.btnRemoveConfig);
            this.layoutControl1.Controls.Add(this.guna2Panel1);
            this.layoutControl1.Controls.Add(this.btnWanAccess);
            this.layoutControl1.Controls.Add(this.btnLanOnly);
            this.layoutControl1.Controls.Add(this.groupControl1);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(690, 423);
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
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(690, 423);
            this.Root.TextVisible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(413, 25);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Lütfen yapmak istediğiniz yapılandırma türünü seçin:";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.labelControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(668, 29);
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 151);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(668, 34);
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Options.UseTextOptions = true;
            this.groupControl1.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.groupControl1.Controls.Add(this.memoLog);
            this.groupControl1.Location = new System.Drawing.Point(13, 198);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(664, 212);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "İşlem Günlüğü";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.groupControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 185);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(668, 216);
            this.layoutControlItem2.TextVisible = false;
            // 
            // memoLog
            // 
            this.memoLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoLog.Location = new System.Drawing.Point(2, 30);
            this.memoLog.Name = "memoLog";
            this.memoLog.Properties.ReadOnly = true;
            this.memoLog.Size = new System.Drawing.Size(660, 180);
            this.memoLog.StyleController = this.layoutControl1;
            this.memoLog.TabIndex = 6;
            // 
            // btnLanOnly
            // 
            this.btnLanOnly.AllowFocus = false;
            this.btnLanOnly.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnLanOnly.Appearance.Options.UseBackColor = true;
            this.btnLanOnly.Location = new System.Drawing.Point(13, 84);
            this.btnLanOnly.Name = "btnLanOnly";
            this.btnLanOnly.Size = new System.Drawing.Size(329, 38);
            this.btnLanOnly.StyleController = this.layoutControl1;
            this.btnLanOnly.TabIndex = 6;
            this.btnLanOnly.Text = "Sadece Yerel Ağ (LAN) Erişimi Ayarla";
            this.btnLanOnly.Click += new System.EventHandler(this.btnLanOnly_Click);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnLanOnly;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 71);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(296, 34);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(333, 42);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextVisible = false;
            // 
            // btnWanAccess
            // 
            this.btnWanAccess.AllowFocus = false;
            this.btnWanAccess.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnWanAccess.Appearance.Options.UseBackColor = true;
            this.btnWanAccess.Location = new System.Drawing.Point(346, 84);
            this.btnWanAccess.Name = "btnWanAccess";
            this.btnWanAccess.Size = new System.Drawing.Size(331, 38);
            this.btnWanAccess.StyleController = this.layoutControl1;
            this.btnWanAccess.TabIndex = 7;
            this.btnWanAccess.Text = "İnternet ve LAN (WAN) Erişimi Ayarla";
            this.btnWanAccess.Click += new System.EventHandler(this.btnWanAccess_Click);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnWanAccess;
            this.layoutControlItem4.Location = new System.Drawing.Point(333, 71);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(300, 34);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(335, 42);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextVisible = false;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Location = new System.Drawing.Point(13, 42);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(664, 38);
            this.guna2Panel1.TabIndex = 8;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.guna2Panel1;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(668, 42);
            this.layoutControlItem5.TextVisible = false;
            // 
            // btnRemoveConfig
            // 
            this.btnRemoveConfig.AllowFocus = false;
            this.btnRemoveConfig.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnRemoveConfig.Appearance.Options.UseBackColor = true;
            this.btnRemoveConfig.Location = new System.Drawing.Point(13, 126);
            this.btnRemoveConfig.Name = "btnRemoveConfig";
            this.btnRemoveConfig.Size = new System.Drawing.Size(329, 34);
            this.btnRemoveConfig.StyleController = this.layoutControl1;
            this.btnRemoveConfig.TabIndex = 9;
            this.btnRemoveConfig.Text = "Yapılandırmayı Geri Al / Kaldır";
            this.btnRemoveConfig.Click += new System.EventHandler(this.btnRemoveConfig_Click);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnRemoveConfig;
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 113);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(245, 34);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(333, 38);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextVisible = false;
            // 
            // btnRemoveUserOnly
            // 
            this.btnRemoveUserOnly.AllowFocus = false;
            this.btnRemoveUserOnly.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.btnRemoveUserOnly.Appearance.Options.UseBackColor = true;
            this.btnRemoveUserOnly.Location = new System.Drawing.Point(346, 126);
            this.btnRemoveUserOnly.Name = "btnRemoveUserOnly";
            this.btnRemoveUserOnly.Size = new System.Drawing.Size(331, 34);
            this.btnRemoveUserOnly.StyleController = this.layoutControl1;
            this.btnRemoveUserOnly.TabIndex = 10;
            this.btnRemoveUserOnly.Text = "Sadece Kullanıcıyı Sil";
            this.btnRemoveUserOnly.Click += new System.EventHandler(this.btnRemoveUserOnly_Click);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnRemoveUserOnly;
            this.layoutControlItem7.Location = new System.Drawing.Point(333, 113);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(126, 34);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(335, 38);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextVisible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 423);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Server Hızlı Kurulum Aracı";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.layoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.MemoEdit memoLog;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private DevExpress.XtraEditors.SimpleButton btnWanAccess;
        private DevExpress.XtraEditors.SimpleButton btnLanOnly;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.SimpleButton btnRemoveConfig;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.SimpleButton btnRemoveUserOnly;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}

