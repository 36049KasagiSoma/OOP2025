namespace UnitConverter {
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbNum1 = new System.Windows.Forms.TextBox();
            this.tbNum2 = new System.Windows.Forms.TextBox();
            this.btChange = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.nud1 = new System.Windows.Forms.NumericUpDown();
            this.nud2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudAns = new System.Windows.Forms.NumericUpDown();
            this.dbCalc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudNotMuch = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNotMuch)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "距離換算";
            // 
            // tbNum1
            // 
            this.tbNum1.BackColor = System.Drawing.Color.DimGray;
            this.tbNum1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbNum1.ForeColor = System.Drawing.Color.White;
            this.tbNum1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbNum1.Location = new System.Drawing.Point(129, 65);
            this.tbNum1.Name = "tbNum1";
            this.tbNum1.ShortcutsEnabled = false;
            this.tbNum1.Size = new System.Drawing.Size(277, 31);
            this.tbNum1.TabIndex = 0;
            this.tbNum1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Conversion_KeyPress);
            // 
            // tbNum2
            // 
            this.tbNum2.BackColor = System.Drawing.Color.DimGray;
            this.tbNum2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tbNum2.ForeColor = System.Drawing.Color.White;
            this.tbNum2.Location = new System.Drawing.Point(129, 181);
            this.tbNum2.Name = "tbNum2";
            this.tbNum2.Size = new System.Drawing.Size(277, 31);
            this.tbNum2.TabIndex = 4;
            // 
            // btChange
            // 
            this.btChange.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btChange.Location = new System.Drawing.Point(250, 102);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(118, 73);
            this.btChange.TabIndex = 3;
            this.btChange.Text = "変換\r\n↓";
            this.btChange.UseVisualStyleBackColor = true;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "変換前：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(29, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 3;
            this.label3.Text = "変換後：";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "m",
            "in",
            "yd"});
            this.comboBox1.Location = new System.Drawing.Point(412, 65);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(74, 32);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comboBox2.ForeColor = System.Drawing.Color.White;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "m",
            "in",
            "yd"});
            this.comboBox2.Location = new System.Drawing.Point(412, 181);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(74, 32);
            this.comboBox2.TabIndex = 2;
            // 
            // nud1
            // 
            this.nud1.BackColor = System.Drawing.Color.DimGray;
            this.nud1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nud1.ForeColor = System.Drawing.Color.White;
            this.nud1.Location = new System.Drawing.Point(33, 316);
            this.nud1.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nud1.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nud1.Name = "nud1";
            this.nud1.Size = new System.Drawing.Size(186, 31);
            this.nud1.TabIndex = 5;
            // 
            // nud2
            // 
            this.nud2.BackColor = System.Drawing.Color.DimGray;
            this.nud2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nud2.ForeColor = System.Drawing.Color.White;
            this.nud2.Location = new System.Drawing.Point(269, 316);
            this.nud2.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nud2.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nud2.Name = "nud2";
            this.nud2.Size = new System.Drawing.Size(186, 31);
            this.nud2.TabIndex = 6;
            this.nud2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(229, 318);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "÷";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(461, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "=";
            // 
            // nudAns
            // 
            this.nudAns.BackColor = System.Drawing.Color.DimGray;
            this.nudAns.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudAns.ForeColor = System.Drawing.Color.White;
            this.nudAns.Location = new System.Drawing.Point(489, 316);
            this.nudAns.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudAns.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudAns.Name = "nudAns";
            this.nudAns.Size = new System.Drawing.Size(186, 31);
            this.nudAns.TabIndex = 8;
            // 
            // dbCalc
            // 
            this.dbCalc.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dbCalc.Location = new System.Drawing.Point(415, 359);
            this.dbCalc.Name = "dbCalc";
            this.dbCalc.Size = new System.Drawing.Size(112, 56);
            this.dbCalc.TabIndex = 9;
            this.dbCalc.Text = "計算";
            this.dbCalc.UseVisualStyleBackColor = true;
            this.dbCalc.Click += new System.EventHandler(this.dbCalc_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(681, 318);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 24);
            this.label6.TabIndex = 7;
            this.label6.Text = "・・・";
            // 
            // nudNotMuch
            // 
            this.nudNotMuch.BackColor = System.Drawing.Color.DimGray;
            this.nudNotMuch.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nudNotMuch.ForeColor = System.Drawing.Color.White;
            this.nudNotMuch.Location = new System.Drawing.Point(727, 316);
            this.nudNotMuch.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.nudNotMuch.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.nudNotMuch.Name = "nudNotMuch";
            this.nudNotMuch.Size = new System.Drawing.Size(186, 31);
            this.nudNotMuch.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(12, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 33);
            this.label7.TabIndex = 10;
            this.label7.Text = "割り算";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(939, 554);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dbCalc);
            this.Controls.Add(this.nudNotMuch);
            this.Controls.Add(this.nudAns);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nud2);
            this.Controls.Add(this.nud1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btChange);
            this.Controls.Add(this.tbNum2);
            this.Controls.Add(this.tbNum1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "距離換算アプリ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nud1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNotMuch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNum1;
        private System.Windows.Forms.TextBox tbNum2;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.NumericUpDown nud1;
        private System.Windows.Forms.NumericUpDown nud2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudAns;
        private System.Windows.Forms.Button dbCalc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudNotMuch;
        private System.Windows.Forms.Label label7;
    }
}

