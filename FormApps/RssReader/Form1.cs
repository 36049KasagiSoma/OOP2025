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
            // 処理中の再押下防止
            btRssGet.Enabled = false;
            try {
                using (var hc = new HttpClient()) {

                    // レスポンス
                    var res = await hc.GetAsync(tbUrl.Text);

                    // 取得できなければ中断
                    res.EnsureSuccessStatusCode();

                    var xmlStr = await res.Content.ReadAsStringAsync();

                    XDocument xdoc = XDocument.Parse(xmlStr);

#pragma warning disable CS8602 // null 参照の可能性があるものの逆参照です。
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
#pragma warning restore CS8602 // null 参照の可能性があるものの逆参照です。

                    lbTitles.DataSource = items;
                    lbTitles.Refresh();
                }
            } catch (XmlException) {
                MessageBox.Show("XMLを変換できませんでした。", "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } catch (Exception ex) when (ex is HttpRequestException || ex is InvalidOperationException) {
                MessageBox.Show("データの取得に失敗しました。", "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } catch (Exception) {
                MessageBox.Show("不明なエラーが発生しました。", "エラー",
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
