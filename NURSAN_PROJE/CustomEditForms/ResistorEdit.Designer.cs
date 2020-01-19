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
            this.add_resistor_value = new DevExpress.XtraEditors.TextEdit();
            this.labelControl26 = new DevExpress.XtraEditors.LabelControl();
            this.add_resistor_tolerance = new DevExpress.XtraEditors.ComboBoxEdit();
            this.add_resistor_name = new DevExpress.XtraEditors.TextEdit();
            this.add_res_tol_lab = new DevExpress.XtraEditors.LabelControl();
            this.add_resistor_multiplier = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_value.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_tolerance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_name.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_multiplier.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.add_resistor_value);
            this.groupControl1.Controls.Add(this.labelControl26);
            this.groupControl1.Controls.Add(this.add_resistor_tolerance);
            this.groupControl1.Controls.Add(this.add_resistor_name);
            this.groupControl1.Controls.Add(this.add_res_tol_lab);
            this.groupControl1.Controls.Add(this.add_resistor_multiplier);
            this.groupControl1.Controls.Add(this.labelControl29);
            this.groupControl1.Location = new System.Drawing.Point(360, 3);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(245, 147);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "groupControl1";
            // 
            // add_resistor_value
            // 
            this.add_resistor_value.Location = new System.Drawing.Point(104, 76);
            this.add_resistor_value.Name = "add_resistor_value";
            this.add_resistor_value.Size = new System.Drawing.Size(57, 20);
            this.add_resistor_value.TabIndex = 31;
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
            // add_resistor_tolerance
            // 
            this.add_resistor_tolerance.Location = new System.Drawing.Point(104, 112);
            this.add_resistor_tolerance.Name = "add_resistor_tolerance";
            this.add_resistor_tolerance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.add_resistor_tolerance.Properties.Items.AddRange(new object[] {
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
            this.add_resistor_tolerance.Size = new System.Drawing.Size(100, 20);
            this.add_resistor_tolerance.TabIndex = 30;
            // 
            // add_resistor_name
            // 
            this.add_resistor_name.Location = new System.Drawing.Point(104, 42);
            this.add_resistor_name.Name = "add_resistor_name";
            this.add_resistor_name.Size = new System.Drawing.Size(100, 20);
            this.add_resistor_name.TabIndex = 26;
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
            // add_resistor_multiplier
            // 
            this.add_resistor_multiplier.Location = new System.Drawing.Point(167, 76);
            this.add_resistor_multiplier.Name = "add_resistor_multiplier";
            this.add_resistor_multiplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.add_resistor_multiplier.Properties.Items.AddRange(new object[] {
            "MΩ",
            "KΩ",
            "Ω",
            "mΩ"});
            this.add_resistor_multiplier.Size = new System.Drawing.Size(36, 20);
            this.add_resistor_multiplier.TabIndex = 28;
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
            // XtraUserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "XtraUserControl1";
            this.Size = new System.Drawing.Size(626, 155);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_value.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_tolerance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_name.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.add_resistor_multiplier.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit add_resistor_value;
        private DevExpress.XtraEditors.LabelControl labelControl26;
        private DevExpress.XtraEditors.ComboBoxEdit add_resistor_tolerance;
        private DevExpress.XtraEditors.TextEdit add_resistor_name;
        private DevExpress.XtraEditors.LabelControl add_res_tol_lab;
        private DevExpress.XtraEditors.ComboBoxEdit add_resistor_multiplier;
        private DevExpress.XtraEditors.LabelControl labelControl29;
    }
}
