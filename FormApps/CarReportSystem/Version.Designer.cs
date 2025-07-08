namespace CarReportSystem {
    partial class Version {
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
            btOk = new Button();
            lbVersion = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("游ゴシック Medium", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(350, 35);
            label1.TabIndex = 0;
            label1.Text = "試乗レポート管理システム";
            // 
            // btOk
            // 
            btOk.Location = new Point(246, 62);
            btOk.Name = "btOk";
            btOk.Size = new Size(116, 41);
            btOk.TabIndex = 1;
            btOk.Text = "OK";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // lbVersion
            // 
            lbVersion.AutoSize = true;
            lbVersion.Font = new Font("游ゴシック Medium", 10F, FontStyle.Bold, GraphicsUnit.Point, 128);
            lbVersion.Location = new Point(29, 62);
            lbVersion.Name = "lbVersion";
            lbVersion.Size = new Size(74, 18);
            lbVersion.TabIndex = 0;
            lbVersion.Text = "Ver 0.0.1";
            // 
            // Version
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(372, 115);
            Controls.Add(btOk);
            Controls.Add(lbVersion);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Version";
            Text = "バージョン情報";
            Load += Version_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btOk;
        private Label lbVersion;
    }
}