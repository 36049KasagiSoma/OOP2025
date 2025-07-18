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
            // �������̍ĉ����h�~
            btRssGet.Enabled = false;
            try {
                using (var hc = new HttpClient()) {

                    // ���X�|���X
                    var res = await hc.GetAsync(tbUrl.Text);

                    // �擾�ł��Ȃ���Β��f

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
                MessageBox.Show("XML��ϊ��ł��܂���ł����B", "�G���[",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            } catch (HttpRequestException) {
                MessageBox.Show("�f�[�^�̎擾�Ɏ��s���܂����B", "�G���[",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            } finally {
                btRssGet.Enabled = true;
            }
        }
    }
}
