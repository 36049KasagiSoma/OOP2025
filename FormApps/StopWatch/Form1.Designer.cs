namespace StopWatch {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.lbTimeDisp = new System.Windows.Forms.Label();
            this.tmDispTimer = new System.Windows.Forms.Timer(this.components);
            this.bdRap = new System.Windows.Forms.Button();
            this.bdReset = new System.Windows.Forms.Button();
            this.bdStop = new System.Windows.Forms.Button();
            this.bdStart = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // lbTimeDisp
            // 
            this.lbTimeDisp.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbTimeDisp.Font = new System.Drawing.Font("MS UI Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTimeDisp.Location = new System.Drawing.Point(17, 9);
            this.lbTimeDisp.Name = "lbTimeDisp";
            this.lbTimeDisp.Size = new System.Drawing.Size(518, 67);
            this.lbTimeDisp.TabIndex = 0;
            this.lbTimeDisp.Text = "00:00:00.00";
            this.lbTimeDisp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmDispTimer
            // 
            this.tmDispTimer.Tick += new System.EventHandler(this.tmDispTimer_Tick);
            // 
            // bdRap
            // 
            this.bdRap.BackgroundImage = global::StopWatch.Properties.Resources.証明書アイコン;
            this.bdRap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bdRap.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bdRap.Location = new System.Drawing.Point(410, 79);
            this.bdRap.Name = "bdRap";
            this.bdRap.Size = new System.Drawing.Size(125, 77);
            this.bdRap.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bdRap, "ラップ");
            this.bdRap.UseVisualStyleBackColor = true;
            this.bdRap.Click += new System.EventHandler(this.bdRap_Click);
            // 
            // bdReset
            // 
            this.bdReset.BackgroundImage = global::StopWatch.Properties.Resources.太いリロードアイコン;
            this.bdReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bdReset.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bdReset.Location = new System.Drawing.Point(279, 79);
            this.bdReset.Name = "bdReset";
            this.bdReset.Size = new System.Drawing.Size(125, 77);
            this.bdReset.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bdReset, "リセット");
            this.bdReset.UseVisualStyleBackColor = true;
            this.bdReset.Click += new System.EventHandler(this.bdReset_Click);
            // 
            // bdStop
            // 
            this.bdStop.BackgroundImage = global::StopWatch.Properties.Resources.再生停止ボタン;
            this.bdStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bdStop.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bdStop.Location = new System.Drawing.Point(148, 79);
            this.bdStop.Name = "bdStop";
            this.bdStop.Size = new System.Drawing.Size(125, 77);
            this.bdStop.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bdStop, "ストップ");
            this.bdStop.UseVisualStyleBackColor = true;
            this.bdStop.Click += new System.EventHandler(this.bdStop_Click);
            // 
            // bdStart
            // 
            this.bdStart.BackgroundImage = global::StopWatch.Properties.Resources.再生ボタン;
            this.bdStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bdStart.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bdStart.Location = new System.Drawing.Point(17, 79);
            this.bdStart.Name = "bdStart";
            this.bdStart.Size = new System.Drawing.Size(125, 77);
            this.bdStart.TabIndex = 1;
            this.toolTip1.SetToolTip(this.bdStart, "スタート");
            this.bdStart.UseVisualStyleBackColor = true;
            this.bdStart.Click += new System.EventHandler(this.bdStart_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(17, 162);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(518, 88);
            this.listBox1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 261);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.bdRap);
            this.Controls.Add(this.bdReset);
            this.Controls.Add(this.bdStop);
            this.Controls.Add(this.bdStart);
            this.Controls.Add(this.lbTimeDisp);
            this.MaximumSize = new System.Drawing.Size(570, 300);
            this.MinimumSize = new System.Drawing.Size(570, 300);
            this.Name = "Form1";
            this.Text = "ストップウォッチ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTimeDisp;
        private System.Windows.Forms.Button bdStart;
        private System.Windows.Forms.Button bdStop;
        private System.Windows.Forms.Button bdReset;
        private System.Windows.Forms.Timer tmDispTimer;
        private System.Windows.Forms.Button bdRap;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

