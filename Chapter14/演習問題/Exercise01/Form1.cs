using System.Threading.Tasks;

namespace Exercise01 {
    public partial class テキスト読み込み : Form {
        public テキスト読み込み() {
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
            tssl.Text = "読み込み中...";
            var sr = new StreamReader(textBox2.Text);
            var text = await sr.ReadToEndAsync();
            textBox1.Text = text;
            tssl.Text = "";
            dispStatus(text.Length);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                readFile();
            }
        }

        private void dispStatus(int count) {
            sttl2.Text = $"{count}文字";
        }
    }
}
