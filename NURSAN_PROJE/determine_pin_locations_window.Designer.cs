namespace NURSAN_PROJE
{
    partial class determine_pin_locations_window
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.determine_pin_locations_image = new DevExpress.XtraEditors.PictureEdit();
            this.determine_pin_locations_undo = new DevExpress.XtraEditors.SimpleButton();
            this.Determine_pin_locations_SavePins = new DevExpress.XtraEditors.SimpleButton();
            this.determine_pin_locations_selectimage = new DevExpress.XtraEditors.SimpleButton();
            this.determine_pin_locations_resetallpins = new DevExpress.XtraEditors.SimpleButton();
            this.determine_pin_locations_determinedpins = new DevExpress.XtraEditors.ListBoxControl();
            this.xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(this.components);
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::NURSAN_PROJE.WaitForm2), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.determine_pin_locations_image.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.determine_pin_locations_determinedpins)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.determine_pin_locations_image);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.determine_pin_locations_undo);
            this.splitContainerControl1.Panel2.Controls.Add(this.Determine_pin_locations_SavePins);
            this.splitContainerControl1.Panel2.Controls.Add(this.determine_pin_locations_selectimage);
            this.splitContainerControl1.Panel2.Controls.Add(this.determine_pin_locations_resetallpins);
            this.splitContainerControl1.Panel2.Controls.Add(this.determine_pin_locations_determinedpins);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(865, 557);
            this.splitContainerControl1.SplitterPosition = 613;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // determine_pin_locations_image
            // 
            this.determine_pin_locations_image.Dock = System.Windows.Forms.DockStyle.Fill;
            this.determine_pin_locations_image.Location = new System.Drawing.Point(0, 0);
            this.determine_pin_locations_image.Name = "determine_pin_locations_image";
            this.determine_pin_locations_image.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.determine_pin_locations_image.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.determine_pin_locations_image.Size = new System.Drawing.Size(613, 557);
            this.determine_pin_locations_image.TabIndex = 0;
            this.determine_pin_locations_image.MouseClick += new System.Windows.Forms.MouseEventHandler(this.determine_pin_locations_image_MouseClick);
            // 
            // determine_pin_locations_undo
            // 
            this.determine_pin_locations_undo.Location = new System.Drawing.Point(8, 103);
            this.determine_pin_locations_undo.Name = "determine_pin_locations_undo";
            this.determine_pin_locations_undo.Size = new System.Drawing.Size(75, 23);
            this.determine_pin_locations_undo.TabIndex = 4;
            this.determine_pin_locations_undo.Text = "Geri al";
            this.determine_pin_locations_undo.Click += new System.EventHandler(this.determine_pin_locations_undo_Click);
            // 
            // Determine_pin_locations_SavePins
            // 
            this.Determine_pin_locations_SavePins.Location = new System.Drawing.Point(164, 132);
            this.Determine_pin_locations_SavePins.Name = "Determine_pin_locations_SavePins";
            this.Determine_pin_locations_SavePins.Size = new System.Drawing.Size(75, 23);
            this.Determine_pin_locations_SavePins.TabIndex = 3;
            this.Determine_pin_locations_SavePins.Text = "Kaydet";
            this.Determine_pin_locations_SavePins.Click += new System.EventHandler(this.Determine_pin_locations_SavePins_Click);
            // 
            // determine_pin_locations_selectimage
            // 
            this.determine_pin_locations_selectimage.Location = new System.Drawing.Point(8, 3);
            this.determine_pin_locations_selectimage.Name = "determine_pin_locations_selectimage";
            this.determine_pin_locations_selectimage.Size = new System.Drawing.Size(75, 23);
            this.determine_pin_locations_selectimage.TabIndex = 2;
            this.determine_pin_locations_selectimage.Text = "Resim Seç";
            this.determine_pin_locations_selectimage.Click += new System.EventHandler(this.determine_pin_locations_selectimage_Click);
            // 
            // determine_pin_locations_resetallpins
            // 
            this.determine_pin_locations_resetallpins.Location = new System.Drawing.Point(8, 132);
            this.determine_pin_locations_resetallpins.Name = "determine_pin_locations_resetallpins";
            this.determine_pin_locations_resetallpins.Size = new System.Drawing.Size(75, 23);
            this.determine_pin_locations_resetallpins.TabIndex = 1;
            this.determine_pin_locations_resetallpins.Text = "Pinleri Sıfırla";
            this.determine_pin_locations_resetallpins.Click += new System.EventHandler(this.determine_pin_locations_resetallpins_Click);
            // 
            // determine_pin_locations_determinedpins
            // 
            this.determine_pin_locations_determinedpins.Location = new System.Drawing.Point(-4, 161);
            this.determine_pin_locations_determinedpins.Name = "determine_pin_locations_determinedpins";
            this.determine_pin_locations_determinedpins.Size = new System.Drawing.Size(243, 393);
            this.determine_pin_locations_determinedpins.TabIndex = 0;
            // 
            // xtraOpenFileDialog1
            // 
            this.xtraOpenFileDialog1.FileName = "xtraOpenFileDialog1";
            this.xtraOpenFileDialog1.Filter = ".jpg,.png,.bmp";
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // determine_pin_locations_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 557);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "determine_pin_locations_window";
            this.Text = "determine_pin_locations_window";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.determine_pin_locations_image.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.determine_pin_locations_determinedpins)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PictureEdit determine_pin_locations_image;
        private DevExpress.XtraEditors.SimpleButton determine_pin_locations_resetallpins;
        private DevExpress.XtraEditors.ListBoxControl determine_pin_locations_determinedpins;
        private DevExpress.XtraEditors.SimpleButton determine_pin_locations_selectimage;
        private DevExpress.XtraEditors.SimpleButton Determine_pin_locations_SavePins;
        private DevExpress.XtraEditors.XtraOpenFileDialog xtraOpenFileDialog1;
        private DevExpress.XtraEditors.SimpleButton determine_pin_locations_undo;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}