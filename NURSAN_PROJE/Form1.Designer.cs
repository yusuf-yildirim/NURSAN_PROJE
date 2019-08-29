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
            DevExpress.DataAccess.Sql.SelectQuery selectQuery1 = new DevExpress.DataAccess.Sql.SelectQuery();
            DevExpress.DataAccess.Sql.Column column1 = new DevExpress.DataAccess.Sql.Column();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression1 = new DevExpress.DataAccess.Sql.ColumnExpression();
            DevExpress.DataAccess.Sql.Table table1 = new DevExpress.DataAccess.Sql.Table();
            DevExpress.DataAccess.Sql.Sorting sorting1 = new DevExpress.DataAccess.Sql.Sorting();
            DevExpress.DataAccess.Sql.ColumnExpression columnExpression2 = new DevExpress.DataAccess.Sql.ColumnExpression();
            this.newproject = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton16 = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.pROJECTSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pROJECTSBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJECTSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJECTSBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // newproject
            // 
            this.newproject.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.newproject.Appearance.Options.UseFont = true;
            this.newproject.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("newproject.ImageOptions.SvgImage")));
            this.newproject.Location = new System.Drawing.Point(42, 66);
            this.newproject.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newproject.Name = "newproject";
            this.newproject.Size = new System.Drawing.Size(256, 47);
            this.newproject.TabIndex = 0;
            this.newproject.Text = "Yeni Proje";
            this.newproject.Click += new System.EventHandler(this.newproject_Click);
            // 
            // simpleButton16
            // 
            this.simpleButton16.Location = new System.Drawing.Point(117, 490);
            this.simpleButton16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simpleButton16.Name = "simpleButton16";
            this.simpleButton16.Size = new System.Drawing.Size(137, 28);
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
            this.listBoxControl1.AppearanceHighlight.Font = new System.Drawing.Font("Tahoma", 49.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
            "ÖRNEK PROJE 1",
            "ÖRNEK PROJE 2",
            "ÖRNEK PROJE 3",
            "ÖRNEK PROJE 4",
            "ÖRNEK PROJE 5",
            "ÖRNEK PROJE 6",
            "ÖRNEK PROJE 7",
            "ÖRNEK PROJE 8",
            "ÖRNEK PROJE 9",
            "ÖRNEK PROJE 10",
            "ÖRNEK PROJE 11",
            "ÖRNEK PROJE 12",
            "ÖRNEK PROJE 13"});
            this.listBoxControl1.Location = new System.Drawing.Point(42, 155);
            this.listBoxControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listBoxControl1.Name = "listBoxControl1";
            this.listBoxControl1.Size = new System.Drawing.Size(256, 307);
            this.listBoxControl1.TabIndex = 3;
            this.listBoxControl1.SelectedIndexChanged += new System.EventHandler(this.listBoxControl1_SelectedIndexChanged);
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "database 1";
            this.sqlDataSource1.Name = "sqlDataSource1";
            columnExpression1.ColumnName = "PROJECT_NAME";
            table1.MetaSerializable = "<Meta X=\"30\" Y=\"30\" Width=\"125\" Height=\"96\" />";
            table1.Name = "PROJECTS";
            columnExpression1.Table = table1;
            column1.Expression = columnExpression1;
            selectQuery1.Columns.Add(column1);
            selectQuery1.Name = "PROJECTS";
            sorting1.Direction = System.ComponentModel.ListSortDirection.Descending;
            columnExpression2.ColumnName = "PROJECT_NAME";
            columnExpression2.Table = table1;
            sorting1.Expression = columnExpression2;
            selectQuery1.Sorting.Add(sorting1);
            selectQuery1.Tables.Add(table1);
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            selectQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = "PERhdGFTZXQgTmFtZT0ic3FsRGF0YVNvdXJjZTEiPjxWaWV3IE5hbWU9IlBST0pFQ1RTIj48RmllbGQgT" +
    "mFtZT0iUFJPSkVDVF9OQU1FIiBUeXBlPSJTdHJpbmciIC8+PC9WaWV3PjwvRGF0YVNldD4=";
            // 
            // pROJECTSBindingSource
            // 
            this.pROJECTSBindingSource.DataMember = "PROJECTS";
            this.pROJECTSBindingSource.DataSource = this.sqlDataSource1;
            // 
            // pROJECTSBindingSource1
            // 
            this.pROJECTSBindingSource1.DataMember = "PROJECTS";
            this.pROJECTSBindingSource1.DataSource = this.sqlDataSource1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 528);
            this.Controls.Add(this.listBoxControl1);
            this.Controls.Add(this.simpleButton16);
            this.Controls.Add(this.newproject);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nursan Projesi";
            ((System.ComponentModel.ISupportInitialize)(this.listBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJECTSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pROJECTSBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton newproject;
        private DevExpress.XtraEditors.SimpleButton simpleButton16;
        private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private System.Windows.Forms.BindingSource pROJECTSBindingSource;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private System.Windows.Forms.BindingSource pROJECTSBindingSource1;
    }
}

