using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RssReader {
    public partial class Form1 : Form {
        private List<ItemData> items;
        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            // 処理中の再押下防止
            btRssGet.Enabled = false;
            try {
                using (var hc = new HttpClient()) {

                    // レスポンス
                    var res = await hc.GetAsync(tbUrl.Text);

                    // 取得できなければ中断

                    res.EnsureSuccessStatusCode();

                    var urlStr = await res.Content.ReadAsStringAsync();

                    XDocument xdoc = XDocument.Parse(urlStr);

                    items = xdoc.Root.Descendants("item")
                        .Select(x =>
                            new ItemData {
                                Title = (string)x.Element("title"),
                            }
                        ).ToList();
                    lbTitles.DataSource = items;
                    lbTitles.Refresh();
                }
            } catch (XmlException) {
                MessageBox.Show("XMLを変換できませんでした。", "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            } catch (HttpRequestException) {
                MessageBox.Show("データの取得に失敗しました。", "エラー",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            } finally {
                btRssGet.Enabled = true;
            }
        }
    }
}
