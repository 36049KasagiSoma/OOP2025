using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace StopWatch {
    public partial class Form1 : Form {
        Stopwatch sw = new Stopwatch();
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            lbTimeDisp.Text = "00:00:00.00";
            tmDispTimer.Interval = 15;
        }

        private void bdStart_Click(object sender, EventArgs e) {
            sw.Start();
            tmDispTimer.Start();
        }

        private void bdStop_Click(object sender, EventArgs e) {
            sw.Stop();
            lbTimeDisp.Text = sw.Elapsed.ToString(@"hh\:mm\:ss\.ff");
        }

        private void bdReset_Click(object sender, EventArgs e) {
            sw.Reset();
            lbTimeDisp.Text = sw.Elapsed.ToString(@"hh\:mm\:ss\.ff"); 
            listBox1.Items.Clear();
        }

        private void tmDispTimer_Tick(object sender, EventArgs e) {
            lbTimeDisp.Text = sw.Elapsed.ToString(@"hh\:mm\:ss\.ff");
        }

        private void bdRap_Click(object sender, EventArgs e) {
            listBox1.Items.Insert(0, sw.Elapsed.ToString(@"hh\:mm\:ss\.ff"));
            listBox1.SelectedIndex = 0;
        }
    }
}
