using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Exercise01 {
    public partial class ReadingTextLines : Form {
        public ReadingTextLines() {
            InitializeComponent();
            dispStatus(0);
        }

        private void button1_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                textBox2.Text = openFileDialog1.FileName;
                readFile();
            }
        }

        private async void readFile() {
            tssl.Text = "ì«Ç›çûÇ›íÜ...";
            StringBuilder sb=new StringBuilder();
            using (var sr = new StreamReader(textBox2.Text)) {
                while(!sr.EndOfStream) {
                    var line = await sr.ReadLineAsync();
                    sb.AppendLine(line);
                }
            }
            textBox1.Text = sb.ToString();
            tssl.Text = "";
            dispStatus(sb.ToString().Length);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                readFile();
            }
        }

        private void dispStatus(int count) {
            sttl2.Text = $"{count}ï∂éö";
        }

        private FindKeywordDiarog? findDialog = null;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Control | Keys.F:// Ctrl + F
                    if (findDialog == null || findDialog.IsDisposed) {
                        if (textBox1.SelectionLength > 0) {
                            string selectedText = textBox1.Text.Substring(textBox1.SelectionStart, textBox1.SelectionLength);
                            findDialog = new FindKeywordDiarog(new FindKeyword(textBox1), selectedText);
                        } else {
                            findDialog = new FindKeywordDiarog(new FindKeyword(textBox1));
                        }
                        findDialog.Show(this);
                    } else {
                        findDialog.Activate();
                        findDialog.Focus();
                    }
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private class FindKeyword : FindKeywordDiarog.IFindEvent {
            private TextBox tb;
            public string text {
                get => tb.Text;
                set => tb.Text = value;
            }
            public FindKeyword(TextBox textbox) {
                this.tb = textbox;
            }
            public void FindNext(string keyword) {
                int start = tb.SelectionStart + tb.SelectionLength;
                int index = tb.Text.IndexOf(keyword, start, StringComparison.OrdinalIgnoreCase);
                if (index >= 0) {
                    tb.Select(index, keyword.Length);
                    tb.ScrollToCaret();
                    tb.Focus();
                } else {
                    MessageBox.Show("å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB", "åüçı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            public void FindPrevious(string keyword) {
                int start = tb.SelectionStart - 1;
                if (start < 0) {
                    MessageBox.Show("å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB", "åüçı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int index = tb.Text.LastIndexOf(keyword, start, StringComparison.OrdinalIgnoreCase);
                if (index >= 0) {
                    tb.Select(index, keyword.Length);
                    tb.ScrollToCaret();
                    tb.Focus();
                } else {
                    MessageBox.Show("å©Ç¬Ç©ÇËÇ‹ÇπÇÒÇ≈ÇµÇΩÅB", "åüçı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
