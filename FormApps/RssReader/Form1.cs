using RssReader.Properties;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RssReader {
    public partial class Form1 : Form {
        private List<ItemData> items;

        private List<FavoriteItem> favoriteItems;
        private bool isLoading = false;

        public Form1() {
            items = new List<ItemData>();
            favoriteItems = new List<FavoriteItem>();
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            // 処理中の再押下防止
            btRssGet.Enabled = false;
            statusLabel.Text = "XMLを取得中...";
            try {
                using (var hc = new HttpClient()) {

                    hc.Timeout = TimeSpan.FromSeconds(SettingData.GetInstance().GetTimeOutValue()); // タイムアウト

                    var tmp = favoriteItems.Where(x => x.Itemname == cbUrl.Text).ToList();
                    string cbText = tmp.Count() > 0 ? tmp[0].ItemUrl : cbUrl.Text;

                    if (!IsValidUrl(cbText)) { // URLが間違っていたらthrow
                        throw new HttpRequestException();
                    }
                    // レスポンス
                    var res = await hc.GetAsync(cbText);

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
                    filtedLabel.Text = "";
                    tbListFilter.Text = "";
                }
            } catch (XmlException) {
                MessageBox.Show("XMLを変換できませんでした。", "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            } catch (TaskCanceledException) {
                MessageBox.Show("接続がタイムアウトしました。", "エラー",
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
                statusLabel.Text = "";
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
            if (webView21.Source != null) {
                if (isLoading) {
                    webView21.Stop();
                } else {
                    webView21.Reload();
                }
            }
        }

        private void tbWebUrl_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                try {
                    webView21.Source = new Uri(tbWebUrl.Text);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, ex.GetType().ToString(),
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    tbWebUrl.Text = string.Empty;
                    tbWebUrl.Focus();
                }
            }
        }

        private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            btWebBack.Enabled = webView21.CanGoBack;
            btWebForward.Enabled = webView21.CanGoForward;
            tbWebUrl.Text = webView21.Source.ToString();
        }

        private void btRssFavorite_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(cbUrl.Text)) {
                MessageBox.Show("保存するURLを入力してください。",
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbUrl.Focus();
                return;
            }
            if (!IsValidUrl(cbUrl.Text)) {
                MessageBox.Show("有効なURLを入力してください。",
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbUrl.Focus();
                return;
            }
            InputDialog dlg = new InputDialog(cbUrl.Text, favoriteItems.Select(x => x.Itemname).ToList());
            if (dlg.ShowDialog() == DialogResult.OK) {
                addComboItems(dlg.Input, cbUrl.Text);
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
            StaticEvent.SaveItem("cmbItem.json", favoriteItems);
            return true;
        }

      

        private void setBackColor() {
            this.BackColor = SettingData.GetInstance().GetBackColor()[2];
            menuStrip1.BackColor = this.BackColor;
            statusStrip1.BackColor = this.BackColor;
            menuStrip1.ForeColor = SettingData.GetInstance().GetBackColor()[3];
            statusStrip1.ForeColor = SettingData.GetInstance().GetBackColor()[3];
            lbTitles.Refresh();
            foreach (Label label in StaticEvent.GetAllLabels(this)) {
                label.ForeColor = SettingData.GetInstance().GetBackColor()[3];
            }
        }

        private async void Form1_Load(object sender, EventArgs e) {
            setBackColor();
            favoriteItems = new List<FavoriteItem>();
            var items = StaticEvent.LoadItem<List<FavoriteItem>>("cmbItem.json");
            if (items is not null) {
                favoriteItems.AddRange(items);
                upDateCbItems();
            }
            btWebBack.Enabled = false;
            btWebForward.Enabled = false;
            loadImageEnable(false);
            try {
                loadImageEnable(true);
                await webView21.EnsureCoreWebView2Async(null);
                loadImageEnable(false);
            } catch (Exception) {
            }
        }

        private void upDateCbItems() {
            cbUrl.Items.Clear();
            cbUrl.Items.AddRange(favoriteItems.Select(x => x.Itemname).ToArray());

        }

        private void btRssFavoriteRemove_Click(object sender, EventArgs e) {
            if (favoriteItems.Where(x => cbUrl.Text.Contains(x.Itemname)).Count() == 0) {
                MessageBox.Show("削除するお気に入りを選択してください。",
                    "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MessageBox.Show("[" + cbUrl.Text + "]をお気に入りから削除しますか?",
                "確認", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                favoriteItems.RemoveAll(x => x.Itemname == cbUrl.Text);
                upDateCbItems();
                StaticEvent.SaveItem("cmbItem.json", favoriteItems);
                cbUrl.Text = string.Empty;
            }
        }

        private void webView21_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e) {
            isLoading = true;
            loadImageEnable(isLoading);
        }

        private void webView21_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            isLoading = false;
            loadImageEnable(isLoading);
        }

        private void loadImageEnable(bool isEnable) {
            loadImage.Visible = isEnable;
            btReload.Text = isEnable ? "×" : "↻";
        }

       

        public static bool IsValidUrl(string url) {
            if (string.IsNullOrWhiteSpace(url))
                return false;

#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
        }

        private void btListFilter_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(tbListFilter.Text)) {
                lbTitles.DataSource = items;
                lbTitles.Refresh();
                filtedLabel.Text = "";
                return;
            }
            string tmp = tbListFilter.Text.Replace("　", " ").Replace("&", " ");
            string[] words = tmp.Split(" ");
            List<ItemData> filtedList = new List<ItemData>();
            foreach (ItemData item in items) {
                foreach (string word in words) {
                    if (item.ToString().ToLower().Contains(word.ToLower())) {
                        filtedList.Add(item);
                        break;
                    }
                }
            }
            lbTitles.DataSource = filtedList;
            lbTitles.Refresh();
            filtedLabel.Text = $"フィルター表示中 >> {items.Count} 件中 {filtedList.Count} 件";
        }

        private void tbListFilter_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                btListFilter_Click(sender, new EventArgs());
            }
        }

        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            //参考:https://apuridasuo.hatenablog.com/entry/2020/07/29/135006

            // 背景描画
            e.DrawBackground();

            // 描画許可判定
            if (e.Index < 0) {
                return;
            }
            // 描画用変数設定
            Brush NdClrWd = new SolidBrush(e.ForeColor);
            string NdWord = ((ListBox)sender).Items[e.Index].ToString() ?? "";
            // 奇数行の場合は背景色を変更し、縞々に見えるようにする
            Color backcolor;
            if (e.Index % 2 == 0) {
                backcolor = SettingData.GetInstance().GetBackColor()[1];
            } else {
                backcolor = SettingData.GetInstance().GetBackColor()[0];
            }
            // ノード作成
            e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State, e.ForeColor, backcolor);
            e.DrawBackground();
            e.Graphics.DrawString(NdWord, e.Font ?? new Font(new FontFamily("ＭＳ ゴシック"), 12f), NdClrWd, e.Bounds, StringFormat.GenericDefault);
            NdClrWd.Dispose();
            e.DrawFocusRectangle();
        }

        private void tsmiSetting_Click(object sender, EventArgs e) {
            new SettingDialog().ShowDialog();
            setBackColor();
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void AbToolStripMenuItem_Click(object sender, EventArgs e) {
            new Version().ShowDialog();
        }
    }
}
