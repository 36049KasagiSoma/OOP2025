namespace CarReportSystem {
    partial class Form1 {
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            dtpDate = new DateTimePicker();
            cbAuthor = new ComboBox();
            groupBox1 = new GroupBox();
            rbOther = new RadioButton();
            rbImport = new RadioButton();
            rbSubaru = new RadioButton();
            rbHonda = new RadioButton();
            rbNissan = new RadioButton();
            rbToyota = new RadioButton();
            dgvRecord = new DataGridView();
            cbCarName = new ComboBox();
            tbReport = new TextBox();
            pbPicture = new PictureBox();
            btPicOpen = new Button();
            dtPicDelete = new Button();
            btRecordAdd = new Button();
            btRecodeModify = new Button();
            dtRecodeDelete = new Button();
            ofdPicFileOpen = new OpenFileDialog();
            btNewRecord = new Button();
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            tsmiFileOpen = new ToolStripMenuItem();
            tsmiFileSave = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            tsmiColor = new ToolStripMenuItem();
            tsmiExit = new ToolStripMenuItem();
            ヘルプHToolStripMenuItem = new ToolStripMenuItem();
            tsmiAbout = new ToolStripMenuItem();
            cdColor = new ColorDialog();
            sfdReportFileSave = new SaveFileDialog();
            ofdReportFileOpen = new OpenFileDialog();
            tsslb = new ToolStripStatusLabel();
            ssMessageArea = new StatusStrip();
            tsslClock = new ToolStripStatusLabel();
            timer = new System.Windows.Forms.Timer(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecord).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            menuStrip1.SuspendLayout();
            ssMessageArea.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(53, 34);
            label1.Name = "label1";
            label1.Size = new Size(76, 30);
            label1.TabIndex = 0;
            label1.Text = "日付：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(32, 64);
            label2.Name = "label2";
            label2.Size = new Size(97, 30);
            label2.TabIndex = 0;
            label2.Text = "記録者：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label3.Location = new Point(37, 94);
            label3.Name = "label3";
            label3.Size = new Size(92, 30);
            label3.TabIndex = 0;
            label3.Text = "メーカー：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label4.Location = new Point(53, 124);
            label4.Name = "label4";
            label4.Size = new Size(76, 30);
            label4.TabIndex = 0;
            label4.Text = "車名：";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label5.Location = new Point(34, 163);
            label5.Name = "label5";
            label5.Size = new Size(95, 30);
            label5.TabIndex = 1;
            label5.Text = "レポート：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label6.Location = new Point(11, 369);
            label6.Name = "label6";
            label6.Size = new Size(118, 30);
            label6.TabIndex = 2;
            label6.Text = "記事一覧：";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label7.Location = new Point(502, 34);
            label7.Name = "label7";
            label7.Size = new Size(76, 30);
            label7.TabIndex = 3;
            label7.Text = "画像：";
            // 
            // dtpDate
            // 
            dtpDate.CalendarFont = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpDate.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            dtpDate.Location = new Point(135, 37);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(221, 27);
            dtpDate.TabIndex = 4;
            // 
            // cbAuthor
            // 
            cbAuthor.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbAuthor.FormattingEnabled = true;
            cbAuthor.Location = new Point(135, 66);
            cbAuthor.Name = "cbAuthor";
            cbAuthor.Size = new Size(221, 28);
            cbAuthor.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rbOther);
            groupBox1.Controls.Add(rbImport);
            groupBox1.Controls.Add(rbSubaru);
            groupBox1.Controls.Add(rbHonda);
            groupBox1.Controls.Add(rbNissan);
            groupBox1.Controls.Add(rbToyota);
            groupBox1.Location = new Point(135, 94);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(351, 30);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            // 
            // rbOther
            // 
            rbOther.AutoSize = true;
            rbOther.Location = new Point(292, 7);
            rbOther.Name = "rbOther";
            rbOther.Size = new Size(56, 19);
            rbOther.TabIndex = 5;
            rbOther.TabStop = true;
            rbOther.Text = "その他";
            rbOther.UseVisualStyleBackColor = true;
            // 
            // rbImport
            // 
            rbImport.AutoSize = true;
            rbImport.Location = new Point(227, 7);
            rbImport.Name = "rbImport";
            rbImport.Size = new Size(61, 19);
            rbImport.TabIndex = 4;
            rbImport.TabStop = true;
            rbImport.Text = "輸入車";
            rbImport.UseVisualStyleBackColor = true;
            // 
            // rbSubaru
            // 
            rbSubaru.AutoSize = true;
            rbSubaru.Location = new Point(167, 7);
            rbSubaru.Name = "rbSubaru";
            rbSubaru.Size = new Size(54, 19);
            rbSubaru.TabIndex = 3;
            rbSubaru.TabStop = true;
            rbSubaru.Text = "スバル";
            rbSubaru.UseVisualStyleBackColor = true;
            // 
            // rbHonda
            // 
            rbHonda.AutoSize = true;
            rbHonda.Location = new Point(108, 7);
            rbHonda.Name = "rbHonda";
            rbHonda.Size = new Size(53, 19);
            rbHonda.TabIndex = 2;
            rbHonda.TabStop = true;
            rbHonda.Text = "ホンダ";
            rbHonda.UseVisualStyleBackColor = true;
            // 
            // rbNissan
            // 
            rbNissan.AutoSize = true;
            rbNissan.Location = new Point(53, 7);
            rbNissan.Name = "rbNissan";
            rbNissan.Size = new Size(49, 19);
            rbNissan.TabIndex = 1;
            rbNissan.TabStop = true;
            rbNissan.Text = "日産";
            rbNissan.UseVisualStyleBackColor = true;
            // 
            // rbToyota
            // 
            rbToyota.AutoSize = true;
            rbToyota.Location = new Point(1, 7);
            rbToyota.Name = "rbToyota";
            rbToyota.Size = new Size(50, 19);
            rbToyota.TabIndex = 0;
            rbToyota.TabStop = true;
            rbToyota.Text = "トヨタ";
            rbToyota.UseVisualStyleBackColor = true;
            // 
            // dgvRecord
            // 
            dgvRecord.AllowUserToAddRows = false;
            dgvRecord.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.Silver;
            dgvRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvRecord.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Yu Gothic UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSkyBlue;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvRecord.DefaultCellStyle = dataGridViewCellStyle2;
            dgvRecord.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvRecord.Location = new Point(136, 369);
            dgvRecord.MultiSelect = false;
            dgvRecord.Name = "dgvRecord";
            dgvRecord.ReadOnly = true;
            dgvRecord.ScrollBars = ScrollBars.None;
            dgvRecord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRecord.Size = new Size(652, 139);
            dgvRecord.TabIndex = 7;
            dgvRecord.DoubleClick += dgvRecord_Click;
            // 
            // cbCarName
            // 
            cbCarName.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbCarName.Location = new Point(135, 125);
            cbCarName.Name = "cbCarName";
            cbCarName.Size = new Size(288, 33);
            cbCarName.TabIndex = 8;
            // 
            // tbReport
            // 
            tbReport.Location = new Point(135, 163);
            tbReport.Multiline = true;
            tbReport.Name = "tbReport";
            tbReport.Size = new Size(348, 155);
            tbReport.TabIndex = 9;
            // 
            // pbPicture
            // 
            pbPicture.BorderStyle = BorderStyle.FixedSingle;
            pbPicture.Location = new Point(502, 67);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(285, 213);
            pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbPicture.TabIndex = 10;
            pbPicture.TabStop = false;
            // 
            // btPicOpen
            // 
            btPicOpen.Font = new Font("Yu Gothic UI", 9.75F);
            btPicOpen.Location = new Point(584, 38);
            btPicOpen.Name = "btPicOpen";
            btPicOpen.Size = new Size(75, 27);
            btPicOpen.TabIndex = 11;
            btPicOpen.Text = "開く...";
            btPicOpen.UseVisualStyleBackColor = true;
            btPicOpen.Click += btPicOpen_Click;
            // 
            // dtPicDelete
            // 
            dtPicDelete.Font = new Font("Yu Gothic UI", 9.75F);
            dtPicDelete.Location = new Point(712, 37);
            dtPicDelete.Name = "dtPicDelete";
            dtPicDelete.Size = new Size(75, 27);
            dtPicDelete.TabIndex = 11;
            dtPicDelete.Text = "削除";
            dtPicDelete.UseVisualStyleBackColor = true;
            dtPicDelete.Click += dtPicDelete_Click;
            // 
            // btRecordAdd
            // 
            btRecordAdd.Font = new Font("Yu Gothic UI", 9.75F);
            btRecordAdd.Location = new Point(550, 336);
            btRecordAdd.Name = "btRecordAdd";
            btRecordAdd.Size = new Size(75, 27);
            btRecordAdd.TabIndex = 11;
            btRecordAdd.Text = "追加";
            btRecordAdd.UseVisualStyleBackColor = true;
            btRecordAdd.Click += btRecordAdd_Click;
            // 
            // btRecodeModify
            // 
            btRecodeModify.Font = new Font("Yu Gothic UI", 9.75F);
            btRecodeModify.Location = new Point(631, 336);
            btRecodeModify.Name = "btRecodeModify";
            btRecodeModify.Size = new Size(75, 27);
            btRecodeModify.TabIndex = 11;
            btRecodeModify.Text = "修正";
            btRecodeModify.UseVisualStyleBackColor = true;
            btRecodeModify.Click += btRecodeModify_Click;
            // 
            // dtRecodeDelete
            // 
            dtRecodeDelete.BackColor = Color.White;
            dtRecodeDelete.Font = new Font("Yu Gothic UI", 9.75F);
            dtRecodeDelete.ForeColor = Color.Black;
            dtRecodeDelete.Location = new Point(712, 336);
            dtRecodeDelete.Name = "dtRecodeDelete";
            dtRecodeDelete.Size = new Size(75, 27);
            dtRecodeDelete.TabIndex = 11;
            dtRecodeDelete.Text = "削除";
            dtRecodeDelete.UseVisualStyleBackColor = false;
            dtRecodeDelete.Click += dtRecodeDelete_Click;
            // 
            // ofdPicFileOpen
            // 
            ofdPicFileOpen.Filter = "画像ファイル|*.png;*.jpg;*.jpeg;|すべてのファイル|*.*";
            // 
            // btNewRecord
            // 
            btNewRecord.Location = new Point(409, 34);
            btNewRecord.Name = "btNewRecord";
            btNewRecord.Size = new Size(77, 33);
            btNewRecord.TabIndex = 12;
            btNewRecord.Text = "新規入力";
            btNewRecord.UseVisualStyleBackColor = true;
            btNewRecord.Click += btNewRecord_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem, ヘルプHToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiFileOpen, tsmiFileSave, toolStripSeparator1, tsmiColor, tsmiExit });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(67, 20);
            ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // tsmiFileOpen
            // 
            tsmiFileOpen.Name = "tsmiFileOpen";
            tsmiFileOpen.Size = new Size(140, 22);
            tsmiFileOpen.Text = "開く...";
            tsmiFileOpen.Click += tsmiFileOpen_Click;
            // 
            // tsmiFileSave
            // 
            tsmiFileSave.Name = "tsmiFileSave";
            tsmiFileSave.Size = new Size(140, 22);
            tsmiFileSave.Text = "保存...";
            tsmiFileSave.Click += tsmiFileSave_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(137, 6);
            // 
            // tsmiColor
            // 
            tsmiColor.Name = "tsmiColor";
            tsmiColor.Size = new Size(140, 22);
            tsmiColor.Text = "色設定...";
            tsmiColor.Click += tsmiColor_Click;
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.ShortcutKeys = Keys.Alt | Keys.F4;
            tsmiExit.Size = new Size(140, 22);
            tsmiExit.Text = "終了";
            tsmiExit.Click += tsmiExit_Click;
            // 
            // ヘルプHToolStripMenuItem
            // 
            ヘルプHToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiAbout });
            ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            ヘルプHToolStripMenuItem.Size = new Size(65, 20);
            ヘルプHToolStripMenuItem.Text = "ヘルプ(&H)";
            // 
            // tsmiAbout
            // 
            tsmiAbout.Name = "tsmiAbout";
            tsmiAbout.Size = new Size(164, 22);
            tsmiAbout.Text = "このアプリについて...";
            tsmiAbout.Click += tsmiAbout_Click;
            // 
            // ofdReportFileOpen
            // 
            ofdReportFileOpen.FileName = "openFileDialog1";
            // 
            // tsslb
            // 
            tsslb.Name = "tsslb";
            tsslb.Size = new Size(0, 17);
            // 
            // ssMessageArea
            // 
            ssMessageArea.Items.AddRange(new ToolStripItem[] { tsslb, tsslClock });
            ssMessageArea.Location = new Point(0, 534);
            ssMessageArea.Name = "ssMessageArea";
            ssMessageArea.Size = new Size(800, 22);
            ssMessageArea.TabIndex = 13;
            ssMessageArea.Text = "statusStrip1";
            // 
            // tsslClock
            // 
            tsslClock.Name = "tsslClock";
            tsslClock.Size = new Size(785, 17);
            tsslClock.Spring = true;
            tsslClock.Text = "時計";
            tsslClock.TextAlign = ContentAlignment.MiddleRight;
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 556);
            Controls.Add(ssMessageArea);
            Controls.Add(menuStrip1);
            Controls.Add(btNewRecord);
            Controls.Add(dtPicDelete);
            Controls.Add(dtRecodeDelete);
            Controls.Add(btRecodeModify);
            Controls.Add(btRecordAdd);
            Controls.Add(btPicOpen);
            Controls.Add(pbPicture);
            Controls.Add(tbReport);
            Controls.Add(cbCarName);
            Controls.Add(dgvRecord);
            Controls.Add(groupBox1);
            Controls.Add(cbAuthor);
            Controls.Add(dtpDate);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "試乗レポート管理システム";
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRecord).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ssMessageArea.ResumeLayout(false);
            ssMessageArea.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private DateTimePicker dtpDate;
        private ComboBox cbAuthor;
        private GroupBox groupBox1;
        private RadioButton rbOther;
        private RadioButton rbImport;
        private RadioButton rbSubaru;
        private RadioButton rbHonda;
        private RadioButton rbNissan;
        private RadioButton rbToyota;
        private DataGridView dgvRecord;
        private ComboBox cbCarName;
        private TextBox tbReport;
        private PictureBox pbPicture;
        private Button btPicOpen;
        private Button dtPicDelete;
        private Button btRecordAdd;
        private Button btRecodeModify;
        private Button dtRecodeDelete;
        private OpenFileDialog ofdPicFileOpen;
        private Button btNewRecord;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem ヘルプHToolStripMenuItem;
        private ToolStripMenuItem tsmiFileOpen;
        private ToolStripMenuItem tsmiFileSave;
        private ToolStripMenuItem tsmiColor;
        private ToolStripMenuItem tsmiExit;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem tsmiAbout;
        private ColorDialog cdColor;
        private SaveFileDialog sfdReportFileSave;
        private OpenFileDialog ofdReportFileOpen;
        private ToolStripStatusLabel tsslb;
        private StatusStrip ssMessageArea;
        private ToolStripStatusLabel tsslClock;
        private System.Windows.Forms.Timer timer;
    }
}
