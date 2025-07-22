namespace RssReader {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        System.ComponentModel.ComponentResourceManager resources;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cbUrl = new ComboBox();
            btRssGet = new Button();
            label1 = new Label();
            lbTitles = new ListBox();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            panel1 = new Panel();
            loadImage = new PictureBox();
            tbWebUrl = new TextBox();
            btReload = new Button();
            btWebForward = new Button();
            btWebBack = new Button();
            btRssFavorite = new Button();
            btRssFavoriteRemove = new Button();
            toolTip = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loadImage).BeginInit();
            SuspendLayout();
            // 
            // cbUrl
            // 
            cbUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbUrl.Font = new Font("Yu Gothic UI", 12F);
            cbUrl.Location = new Point(60, 6);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(373, 29);
            cbUrl.TabIndex = 0;
            toolTip.SetToolTip(cbUrl, "対象のURLまたはお気に入り名を入力してください。");
            // 
            // btRssGet
            // 
            btRssGet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRssGet.Font = new Font("Yu Gothic UI", 12F);
            btRssGet.Location = new Point(439, 4);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(60, 30);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            toolTip.SetToolTip(btRssGet, "入力されたURLから、記事を取得します。");
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(42, 21);
            label1.TabIndex = 2;
            label1.Text = "URL:";
            // 
            // lbTitles
            // 
            lbTitles.Dock = DockStyle.Fill;
            lbTitles.Font = new Font("Yu Gothic UI", 12F);
            lbTitles.FormattingEnabled = true;
            lbTitles.HorizontalScrollbar = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(0, 0);
            lbTitles.Name = "lbTitles";
            lbTitles.RightToLeft = RightToLeft.No;
            lbTitles.Size = new Size(231, 396);
            lbTitles.TabIndex = 3;
            lbTitles.DoubleClick += lbTitles_DoubleClick;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Location = new Point(0, 27);
            webView21.Name = "webView21";
            webView21.Size = new Size(348, 369);
            webView21.TabIndex = 4;
            webView21.ZoomFactor = 1D;
            webView21.NavigationStarting += webView21_NavigationStarting;
            webView21.NavigationCompleted += webView21_NavigationCompleted;
            webView21.SourceChanged += webView21_SourceChanged;

            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 42);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lbTitles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(webView21);
            splitContainer1.Size = new Size(583, 396);
            splitContainer1.SplitterDistance = 231;
            splitContainer1.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(loadImage);
            panel1.Controls.Add(tbWebUrl);
            panel1.Controls.Add(btReload);
            panel1.Controls.Add(btWebForward);
            panel1.Controls.Add(btWebBack);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(348, 23);
            panel1.TabIndex = 6;
            // 
            // loadImage
            // 
            loadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loadImage.Image = (Image)resources.GetObject("loadImage.Image");
            loadImage.Location = new Point(328, 3);
            loadImage.Name = "loadImage";
            loadImage.Size = new Size(17, 17);
            loadImage.SizeMode = PictureBoxSizeMode.StretchImage;
            loadImage.TabIndex = 3;
            loadImage.TabStop = false;
            // 
            // tbWebUrl
            // 
            tbWebUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbWebUrl.Font = new Font("Yu Gothic UI", 7F);
            tbWebUrl.Location = new Point(138, 1);
            tbWebUrl.Name = "tbWebUrl";
            tbWebUrl.Size = new Size(184, 20);
            tbWebUrl.TabIndex = 2;
            tbWebUrl.KeyDown += tbWebUrl_KeyDown;
            // 
            // btReload
            // 
            btReload.Location = new Point(95, 0);
            btReload.Name = "btReload";
            btReload.Size = new Size(40, 23);
            btReload.TabIndex = 1;
            btReload.Text = "↻";
            btReload.UseVisualStyleBackColor = true;
            btReload.Click += btReload_Click;
            // 
            // btWebForward
            // 
            btWebForward.Location = new Point(49, 0);
            btWebForward.Name = "btWebForward";
            btWebForward.Size = new Size(40, 23);
            btWebForward.TabIndex = 1;
            btWebForward.Text = "→";
            btWebForward.UseVisualStyleBackColor = true;
            btWebForward.Click += btWebForward_Click;
            // 
            // btWebBack
            // 
            btWebBack.Location = new Point(3, 0);
            btWebBack.Name = "btWebBack";
            btWebBack.Size = new Size(40, 23);
            btWebBack.TabIndex = 0;
            btWebBack.Text = "←";
            btWebBack.UseVisualStyleBackColor = true;
            btWebBack.Click += btWebBack_Click;
            // 
            // btRssFavorite
            // 
            btRssFavorite.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRssFavorite.Font = new Font("Yu Gothic UI", 12F);
            btRssFavorite.Image = (Image)resources.GetObject("btRssFavorite.Image");
            btRssFavorite.Location = new Point(505, 4);
            btRssFavorite.Name = "btRssFavorite";
            btRssFavorite.Size = new Size(42, 30);
            btRssFavorite.TabIndex = 1;
            toolTip.SetToolTip(btRssFavorite, "入力されたURLを名前を付けてお気に入り登録します。");
            btRssFavorite.UseVisualStyleBackColor = true;
            btRssFavorite.Click += btRssFavorite_Click;
            // 
            // btRssFavoriteRemove
            // 
            btRssFavoriteRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRssFavoriteRemove.Font = new Font("Yu Gothic UI", 12F);
            btRssFavoriteRemove.Image = (Image)resources.GetObject("btRssFavoriteRemove.Image");
            btRssFavoriteRemove.Location = new Point(553, 4);
            btRssFavoriteRemove.Name = "btRssFavoriteRemove";
            btRssFavoriteRemove.Size = new Size(42, 30);
            btRssFavoriteRemove.TabIndex = 1;
            toolTip.SetToolTip(btRssFavoriteRemove, "入力されたお気に入り名から、お気に入りを削除します。");
            btRssFavoriteRemove.UseVisualStyleBackColor = true;
            btRssFavoriteRemove.Click += btRssFavoriteRemove_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(607, 450);
            Controls.Add(splitContainer1);
            Controls.Add(btRssFavoriteRemove);
            Controls.Add(btRssFavorite);
            Controls.Add(btRssGet);
            Controls.Add(cbUrl);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loadImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbUrl;
        private Button btRssGet;
        private Label label1;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private SplitContainer splitContainer1;
        private Panel panel1;
        private Button btWebForward;
        private Button btWebBack;
        private Button btReload;
        private Button btRssFavorite;
        private TextBox tbWebUrl;
        private Button btRssFavoriteRemove;
        private ToolTip toolTip;
        private PictureBox loadImage;
    }
}
