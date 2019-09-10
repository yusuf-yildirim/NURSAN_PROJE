namespace NURSAN_PROJE
{
    partial class newuserform
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
            this.newusercancelbutton = new DevExpress.XtraEditors.SimpleButton();
            this.newuserokbutton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // newusercancelbutton
            // 
            this.newusercancelbutton.Location = new System.Drawing.Point(388, 96);
            this.newusercancelbutton.Name = "newusercancelbutton";
            this.newusercancelbutton.Size = new System.Drawing.Size(94, 29);
            this.newusercancelbutton.TabIndex = 0;
            this.newusercancelbutton.Text = "İptal";
            // 
            // newuserokbutton
            // 
            this.newuserokbutton.Location = new System.Drawing.Point(59, 96);
            this.newuserokbutton.Name = "newuserokbutton";
            this.newuserokbutton.Size = new System.Drawing.Size(94, 29);
            this.newuserokbutton.TabIndex = 1;
            this.newuserokbutton.Text = "OK";
            this.newuserokbutton.Click += new System.EventHandler(this.newuserokbutton_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(59, 41);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(219, 28);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Kullanıcı İsmini Giriniz";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(312, 48);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(125, 22);
            this.textEdit1.TabIndex = 3;
            // 
            // newuserform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 165);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.newuserokbutton);
            this.Controls.Add(this.newusercancelbutton);
            this.Name = "newuserform";
            this.Text = "Yeni Kullanıcı";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton newusercancelbutton;
        private DevExpress.XtraEditors.SimpleButton newuserokbutton;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
    }
}