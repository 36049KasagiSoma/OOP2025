﻿namespace RssReader {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            cbUrl = new ComboBox();
            btRssGet = new Button();
            label1 = new Label();
            lbTitles = new ListBox();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            splitContainer1 = new SplitContainer();
            panel2 = new Panel();
            tbListFilter = new TextBox();
            btListFilter = new Button();
            panel1 = new Panel();
            loadImage = new PictureBox();
            tbWebUrl = new TextBox();
            btReload = new Button();
            btWebForward = new Button();
            btWebBack = new Button();
            btRssFavorite = new Button();
            btRssFavoriteRemove = new Button();
            toolTip = new ToolTip(components);
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            filtedLabel = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            tsmiSetting = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            HelpToolStripMenuItem = new ToolStripMenuItem();
            tsmiAbout = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)loadImage).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // cbUrl
            // 
            cbUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbUrl.Font = new Font("Yu Gothic UI", 12F);
            cbUrl.Location = new Point(176, 27);
            cbUrl.Name = "cbUrl";
            cbUrl.Size = new Size(349, 29);
            cbUrl.TabIndex = 0;
            toolTip.SetToolTip(cbUrl, "対象のURLまたはお気に入り名を入力してください。");
            // 
            // btRssGet
            // 
            btRssGet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRssGet.Font = new Font("Yu Gothic UI", 12F);
            btRssGet.Location = new Point(531, 25);
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
            label1.Location = new Point(12, 32);
            label1.Name = "label1";
            label1.Size = new Size(158, 21);
            label1.TabIndex = 2;
            label1.Text = "URL（お気に入り名）:";
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.Font = new Font("Yu Gothic UI", 12F);
            lbTitles.FormattingEnabled = true;
            lbTitles.HorizontalScrollbar = true;
            lbTitles.ItemHeight = 21;
            lbTitles.Location = new Point(0, 31);
            lbTitles.Name = "lbTitles";
            lbTitles.RightToLeft = RightToLeft.No;
            lbTitles.Size = new Size(267, 361);
            lbTitles.TabIndex = 3;
            lbTitles.DrawItem += lbTitles_DrawItem;
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
            webView21.Size = new Size(404, 363);
            webView21.TabIndex = 4;
            webView21.ZoomFactor = 1D;
            webView21.NavigationStarting += webView21_NavigationStarting;
            webView21.NavigationCompleted += webView21_NavigationCompleted;
            webView21.SourceChanged += webView21_SourceChanged;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.Location = new Point(12, 61);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Controls.Add(lbTitles);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(webView21);
            splitContainer1.Size = new Size(675, 403);
            splitContainer1.SplitterDistance = 267;
            splitContainer1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Controls.Add(tbListFilter);
            panel2.Controls.Add(btListFilter);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(267, 31);
            panel2.TabIndex = 7;
            // 
            // tbListFilter
            // 
            tbListFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbListFilter.Font = new Font("Yu Gothic UI", 12F);
            tbListFilter.Location = new Point(2, 1);
            tbListFilter.Name = "tbListFilter";
            tbListFilter.Size = new Size(231, 29);
            tbListFilter.TabIndex = 2;
            tbListFilter.KeyDown += tbListFilter_KeyDown;
            // 
            // btListFilter
            // 
            btListFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btListFilter.Image = (Image)resources.GetObject("btListFilter.Image");
            btListFilter.Location = new Point(234, 0);
            btListFilter.Name = "btListFilter";
            btListFilter.Size = new Size(33, 30);
            btListFilter.TabIndex = 0;
            toolTip.SetToolTip(btListFilter, "フィルター");
            btListFilter.UseVisualStyleBackColor = true;
            btListFilter.Click += btListFilter_Click;
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
            panel1.Size = new Size(404, 23);
            panel1.TabIndex = 6;
            // 
            // loadImage
            // 
            loadImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loadImage.Image = (Image)resources.GetObject("loadImage.Image");
            loadImage.Location = new Point(384, 3);
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
            tbWebUrl.Size = new Size(240, 20);
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
            toolTip.SetToolTip(btReload, "ページの再読み込み");
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
            toolTip.SetToolTip(btWebForward, "次へ");
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
            toolTip.SetToolTip(btWebBack, "前へ");
            btWebBack.UseVisualStyleBackColor = true;
            btWebBack.Click += btWebBack_Click;
            // 
            // btRssFavorite
            // 
            btRssFavorite.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRssFavorite.Font = new Font("Yu Gothic UI", 12F);
            btRssFavorite.Image = (Image)resources.GetObject("btRssFavorite.Image");
            btRssFavorite.Location = new Point(597, 25);
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
            btRssFavoriteRemove.Location = new Point(645, 25);
            btRssFavoriteRemove.Name = "btRssFavoriteRemove";
            btRssFavoriteRemove.Size = new Size(42, 30);
            btRssFavoriteRemove.TabIndex = 1;
            toolTip.SetToolTip(btRssFavoriteRemove, "入力されたお気に入り名から、お気に入りを削除します。");
            btRssFavoriteRemove.UseVisualStyleBackColor = true;
            btRssFavoriteRemove.Click += btRssFavoriteRemove_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel, filtedLabel });
            statusStrip1.Location = new Point(0, 454);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(699, 22);
            statusStrip1.TabIndex = 6;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 17);
            // 
            // filtedLabel
            // 
            filtedLabel.Name = "filtedLabel";
            filtedLabel.Size = new Size(0, 17);
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, HelpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(699, 24);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiSetting, toolStripSeparator1, tsmiExit });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(67, 20);
            FileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // tsmiSetting
            // 
            tsmiSetting.Name = "tsmiSetting";
            tsmiSetting.Size = new Size(155, 22);
            tsmiSetting.Text = "設定(&O)...";
            tsmiSetting.Click += tsmiSetting_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.ShortcutKeys = Keys.Alt | Keys.F4;
            tsmiExit.Size = new Size(155, 22);
            tsmiExit.Text = "終了(&X)";
            tsmiExit.Click += ExitXToolStripMenuItem_Click;
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiAbout });
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            HelpToolStripMenuItem.Size = new Size(65, 20);
            HelpToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // tsmiAbout
            // 
            tsmiAbout.Name = "tsmiAbout";
            tsmiAbout.Size = new Size(214, 22);
            tsmiAbout.Text = "このアプリケーションについて(&A)";
            tsmiAbout.Click += AbToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(152, 6);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 476);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(btRssFavoriteRemove);
            Controls.Add(btRssFavorite);
            Controls.Add(btRssGet);
            Controls.Add(cbUrl);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(400, 250);
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)loadImage).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
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
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private TextBox tbListFilter;
        private Button btListFilter;
        private Panel panel2;
        private ToolStripStatusLabel filtedLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem tsmiSetting;
        private ToolStripMenuItem tsmiExit;
        private ToolStripMenuItem HelpToolStripMenuItem;
        private ToolStripMenuItem tsmiAbout;
        private ToolStripSeparator toolStripSeparator1;
    }
}
