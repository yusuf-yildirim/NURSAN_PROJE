namespace NURSAN_PROJE
{
    partial class ResistorEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.updateresistorvalue = new DevExpress.XtraEditors.TextEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.updateresistortolerence = new DevExpress.XtraEditors.ComboBoxEdit();
            this.updateresistorname = new DevExpress.XtraEditors.TextEdit();
            this.add_res_tol_lab = new DevExpress.XtraEditors.LabelControl();
            this.updateresistormultiplier = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistorvalue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistortolerence.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistorname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistormultiplier.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.updateresistorvalue);
            this.groupControl1.Controls.Add(this.labelControl26);
            this.groupControl1.Controls.Add(this.updateresistortolerence);
            this.groupControl1.Controls.Add(this.updateresistorname);
            this.groupControl1.Controls.Add(this.add_res_tol_lab);
            this.groupControl1.Controls.Add(this.updateresistormultiplier);
            this.groupControl1.Controls.Add(this.labelControl29);
            this.groupControl1.Location = new System.Drawing.Point(360, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(245, 147);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // updateresistorvalue
            // 
            this.updateresistorvalue.Location = new System.Drawing.Point(104, 76);
            this.updateresistorvalue.Name = "updateresistorvalue";
            this.updateresistorvalue.Size = new System.Drawing.Size(57, 20);
            this.updateresistorvalue.TabIndex = 31;
            // 
            // labelControl26
            // 
            this.labelControl26.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl26.Appearance.Options.UseFont = true;
            this.labelControl26.Location = new System.Drawing.Point(32, 41);
            this.labelControl26.Name = "labelControl26";
            this.labelControl26.Size = new System.Drawing.Size(31, 19);
            this.labelControl26.TabIndex = 25;
            this.labelControl26.Text = "İsim";
            // 
            // updateresistortolerence
            // 
            this.updateresistortolerence.Location = new System.Drawing.Point(104, 112);
            this.updateresistortolerence.Name = "updateresistortolerence";
            this.updateresistortolerence.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.updateresistortolerence.Properties.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60",
            "65",
            "70",
            "75",
            "80",
            "85",
            "90",
            "95",
            "100"});
            this.updateresistortolerence.Size = new System.Drawing.Size(100, 20);
            this.updateresistortolerence.TabIndex = 30;
            // 
            // updateresistorname
            // 
            this.updateresistorname.Location = new System.Drawing.Point(104, 42);
            this.updateresistorname.Name = "updateresistorname";
            this.updateresistorname.Size = new System.Drawing.Size(100, 20);
            this.updateresistorname.TabIndex = 26;
            // 
            // add_res_tol_lab
            // 
            this.add_res_tol_lab.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.add_res_tol_lab.Appearance.Options.UseFont = true;
            this.add_res_tol_lab.Location = new System.Drawing.Point(32, 110);
            this.add_res_tol_lab.Name = "add_res_tol_lab";
            this.add_res_tol_lab.Size = new System.Drawing.Size(65, 19);
            this.add_res_tol_lab.TabIndex = 29;
            this.add_res_tol_lab.Text = "Toleransı";
            // 
            // updateresistormultiplier
            // 
            this.updateresistormultiplier.Location = new System.Drawing.Point(167, 76);
            this.updateresistormultiplier.Name = "updateresistormultiplier";
            this.updateresistormultiplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.updateresistormultiplier.Properties.Items.AddRange(new object[] {
            "MΩ",
            "KΩ",
            "Ω",
            "mΩ"});
            this.updateresistormultiplier.Size = new System.Drawing.Size(36, 20);
            this.updateresistormultiplier.TabIndex = 28;
            // 
            // labelControl29
            // 
            this.labelControl29.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl29.Appearance.Options.UseFont = true;
            this.labelControl29.Location = new System.Drawing.Point(32, 74);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(46, 19);
            this.labelControl29.TabIndex = 27;
            this.labelControl29.Text = "Değeri";
            // 
            // ResistorEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "ResistorEdit";
            this.Size = new System.Drawing.Size(626, 155);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistorvalue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistortolerence.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistorname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateresistormultiplier.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit updateresistorvalue;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.ComboBoxEdit updateresistortolerence;
        private DevExpress.XtraEditors.TextEdit updateresistorname;
        private DevExpress.XtraEditors.LabelControl add_res_tol_lab;
        private DevExpress.XtraEditors.ComboBoxEdit updateresistormultiplier;
        private DevExpress.XtraEditors.LabelControl labelControl29;
    }
}
