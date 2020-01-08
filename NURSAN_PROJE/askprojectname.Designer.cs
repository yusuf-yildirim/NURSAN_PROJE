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
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            this.label1 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.okaskprojectnamebutton = new DevExpress.XtraEditors.SimpleButton();
            this.cancelaskprojectnamebutton = new DevExpress.XtraEditors.SimpleButton();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.label1.Location = new System.Drawing.Point(21, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lütfen Proje İsmini Giriniz";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(272, 67);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(157, 20);
            this.textEdit1.TabIndex = 1;
            this.textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit1_KeyPress);
            // 
            // okaskprojectnamebutton
            // 
            this.okaskprojectnamebutton.Location = new System.Drawing.Point(54, 114);
            this.okaskprojectnamebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okaskprojectnamebutton.Name = "okaskprojectnamebutton";
            this.okaskprojectnamebutton.Size = new System.Drawing.Size(81, 24);
            this.okaskprojectnamebutton.TabIndex = 2;
            this.okaskprojectnamebutton.Text = "OK";
            this.okaskprojectnamebutton.Click += new System.EventHandler(this.okaskprojectnamebutton_Click);
            // 
            // cancelaskprojectnamebutton
            // 
            this.cancelaskprojectnamebutton.Location = new System.Drawing.Point(339, 114);
            this.cancelaskprojectnamebutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelaskprojectnamebutton.Name = "cancelaskprojectnamebutton";
            this.cancelaskprojectnamebutton.Size = new System.Drawing.Size(81, 24);
            this.cancelaskprojectnamebutton.TabIndex = 3;
            this.cancelaskprojectnamebutton.Text = "İptal";
            this.cancelaskprojectnamebutton.Click += new System.EventHandler(this.cancelaskprojectnamebutton_Click);
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "tablo";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery2.Name = "create_project";
            queryParameter2.Name = "project_name";
            queryParameter2.Type = typeof(string);
            queryParameter2.ValueInfo = "null";
            customSqlQuery2.Parameters.Add(queryParameter2);
            customSqlQuery2.Sql = "insert into proje(ad) values(@project_name)";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = "PERhdGFTZXQgTmFtZT0ic3FsRGF0YVNvdXJjZTEiPjxWaWV3IE5hbWU9ImNyZWF0ZV9wcm9qZWN0IiAvP" +
    "jwvRGF0YVNldD4=";
            // 
            // askprojectname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 172);
            this.Controls.Add(this.cancelaskprojectnamebutton);
            this.Controls.Add(this.okaskprojectnamebutton);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "askprojectname";
            this.Text = "Yeni Proje İsmi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.askprojectname_FormClosed);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.askprojectname_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.SimpleButton okaskprojectnamebutton;
        private DevExpress.XtraEditors.SimpleButton cancelaskprojectnamebutton;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    }
}