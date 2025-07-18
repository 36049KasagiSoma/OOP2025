using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Policy;
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
                //Process.Start(
                //    new ProcessStartInfo(data.Link) {
                //        UseShellExecute = true
                //    }
                //);
                webView21.Source = new Uri(data.Link);

            }
        }
    }
}
