namespace NURSAN_PROJE
{
    partial class askprojectname
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
            this.label1 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.okaskprojectnamebutton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelaskprojectnamebutton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(25, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(286, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lütfen Proje İsmini Giriniz";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(317, 83);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(183, 22);
            this.textEdit1.TabIndex = 1;
            // 
            // okaskprojectnamebutton
            // 
            this.okaskprojectnamebutton.Location = new System.Drawing.Point(63, 140);
            this.okaskprojectnamebutton.Name = "okaskprojectnamebutton";
            this.okaskprojectnamebutton.Size = new System.Drawing.Size(94, 29);
            this.okaskprojectnamebutton.TabIndex = 2;
            this.okaskprojectnamebutton.Text = "OK";
            this.okaskprojectnamebutton.Click += new System.EventHandler(this.okaskprojectnamebutton_Click);
            // 
            // cancelaskprojectnamebutton
            // 
            this.cancelaskprojectnamebutton.Location = new System.Drawing.Point(395, 140);
            this.cancelaskprojectnamebutton.Name = "cancelaskprojectnamebutton";
            this.cancelaskprojectnamebutton.Size = new System.Drawing.Size(94, 29);
            this.cancelaskprojectnamebutton.TabIndex = 3;
            this.cancelaskprojectnamebutton.Text = "İptal";
            // 
            // askprojectname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 212);
            this.Controls.Add(this.cancelaskprojectnamebutton);
            this.Controls.Add(this.okaskprojectnamebutton);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.label1);
            this.Name = "askprojectname";
            this.Text = "Yeni Proje İsmi";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton okaskprojectnamebutton;
        private DevExpress.XtraEditors.SimpleButton cancelaskprojectnamebutton;
    }
}