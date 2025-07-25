using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RssReader {
    public partial class InputDialog : Form {
        public string Input = "";
        private List<string> items;
        public InputDialog(string url, List<string> items   ) {
            InitializeComponent();
            lbUrl.Text = url;
            this.items = items;
            this.BackColor = SettingData.GetInstance().GetBackColor()[2];
            foreach (Label label in StaticEvent.GetAllLabels(this)) {
                label.ForeColor = SettingData.GetInstance().GetBackColor()[3];
            }
        }

        private void btOk_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(tbInput.Text)) {
                MessageBox.Show("お気に入り名の項目は必須です。",
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbInput.Focus();
                return;
            }
            if (items.Contains(tbInput.Text)) {
                MessageBox.Show("この名前は既に使用されています。",
                    "重複エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbInput.Focus();
                return;
            }
            Input = tbInput.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e) {

        }
    }
}
