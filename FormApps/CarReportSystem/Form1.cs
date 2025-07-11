using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarReportSystem {
    public partial class Form1 : Form {
        BindingList<CarRecord> listCarRecords = new BindingList<CarRecord>();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarRecords;
            DataGridViewColumn column = dgvRecord.Columns["Picture"];
            if (column is DataGridViewImageColumn imageColumn) {
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        // 設定ファイルの場所
        private const string SETTING_FILE_PATH = "setting.xml";

        // フォーム開始時処理
        private void Form1_Load(object sender, EventArgs e) {
            var sb = new StringBuilder();
            using (var reader = XmlReader.Create(SETTING_FILE_PATH)) {
                var ser = new XmlSerializer(Settings.GetInstans().GetType());
                try {
                    int cVal = ((ser.Deserialize(reader) as Settings) ?? Settings.GetInstans()).MainFormBackColor;
                    BackColor = Color.FromArgb(cVal);
                    Settings.GetInstans().MainFormBackColor = cVal;
                } catch (Exception e2) {
                    Console.Error.WriteLine("ファイル読み込みでエラー：" + e2.Message);
                }
            }
        }
        
        // フォーム終了時処理
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            try {
                using (FileStream fs = File.Open(SETTING_FILE_PATH, FileMode.Create)) {
                    var ser = new XmlSerializer(Settings.GetInstans().GetType());
                    ser.Serialize(fs, Settings.GetInstans());
                }
            } catch (Exception e2) {
                Console.Error.WriteLine("設定ファイルの書き出しに失敗：" + e2.Message);
            }

        }

        // 画像開くボタン処理
        private void btPicOpen_Click(object sender, EventArgs e) {
            if (ofdPicFileOpen.ShowDialog(this) == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        // 画像削除ボタン処理
        private void dtPicDelete_Click(object sender, EventArgs e) {
            if (MessageBox.Show("画像を削除しますか?", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                pbPicture.Image = null;
            }
        }

        // 追加ボタン処理
        private void btRecordAdd_Click(object sender, EventArgs e) {
            addItem();
            ClearForm();
        }

        // レコードクリック時処理
        private void dgvRecord_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) return;
            if (checkInStatus() &&
               MessageBox.Show("設定済みの項目があります。上書きしますか?", "確認",
               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }
            try {
                dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
            } catch (ArgumentOutOfRangeException) {
                return;
            }

            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            SetMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
        }

        // 新規入力ボタン処理
        private void btNewRecord_Click(object sender, EventArgs e) {
            if (checkInStatus() &&
                MessageBox.Show("設定済みの項目があります。削除しますか?", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                ClearForm();
            }
        }

        // 修正ボタン処理
        private void btRecodeModify_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) {
                MessageBox.Show("対象レコードが選択されていません。", "レコード選択エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("選択された項目を更新しますか?", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                setItem(dgvRecord.CurrentRow.Index);
                dgvRecord.Refresh();
                ClearForm();
            }
        }

        // 削除ボタン処理
        private void dtRecodeDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) {
                MessageBox.Show("対象レコードが選択されていません。", "レコード選択エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("選択された項目を削除しますか?", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                dgvRecord.Refresh();
                deleteItem(dgvRecord.CurrentRow.Index);
            }
        }

        // メニューバー【終了】処理
        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        // メニューバー【このアプリ】処理
        private void tsmiAbout_Click(object sender, EventArgs e) {
            new Version().ShowDialog();
        }

        // メニューバー【色設定】処理
        private void tsmiColor_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                this.BackColor = cdColor.Color;
                Settings.GetInstans().MainFormBackColor = cdColor.Color.ToArgb();
            }

        }

        // メニューバー【保存】処理
        private void tsmiFileSave_Click(object sender, EventArgs e) {
            reportSave();
        }

        // メニューバー【開く】処理
        private void tsmiFileOpen_Click(object sender, EventArgs e) {
            listCarRecords = reportOpen();
            dgvRecord.DataSource = listCarRecords;
            dgvRecord.Refresh();
            foreach (var record in listCarRecords) {
                addCbItem(cbAuthor, record.Author);
                addCbItem(cbCarName, record.CarName);
            }
        }

        // レポート新規追加
        private void addItem() {
            if (!checkStatus()) return;
            var carRecode = createCarRecode();

            listCarRecords.Add(carRecode);

            addCbItem(cbAuthor, carRecode.Author);
            addCbItem(cbCarName, carRecode.CarName);
        }

        // レポート上書き
        private void setItem(int index) {
            if (!checkStatus()) return;
            if (index < 0 || index >= listCarRecords.Count) {
                Console.Error.WriteLine($"指定されたindex値が不正です。index={index}");
            }
            var carRecode = createCarRecode();

            listCarRecords[index] = carRecode;

            addCbItem(cbAuthor, carRecode.Author);
            addCbItem(cbCarName, carRecode.CarName);
        }
        
        // レポート削除
        private void deleteItem(int index) {
            if (index < 0 || index >= listCarRecords.Count) {
                Console.Error.WriteLine($"指定されたindex値が不正です。index={index}");
            }

            listCarRecords.RemoveAt(index);
        }

        // 必須項目入力チェック
        private bool checkStatus() {
            if (string.IsNullOrEmpty(cbCarName.Text)) {
                tsslb.Text = "車名が入力されていません。";
                cbCarName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbAuthor.Text)) {
                tsslb.Text = "記録者が入力されていません。";
                cbAuthor.Focus();
                return false;
            }
            tsslb.Text = "";
            return true;
        }

        // 入力データからのCarRecord生成
        private CarRecord createCarRecode() {
            return new CarRecord {
                Date = dtpDate.Value.Date,
                Author = cbAuthor.Text,
                Maker = GetMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
            };
        }

        // コンボボックスにアイテム追加（重複なし）
        private void addCbItem(ComboBox comboBox, string item) {
            if (string.IsNullOrEmpty(item)) return;
            if (!comboBox.Items.Contains(item)) {
                comboBox.Items.Add(item);
            }
        }

        // 項目のクリア
        private void ClearForm() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = string.Empty;
            cbCarName.Text = string.Empty;
            tbReport.Text = string.Empty;

            //未選択状態
            rbToyota.Checked = false;
            rbNissan.Checked = false;
            rbHonda.Checked = false;
            rbSubaru.Checked = false;
            rbImport.Checked = false;
            rbOther.Checked = false;

            pbPicture.Image = null;

        }

        // 項目からのメーカー取得
        private MakerGroup GetMaker() {
            if (rbToyota.Checked) return MakerGroup.TOYOTA;
            if (rbNissan.Checked) return MakerGroup.NISSAN;
            if (rbHonda.Checked) return MakerGroup.HONDA;
            if (rbSubaru.Checked) return MakerGroup.SUBARU;
            if (rbImport.Checked) return MakerGroup.IMPORT;
            if (rbOther.Checked) return MakerGroup.OTHER;
            return MakerGroup.NONE;
        }

        // 項目にメーカーセット
        private void SetMaker(MakerGroup maker) {
            switch (maker) {
                case MakerGroup.NONE:
                default:
                    rbToyota.Checked = false;
                    rbNissan.Checked = false;
                    rbHonda.Checked = false;
                    rbSubaru.Checked = false;
                    rbImport.Checked = false;
                    rbOther.Checked = false;
                    break;
                case MakerGroup.TOYOTA:
                    rbToyota.Checked = true;
                    break;
                case MakerGroup.NISSAN:
                    rbNissan.Checked = true;
                    break;
                case MakerGroup.HONDA:
                    rbHonda.Checked = true;
                    break;
                case MakerGroup.SUBARU:
                    rbSubaru.Checked = true;
                    break;
                case MakerGroup.IMPORT:
                    rbImport.Checked = true;
                    break;
                case MakerGroup.OTHER:
                    rbOther.Checked = true;
                    break;

            }
        }
        
        // 項目にデータが１つでも入力されているか
        private bool checkInStatus() {
            if (!cbAuthor.Text.Equals(string.Empty)) return true;
            if (!cbCarName.Text.Equals(string.Empty)) return true;
            if (!tbReport.Text.Equals(string.Empty)) return true;

            if (rbToyota.Checked) return true;
            if (rbNissan.Checked) return true;
            if (rbHonda.Checked) return true;
            if (rbSubaru.Checked) return true;
            if (rbImport.Checked) return true;
            if (rbOther.Checked) return true;

            if (pbPicture.Image != null) return true;

            return false;
        }

        // レポートのファイル出力
        private void reportSave() {
            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    using (FileStream fs = File.Open(sfdReportFileSave.FileName, FileMode.Create)) {
#pragma warning disable SYSLIB0011
                        var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                        bf.Serialize(fs, listCarRecords);
                    }
                } catch (Exception e) {
                    tsslb.Text = "ファイル書き出しエラー：" + e.Message;
                }
            }
        }

        // レポートファイルの読込
        private BindingList<CarRecord> reportOpen() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    using (FileStream fs = File.Open(ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {
#pragma warning disable SYSLIB0011
                        var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                        Object o = bf.Deserialize(fs);
                        if (o as BindingList<CarRecord> != null) {
                            return (BindingList<CarRecord>)o;
                        }
                    }
                } catch (Exception e) {
                    tsslb.Text = "ファイル読み込みエラー：" + e.Message;
                }
            }
            return new BindingList<CarRecord>();
        }

    }
}
