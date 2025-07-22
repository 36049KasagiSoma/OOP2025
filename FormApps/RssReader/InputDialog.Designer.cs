namespace RssReader {
    partial class InputDialog {
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
            label1 = new Label();
            lbUrl = new Label();
            label3 = new Label();
            tbInput = new TextBox();
            btOk = new Button();
            btCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(55, 21);
            label1.TabIndex = 0;
            label1.Text = "URL：";
            // 
            // lbUrl
            // 
            lbUrl.BackColor = SystemColors.ControlLightLight;
            lbUrl.Font = new Font("Yu Gothic UI", 12F);
            lbUrl.Location = new Point(73, 9);
            lbUrl.Name = "lbUrl";
            lbUrl.Size = new Size(420, 133);
            lbUrl.TabIndex = 0;
            lbUrl.Text = "I am URL";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F);
            label3.Location = new Point(12, 147);
            label3.Name = "label3";
            label3.Size = new Size(94, 21);
            label3.TabIndex = 0;
            label3.Text = "お気に入り名";
            // 
            // tbInput
            // 
            tbInput.Font = new Font("Yu Gothic UI", 12F);
            tbInput.Location = new Point(28, 171);
            tbInput.Name = "tbInput";
            tbInput.Size = new Size(465, 29);
            tbInput.TabIndex = 1;
            // 
            // btOk
            // 
            btOk.Font = new Font("Yu Gothic UI", 12F);
            btOk.Location = new Point(418, 210);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 33);
            btOk.TabIndex = 2;
            btOk.Text = "保存";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // btCancel
            // 
            btCancel.Font = new Font("Yu Gothic UI", 12F);
            btCancel.Location = new Point(337, 210);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(75, 33);
            btCancel.TabIndex = 2;
            btCancel.Text = "取消";
            btCancel.UseVisualStyleBackColor = true;
            btCancel.Click += btCancel_Click;
            // 
            // InputDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 255);
            Controls.Add(btCancel);
            Controls.Add(btOk);
            Controls.Add(tbInput);
            Controls.Add(lbUrl);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "InputDialog";
            Text = "保存名の入力";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lbUrl;
        private Label label3;
        private TextBox tbInput;
        private Button btOk;
        private Button btCancel;
    }
}