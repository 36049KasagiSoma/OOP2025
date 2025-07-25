namespace RssReader {
    partial class SettingDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDialog));
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            colorDialog = new ColorDialog();
            label2 = new Label();
            label3 = new Label();
            Opanel = new Panel();
            Epanel = new Panel();
            Obutton = new Button();
            Ebutton = new Button();
            timeoutValue = new NumericUpDown();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            Bpanel = new Panel();
            Bbutton = new Button();
            button3 = new Button();
            label7 = new Label();
            Tpanel = new Panel();
            Tbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)timeoutValue).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(239, 212);
            button1.Name = "button1";
            button1.Size = new Size(89, 33);
            button1.TabIndex = 0;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(144, 212);
            button2.Name = "button2";
            button2.Size = new Size(89, 33);
            button2.TabIndex = 0;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(58, 21);
            label1.TabIndex = 1;
            label1.Text = "色設定";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(32, 39);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "奇数行：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(32, 65);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 2;
            label3.Text = "偶数行：";
            // 
            // Opanel
            // 
            Opanel.BorderStyle = BorderStyle.FixedSingle;
            Opanel.Location = new Point(93, 40);
            Opanel.Name = "Opanel";
            Opanel.Size = new Size(34, 14);
            Opanel.TabIndex = 3;
            // 
            // Epanel
            // 
            Epanel.BorderStyle = BorderStyle.FixedSingle;
            Epanel.Location = new Point(93, 66);
            Epanel.Name = "Epanel";
            Epanel.Size = new Size(34, 14);
            Epanel.TabIndex = 3;
            // 
            // Obutton
            // 
            Obutton.Location = new Point(133, 35);
            Obutton.Name = "Obutton";
            Obutton.Size = new Size(75, 23);
            Obutton.TabIndex = 4;
            Obutton.Text = "選択...";
            Obutton.UseVisualStyleBackColor = true;
            Obutton.Click += Obutton_Click;
            // 
            // Ebutton
            // 
            Ebutton.Location = new Point(133, 61);
            Ebutton.Name = "Ebutton";
            Ebutton.Size = new Size(75, 23);
            Ebutton.TabIndex = 4;
            Ebutton.Text = "選択...";
            Ebutton.UseVisualStyleBackColor = true;
            Ebutton.Click += Ebutton_Click;
            // 
            // timeoutValue
            // 
            timeoutValue.Location = new Point(32, 165);
            timeoutValue.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            timeoutValue.Name = "timeoutValue";
            timeoutValue.Size = new Size(120, 23);
            timeoutValue.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            label4.Location = new Point(12, 141);
            label4.Name = "label4";
            label4.Size = new Size(115, 21);
            label4.TabIndex = 1;
            label4.Text = "タイムアウト設定";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(158, 173);
            label5.Name = "label5";
            label5.Size = new Size(19, 15);
            label5.TabIndex = 2;
            label5.Text = "秒";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 92);
            label6.Name = "label6";
            label6.Size = new Size(55, 15);
            label6.TabIndex = 2;
            label6.Text = "背景色：";
            // 
            // Bpanel
            // 
            Bpanel.BorderStyle = BorderStyle.FixedSingle;
            Bpanel.Location = new Point(93, 92);
            Bpanel.Name = "Bpanel";
            Bpanel.Size = new Size(34, 14);
            Bpanel.TabIndex = 3;
            // 
            // Bbutton
            // 
            Bbutton.Location = new Point(133, 88);
            Bbutton.Name = "Bbutton";
            Bbutton.Size = new Size(75, 23);
            Bbutton.TabIndex = 4;
            Bbutton.Text = "選択...";
            Bbutton.UseVisualStyleBackColor = true;
            Bbutton.Click += Bbutton_Click;
            // 
            // button3
            // 
            button3.Location = new Point(6, 212);
            button3.Name = "button3";
            button3.Size = new Size(89, 33);
            button3.TabIndex = 0;
            button3.Text = "デフォルト";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(32, 119);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 2;
            label7.Text = "文字色：";
            // 
            // Tpanel
            // 
            Tpanel.BorderStyle = BorderStyle.FixedSingle;
            Tpanel.Location = new Point(93, 119);
            Tpanel.Name = "Tpanel";
            Tpanel.Size = new Size(34, 14);
            Tpanel.TabIndex = 3;
            // 
            // Tbutton
            // 
            Tbutton.Location = new Point(133, 115);
            Tbutton.Name = "Tbutton";
            Tbutton.Size = new Size(75, 23);
            Tbutton.TabIndex = 4;
            Tbutton.Text = "選択...";
            Tbutton.UseVisualStyleBackColor = true;
            Tbutton.Click += Tbutton_Click;
            // 
            // SettingDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 251);
            Controls.Add(timeoutValue);
            Controls.Add(Tbutton);
            Controls.Add(Bbutton);
            Controls.Add(Ebutton);
            Controls.Add(Obutton);
            Controls.Add(Tpanel);
            Controls.Add(Bpanel);
            Controls.Add(Epanel);
            Controls.Add(Opanel);
            Controls.Add(label5);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label4);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(350, 290);
            MinimumSize = new Size(350, 290);
            Name = "SettingDialog";
            Text = "設定";
            Load += SettingDialog_Load;
            ((System.ComponentModel.ISupportInitialize)timeoutValue).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Label label1;
        private ColorDialog colorDialog;
        private Label label2;
        private Label label3;
        private Panel Opanel;
        private Panel Epanel;
        private Button Obutton;
        private Button Ebutton;
        private NumericUpDown timeoutValue;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel Bpanel;
        private Button Bbutton;
        private Button button3;
        private Label label7;
        private Panel Tpanel;
        private Button Tbutton;
    }
}