using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RssReader {
    public partial class Form1 : Form {
        private List<ItemData> items;

        private List<FavoriteItem> favoriteItems;


        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            // �������̍ĉ����h�~
            btRssGet.Enabled = false;
            try {
                using (var hc = new HttpClient()) {

                    var tmp = favoriteItems.Where(x => x.Itemname == cbUrl.Text).ToList();
                    string cbText = tmp.Count() > 0 ? tmp[0].ItemUrl : cbUrl.Text;

                    // ���X�|���X
                    var res = await hc.GetAsync(cbText);

                    // �擾�ł��Ȃ���Β��f
                    res.EnsureSuccessStatusCode();

                    var xmlStr = await res.Content.ReadAsStringAsync();

                    XDocument xdoc = XDocument.Parse(xmlStr);

#pragma warning disable CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B
                    items = xdoc.Root.Descendants("item")
                        .Select(x =>
                            new ItemData {
                                Title = x.Element("title").Value,
                                Link = x.Element("link").Value,
                                PubDate = DateTime.ParseExact(
                                    x.Element("pubDate").Value,
                                    "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                    CultureInfo.InvariantCulture
                                ),
                            }
                        ).ToList();
#pragma warning restore CS8602 // null �Q�Ƃ̉\����������̂̋t�Q�Ƃł��B

                    lbTitles.DataSource = items;
                    lbTitles.Refresh();
                }
            } catch (XmlException) {
                MessageBox.Show("XML��ϊ��ł��܂���ł����B", "�G���[",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } catch (Exception ex) when (ex is HttpRequestException || ex is InvalidOperationException) {
                MessageBox.Show("�f�[�^�̎擾�Ɏ��s���܂����B", "�G���[",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } catch (Exception) {
                MessageBox.Show("�s���ȃG���[���������܂����B", "�G���[",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } finally {
                btRssGet.Enabled = true;
            }

        }

        private void lbTitles_DoubleClick(object sender, EventArgs e) {
            if (lbTitles.SelectedItems is null) return;
            var obj = lbTitles.SelectedItem;
            if (obj is ItemData data) {
                webView21.Source = new Uri(data.Link);
            }
        }

        private void btWebBack_Click(object sender, EventArgs e) {
            webView21.GoBack();
        }

        private void btWebForward_Click(object sender, EventArgs e) {
            webView21.GoForward();
        }

        private void btReload_Click(object sender, EventArgs e) {
            webView21.Reload();
        }

        private void tbWebUrl_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                webView21.Source = new Uri(tbWebUrl.Text);
            }
        }

        private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            tbWebUrl.Text = webView21.Source.ToString();
        }

        private void btRssFavorite_Click(object sender, EventArgs e) {
            InputDialog dlg = new InputDialog(cbUrl.Text);
            if (dlg.ShowDialog() == DialogResult.OK) {
                if (!addComboItems(dlg.Input, cbUrl.Text)) {
                    MessageBox.Show("���̖��O�͊��Ɏg�p����Ă��܂��B", "�d���G���[");
                }
            }
        }

        private bool addComboItems(string additem, string addurl) {
            FavoriteItem item = new FavoriteItem {
                Itemname = additem,
                ItemUrl = addurl,
            };
            if (favoriteItems.Contains(item)) {
                return false;
            }

            favoriteItems.Add(item);
            upDateCbItems();
            saveItem("cmbItem.json", favoriteItems);
            return true;
        }

        private void saveItem(string filePath, object item) {
            string jsonText = JsonSerializer.Serialize(item,
                new JsonSerializerOptions {
                    WriteIndented = true
                });
            System.IO.File.WriteAllText(filePath, jsonText);
        }

        private void Form1_Load(object sender, EventArgs e) {
            favoriteItems = new List<FavoriteItem>();
            var items = loadItem<List<FavoriteItem>>("cmbItem.json");
            if (items is not null) {
                favoriteItems.AddRange(items);
                upDateCbItems();
            }
        }

        private void upDateCbItems() {
            cbUrl.Items.Clear();
            cbUrl.Items.AddRange(favoriteItems.Select(x => x.Itemname).ToArray());

        }

        private void button1_Click(object sender, EventArgs e) {
            if (favoriteItems.Where(x => cbUrl.Text.Contains(x.Itemname)).Count() == 0) {
                return;
            }
            if (MessageBox.Show("["+cbUrl.Text + "]�����C�ɓ��肩��폜���܂���?",
                "�m�F", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                favoriteItems.RemoveAll(x => x.Itemname == cbUrl.Text);
                upDateCbItems();
                cbUrl.Text= string.Empty;
            }
        }

        private T? loadItem<T>(string filePath) {
            if (System.IO.File.Exists(filePath)) {
                string jsonText = System.IO.File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonText);
            }
            return default(T);
        }
    }
}
