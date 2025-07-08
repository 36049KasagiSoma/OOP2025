using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;

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

        private void Form1_Load(object sender, EventArgs e) {

        }

        // �摜�J���{�^������
        private void btPicOpen_Click(object sender, EventArgs e) {
            if (ofdPicFileOpen.ShowDialog(this) == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        // �摜�폜�{�^������
        private void dtPicDelete_Click(object sender, EventArgs e) {
            if (MessageBox.Show("�摜���폜���܂���?", "�m�F",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                pbPicture.Image = null;
            }
        }

        // �ǉ��{�^������
        private void btRecordAdd_Click(object sender, EventArgs e) {
            addItem();
            ClearForm();
        }

        // ���R�[�h�N���b�N������
        private void dgvRecord_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) return;
            if (checkInStatus() &&
               MessageBox.Show("�ݒ�ς݂̍��ڂ�����܂��B�㏑�����܂���?", "�m�F",
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

        // �V�K���̓{�^������
        private void btNewRecord_Click(object sender, EventArgs e) {
            if (checkInStatus() &&
                MessageBox.Show("�ݒ�ς݂̍��ڂ�����܂��B�폜���܂���?", "�m�F",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                ClearForm();
            }
        }

        // �C���{�^������
        private void btRecodeModify_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) {
                MessageBox.Show("�Ώۃ��R�[�h���I������Ă��܂���B", "���R�[�h�I���G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("�I�����ꂽ���ڂ��X�V���܂���?", "�m�F",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                setItem(dgvRecord.CurrentRow.Index);
                dgvRecord.Refresh();
                ClearForm();
            }
        }

        // �폜�{�^������
        private void dtRecodeDelete_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) {
                MessageBox.Show("�Ώۃ��R�[�h���I������Ă��܂���B", "���R�[�h�I���G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("�I�����ꂽ���ڂ��폜���܂���?", "�m�F",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                dgvRecord.Refresh();
                deleteItem(dgvRecord.CurrentRow.Index);
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmiAbout_Click(object sender, EventArgs e) {
            new Version().ShowDialog();
        }

        private void addItem() {
            if (!isCheckStatus()) return;
            var carRecode = createCarRecode();

            listCarRecords.Add(carRecode);

            addCbItem(cbAuthor, carRecode.Author);
            addCbItem(cbCarName, carRecode.CarName);
        }

        private void setItem(int index) {
            if (!isCheckStatus()) return;
            if (index < 0 || index >= listCarRecords.Count) {
                Console.Error.WriteLine($"�w�肳�ꂽindex�l���s���ł��Bindex={index}");
            }
            var carRecode = createCarRecode();

            listCarRecords[index] = carRecode;

            addCbItem(cbAuthor, carRecode.Author);
            addCbItem(cbCarName, carRecode.CarName);
        }

        private bool isCheckStatus() {
            if (string.IsNullOrEmpty(cbCarName.Text)) {
                tsslb.Text = "�Ԗ������͂���Ă��܂���B";
                cbCarName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(cbAuthor.Text)) {
                tsslb.Text = "�L�^�҂����͂���Ă��܂���B";
                cbAuthor.Focus();
                return false;
            }
            tsslb.Text = "";
            return true;
        }

        private void deleteItem(int index) {
            if (index < 0 || index >= listCarRecords.Count) {
                Console.Error.WriteLine($"�w�肳�ꂽindex�l���s���ł��Bindex={index}");
            }

            listCarRecords.RemoveAt(index);
        }

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

        private void addCbItem(ComboBox comboBox, string item) {
            if (string.IsNullOrEmpty(item)) return;
            if (!comboBox.Items.Contains(item)) {
                comboBox.Items.Add(item);
            }
        }

        private void ClearForm() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = string.Empty;
            cbCarName.Text = string.Empty;
            tbReport.Text = string.Empty;

            //���I�����
            rbToyota.Checked = false;
            rbNissan.Checked = false;
            rbHonda.Checked = false;
            rbSubaru.Checked = false;
            rbImport.Checked = false;
            rbOther.Checked = false;

            pbPicture.Image = null;

        }

        private MakerGroup GetMaker() {
            if (rbToyota.Checked) return MakerGroup.TOYOTA;
            if (rbNissan.Checked) return MakerGroup.NISSAN;
            if (rbHonda.Checked) return MakerGroup.HONDA;
            if (rbSubaru.Checked) return MakerGroup.SUBARU;
            if (rbImport.Checked) return MakerGroup.IMPORT;
            if (rbOther.Checked) return MakerGroup.OTHER;
            return MakerGroup.NONE;
        }

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

        private void tsmiColor_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                this.BackColor = cdColor.Color;
            }

        }

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
                    tsslb.Text = "�t�@�C�������o���G���[�F" + e.Message;
                }
            }
        }

        private BindingList<CarRecord> reportOpen() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    using (FileStream fs = File.Open(ofdReportFileOpen.FileName, FileMode.Open,FileAccess.Read)) {
#pragma warning disable SYSLIB0011
                        var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                        Object o = bf.Deserialize(fs);
                        if (o as BindingList<CarRecord> != null) {
                            return (BindingList<CarRecord>)o;
                        }
                    }
                } catch (Exception e) {
                    tsslb.Text = "�t�@�C���ǂݍ��݃G���[�F" + e.Message;
                }
            }
            return new BindingList<CarRecord>();
        }

        private void tsmiFileSave_Click(object sender, EventArgs e) {
            reportSave();
        }

        private void tsmiFileOpen_Click(object sender, EventArgs e) {
            listCarRecords = reportOpen();
            dgvRecord.DataSource = listCarRecords;
            dgvRecord.Refresh();
        }
    }
}
