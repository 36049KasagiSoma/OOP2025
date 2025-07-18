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
        public InputDialog(string url) {
            InitializeComponent();
            lbUrl.Text = url;
        }

        private void btOk_Click(object sender, EventArgs e) {
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
