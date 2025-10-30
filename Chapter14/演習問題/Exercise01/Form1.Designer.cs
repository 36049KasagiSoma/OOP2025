namespace Exercise01 {
    partial class テキスト読み込み {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            statusStrip1 = new StatusStrip();
            tssl = new ToolStripStatusLabel();
            sttl2 = new ToolStripStatusLabel();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            openFileDialog1 = new OpenFileDialog();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tssl, sttl2 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // tssl
            // 
            tssl.Name = "tssl";
            tssl.Size = new Size(667, 17);
            tssl.Spring = true;
            tssl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // sttl2
            // 
            sttl2.Name = "sttl2";
            sttl2.Size = new Size(118, 17);
            sttl2.Text = "toolStripStatusLabel1";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(12, 47);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(776, 378);
            textBox1.TabIndex = 1;
            textBox1.WordWrap = false;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Location = new Point(727, 8);
            button1.Name = "button1";
            button1.Size = new Size(61, 29);
            button1.TabIndex = 2;
            button1.Text = "参照...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(12, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(709, 23);
            textBox2.TabIndex = 3;
            textBox2.KeyDown += textBox2_KeyDown;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // テキスト読み込み
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(statusStrip1);
            Name = "テキスト読み込み";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tssl;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private OpenFileDialog openFileDialog1;
        private ToolStripStatusLabel sttl2;
    }
}
