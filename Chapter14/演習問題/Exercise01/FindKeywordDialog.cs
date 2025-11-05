using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise01 {
    public partial class FindKeywordDiarog : Form {
        IFindEvent? findEvent;

        public FindKeywordDiarog(IFindEvent? events, string keyword ="") {
            InitializeComponent();
            this.findEvent = events;
            textBox1.Text = keyword;
        }

        public void button1_Click(object sender, EventArgs e) {
            findEvent?.FindNext(textBox1.Text);
            UpdateCount(findEvent?.text ?? "");
            textBox1.Focus();
        }
        public void button2_Click(object sender, EventArgs e) {
            findEvent?.FindPrevious(textBox1.Text);
            UpdateCount(findEvent?.text ?? "");
            textBox1.Focus();
        }
        public void button3_Click(object sender, EventArgs e) {
            this.Close();
        }

        public void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                findEvent?.FindNext(textBox1.Text);
                UpdateCount(findEvent?.text ?? "");
                textBox1.Focus();
            }
        }

        public void UpdateCount(string text) {
            int cnt = Regex.Matches(text, textBox1.Text).Count;
            label1.Text = $"検索結果:{cnt}件";
        }

        public interface IFindEvent {
            void FindNext(string keyword);
            void FindPrevious(string keyword);
            string text { get; set; }
        }
    }
}
