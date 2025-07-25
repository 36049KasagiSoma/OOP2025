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
    public partial class SettingDialog : Form {
        public SettingDialog() {
            InitializeComponent();
            this.BackColor = SettingData.GetInstance().GetBackColor()[2];

            foreach (Label label in StaticEvent.GetAllLabels(this)) {
                label.ForeColor = SettingData.GetInstance().GetBackColor()[3];
            }

        }


        private void SettingDialog_Load(object sender, EventArgs e) {
            Opanel.BackColor = SettingData.GetInstance().GetBackColor()[0];
            Epanel.BackColor = SettingData.GetInstance().GetBackColor()[1];
            Bpanel.BackColor = SettingData.GetInstance().GetBackColor()[2];
            Tpanel.BackColor = SettingData.GetInstance().GetBackColor()[3];
            timeoutValue.Value = SettingData.GetInstance().GetTimeOutValue();
        }

        private void Obutton_Click(object sender, EventArgs e) {
            colorDialog.Color = Opanel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Opanel.BackColor = colorDialog.Color;
            }
        }

        private void Ebutton_Click(object sender, EventArgs e) {
            colorDialog.Color = Epanel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Epanel.BackColor = colorDialog.Color;
            }
        }

        private void Bbutton_Click(object sender, EventArgs e) {
            colorDialog.Color = Bpanel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Bpanel.BackColor = colorDialog.Color;
            }
        }
        private void Tbutton_Click(object sender, EventArgs e) {
            colorDialog.Color = Tpanel.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                Tpanel.BackColor = colorDialog.Color;
            }

        }

        private void button1_Click(object sender, EventArgs e) {
            Sdata data = new Sdata();
            data.OddBackColor = Opanel.BackColor.ToArgb();
            data.EvenBackColor = Epanel.BackColor.ToArgb();
            data.BackBackColor = Bpanel.BackColor.ToArgb();
            data.TextBackColor = Tpanel.BackColor.ToArgb();
            data.TimeOutValue = (int)timeoutValue.Value;
            SettingData.GetInstance().SaveItem(data);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            Sdata tmp = new Sdata();
            Opanel.BackColor = Color.FromArgb(tmp.OddBackColor);
            Epanel.BackColor = Color.FromArgb(tmp.EvenBackColor);
            Bpanel.BackColor = Color.FromArgb(tmp.BackBackColor);
            timeoutValue.Value = tmp.TimeOutValue;
        }


    }
}
