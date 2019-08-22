namespace NURSAN_PROJE
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.newproject = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton16 = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // newproject
            // 
            this.newproject.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.newproject.Appearance.Options.UseFont = true;
            this.newproject.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.newproject.Location = new System.Drawing.Point(49, 81);
            this.newproject.Name = "newproject";
            this.newproject.Size = new System.Drawing.Size(299, 58);
            this.newproject.TabIndex = 0;
            this.newproject.Text = "Yeni Proje";
            this.newproject.Click += new System.EventHandler(this.newproject_Click);
            // 
            // simpleButton16
            // 
            this.simpleButton16.Location = new System.Drawing.Point(136, 603);
            this.simpleButton16.Name = "simpleButton16";
            this.simpleButton16.Size = new System.Drawing.Size(160, 35);
            this.simpleButton16.TabIndex = 2;
            this.simpleButton16.Text = "Çıkış";
            this.simpleButton16.Click += new System.EventHandler(this.simpleButton16_Click);
            // 
            // listBoxControl1
            // 
            this.listBoxControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBoxControl1.Appearance.Options.UseFont = true;
            this.listBoxControl1.AppearanceHighlight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxControl1.AppearanceHighlight.BackColor2 = System.Drawing.Color.Gainsboro;
            this.listBoxControl1.AppearanceHighlight.Font = new System.Drawing.Font("Tahoma", 34.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBoxControl1.AppearanceHighlight.FontSizeDelta = 15;
            this.listBoxControl1.AppearanceHighlight.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.listBoxControl1.AppearanceHighlight.Options.UseBackColor = true;
            this.listBoxControl1.AppearanceHighlight.Options.UseFont = true;
            this.behaviorManager1.SetBehaviors(this.listBoxControl1, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.Behaviors.ScrollAnnotationsBehavior.Create(typeof(DevExpress.XtraEditors.Behaviors.ScrollAnnotationsBehaviorSourceForListBox), ((DevExpress.Utils.Behaviors.ScrollAnnotationType)((DevExpress.Utils.Behaviors.ScrollAnnotationType.SearchResult | DevExpress.Utils.Behaviors.ScrollAnnotationType.Selection))), new System.Drawing.Color[] {
                        System.Drawing.Color.Sienna,
                        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))))}, new DevExpress.Utils.Behaviors.ScrollAnnotationAlignment[] {
                        DevExpress.Utils.Behaviors.ScrollAnnotationAlignment.Default,
                        DevExpress.Utils.Behaviors.ScrollAnnotationAlignment.Center})))});
            this.listBoxControl1.ContextButtonOptions.AllowGlyphSkinning = true;
            this.listBoxControl1.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.listBoxControl1.HotTrackItems = true;
            this.listBoxControl1.ItemPadding = new System.Windows.Forms.Padding(5500, -15, 0, 0);
            this.listBoxControl1.Items.AddRange(new object[] {
            "ıoaşdsasd7",
            "asdas",
            "dasd",
            "as",
            "da",
            "sd",
            "asdasdasdasdasd",
            "asdasdasdasd",
            "asdasdasdasd",
            "asdasdasdasdas"});
            this.listBoxControl1.Location = new System.Drawing.Point(49, 191);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(299, 378);
            this.listBoxControl1.TabIndex = 3;
            this.listBoxControl1.SelectedIndexChanged += new System.EventHandler(this.listBoxControl1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 650);
            this.Controls.Add(this.listBoxControl1);
            this.Controls.Add(this.simpleButton16);
            this.Controls.Add(this.newproject);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nursan Projesi";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton newproject;
        private DevExpress.XtraEditors.SimpleButton simpleButton16;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
    }
}

