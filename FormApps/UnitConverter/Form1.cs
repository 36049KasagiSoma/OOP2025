using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitConverter {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            tbNum1.Select();
        }

        private void Conversion_KeyPress(object sender, KeyPressEventArgs e) {
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b') {
                e.Handled = true;
            }
        }

        private void btChange_Click(object sender, EventArgs e) {
            int inVal = 0;
            int.TryParse(tbNum1.Text, out inVal);
            double outVal = inVal;
            tbNum2.Text = comboBox1.Text;
            switch (comboBox1.Text + "," + comboBox2.Text) {
                case ("m,in"):
                    outVal = MerterConverter.MeterToInch(inVal);
                    break;
                case ("in,m"):
                    outVal = MerterConverter.InchToMeter(inVal);
                    break;
                case ("m,yd"):
                    outVal = MerterConverter.MeterToYard(inVal);
                    break;
                case ("yd,m"):
                    outVal = MerterConverter.YardToMeter(inVal);
                    break;
                case ("yd,in"):
                    outVal = MerterConverter.YardToInch(inVal);
                    break;
                case ("in,yd"):
                    outVal = MerterConverter.InchToYard(inVal);
                    break;
            }
            tbNum2.Text = outVal.ToString();
            tbNum2.Select();
        }

        private void dbCalc_Click(object sender, EventArgs e) {
            if (nud2.Value == 0) {
                MessageBox.Show("0で除算はできません。", "ゼロ除算エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                nud2.Value = 1;
            }
            nudAns.Value = Math.Floor(nud1.Value / nud2.Value);
            nudNotMuch.Value = nud1.Value % nud2.Value;
        }
    }
}
