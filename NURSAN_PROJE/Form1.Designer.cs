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
            DevExpress.XtraEditors.TableLayout.ItemTemplateBase ıtemTemplateBase1 = new DevExpress.XtraEditors.TableLayout.ItemTemplateBase();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition1 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition2 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TemplatedItemElement templatedItemElement1 = new DevExpress.XtraEditors.TableLayout.TemplatedItemElement();
            DevExpress.XtraEditors.TableLayout.TemplatedItemElement templatedItemElement2 = new DevExpress.XtraEditors.TableLayout.TemplatedItemElement();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition1 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            this.newproject = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton16 = new DevExpress.XtraEditors.SimpleButton();
            this.projectlistbox = new DevExpress.XtraEditors.ListBoxControl();
            this.projeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.selectscreendb = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dropDownButton1 = new DevExpress.XtraEditors.DropDownButton();
            this.label1 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.projectlistbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.projeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // newproject
            // 
            this.newproject.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.newproject.Appearance.Options.UseFont = true;
            this.newproject.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("newproject.ImageOptions.SvgImage")));
            this.newproject.Location = new System.Drawing.Point(28, 76);
            this.newproject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newproject.Name = "newproject";
            this.newproject.Size = new System.Drawing.Size(256, 47);
            this.newproject.TabIndex = 0;
            this.newproject.Text = "Yeni Proje";
            this.newproject.Click += new System.EventHandler(this.newproject_Click);
            // 
            // simpleButton16
            // 
            this.simpleButton16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.simpleButton16.Location = new System.Drawing.Point(0, 500);
            this.simpleButton16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton16.Name = "simpleButton16";
            this.simpleButton16.Size = new System.Drawing.Size(315, 28);
            this.simpleButton16.TabIndex = 2;
            this.simpleButton16.Text = "Çıkış";
            this.simpleButton16.Click += new System.EventHandler(this.simpleButton16_Click);
            // 
            // projectlistbox
            // 
            this.projectlistbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectlistbox.Appearance.Font = new System.Drawing.Font("Tahoma", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.projectlistbox.Appearance.Options.UseFont = true;
            this.projectlistbox.AppearanceHighlight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.projectlistbox.AppearanceHighlight.BackColor2 = System.Drawing.Color.Gainsboro;
            this.projectlistbox.AppearanceHighlight.Font = new System.Drawing.Font("Tahoma", 214.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.projectlistbox.AppearanceHighlight.FontSizeDelta = 15;
            this.projectlistbox.AppearanceHighlight.FontStyleDelta = System.Drawing.FontStyle.Bold;
            this.projectlistbox.AppearanceHighlight.Options.UseBackColor = true;
            this.projectlistbox.AppearanceHighlight.Options.UseFont = true;
            this.behaviorManager1.SetBehaviors(this.projectlistbox, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.Behaviors.ScrollAnnotationsBehavior.Create(typeof(DevExpress.XtraEditors.Behaviors.ScrollAnnotationsBehaviorSourceForListBox), ((DevExpress.Utils.Behaviors.ScrollAnnotationType)((DevExpress.Utils.Behaviors.ScrollAnnotationType.SearchResult | DevExpress.Utils.Behaviors.ScrollAnnotationType.Selection))), new System.Drawing.Color[] {
                        System.Drawing.Color.Sienna,
                        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))))}, new DevExpress.Utils.Behaviors.ScrollAnnotationAlignment[] {
                        DevExpress.Utils.Behaviors.ScrollAnnotationAlignment.Default,
                        DevExpress.Utils.Behaviors.ScrollAnnotationAlignment.Center})))});
            this.projectlistbox.ContextButtonOptions.AllowGlyphSkinning = true;
            this.projectlistbox.DataSource = this.projeBindingSource;
            this.projectlistbox.DisplayMember = "NAME";
            this.projectlistbox.HighlightedItemStyle = DevExpress.XtraEditors.HighlightStyle.Skinned;
            this.projectlistbox.HotTrackItems = true;
            this.projectlistbox.ItemHeight = 19;
            this.projectlistbox.ItemPadding = new System.Windows.Forms.Padding(5500, -15, 0, 0);
            this.projectlistbox.Location = new System.Drawing.Point(28, 209);
            this.projectlistbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.projectlistbox.Name = "projectlistbox";
            this.projectlistbox.Padding = new System.Windows.Forms.Padding(0, 0, 0, 50);
            this.projectlistbox.Size = new System.Drawing.Size(256, 287);
            this.projectlistbox.TabIndex = 3;
            tableColumnDefinition1.Length.Value = 170D;
            tableColumnDefinition2.Length.Value = 82D;
            ıtemTemplateBase1.Columns.Add(tableColumnDefinition1);
            ıtemTemplateBase1.Columns.Add(tableColumnDefinition2);
            templatedItemElement1.FieldName = "NAME";
            templatedItemElement1.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            templatedItemElement1.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            templatedItemElement1.Text = "NAME";
            templatedItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            templatedItemElement2.ColumnIndex = 1;
            templatedItemElement2.FieldName = "PATH";
            templatedItemElement2.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            templatedItemElement2.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            templatedItemElement2.Text = "PATH";
            templatedItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            ıtemTemplateBase1.Elements.Add(templatedItemElement1);
            ıtemTemplateBase1.Elements.Add(templatedItemElement2);
            ıtemTemplateBase1.Name = "template1";
            tableRowDefinition1.Length.Value = 30D;
            ıtemTemplateBase1.Rows.Add(tableRowDefinition1);
            this.projectlistbox.Templates.Add(ıtemTemplateBase1);
            this.projectlistbox.ValueMember = "PATH";
            this.projectlistbox.SelectedIndexChanged += new System.EventHandler(this.listBoxControl1_SelectedIndexChanged);
            // 
            // projeBindingSource
            // 
            this.projeBindingSource.DataMember = "proje";
            this.projeBindingSource.DataSource = this.selectscreendb;
            // 
            // selectscreendb
            // 
            this.selectscreendb.ConnectionName = "maindatabase";
            this.selectscreendb.Name = "selectscreendb";
            customSqlQuery1.Name = "proje";
            customSqlQuery1.Sql = "select [Recent].[NAME], [Recent].[PATH]\r\n  from [Recent] [Recent]";
            this.selectscreendb.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.selectscreendb.ResultSchemaSerializable = "PERhdGFTZXQgTmFtZT0ic2VsZWN0c2NyZWVuZGIiPjxWaWV3IE5hbWU9InByb2plIj48RmllbGQgTmFtZ" +
    "T0iTkFNRSIgVHlwZT0iU3RyaW5nIiAvPjxGaWVsZCBOYW1lPSJQQVRIIiBUeXBlPSJTdHJpbmciIC8+P" +
    "C9WaWV3PjwvRGF0YVNldD4=";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(28, 30);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(81, 24);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Yeni Kullanıcı";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dropDownButton1
            // 
            this.dropDownButton1.Location = new System.Drawing.Point(152, 30);
            this.dropDownButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dropDownButton1.Name = "dropDownButton1";
            this.dropDownButton1.Size = new System.Drawing.Size(132, 24);
            this.dropDownButton1.TabIndex = 5;
            this.dropDownButton1.Text = "Kullanıcı Listesi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(28, 158);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(256, 47);
            this.simpleButton2.TabIndex = 7;
            this.simpleButton2.Text = "SON AÇILAN DOSYALAR";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 528);
            this.ControlBox = false;
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dropDownButton1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.projectlistbox);
            this.Controls.Add(this.simpleButton16);
            this.Controls.Add(this.newproject);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nursan Projesi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectlistbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.projeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton newproject;
        private DevExpress.XtraEditors.SimpleButton simpleButton16;
        private DevExpress.XtraEditors.ListBoxControl projectlistbox;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.DropDownButton dropDownButton1;
        private System.Windows.Forms.Label label1;
        private DevExpress.DataAccess.Sql.SqlDataSource selectscreendb;
        private System.Windows.Forms.BindingSource projeBindingSource;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

