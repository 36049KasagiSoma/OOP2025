using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebBrowser {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// WebView2が初期化されたかどうか
        /// </summary>
        private bool isWebView2Initialized = false;

        public MainWindow() {
            InitializeComponent();
            UpdateNavigationButtons();
            webView.CoreWebView2InitializationCompleted += this.WebView2InitializationCompleted;

            webView.NavigationStarting += (s, e) => {
                // プログレスバーをインジケーター表示(読み込み中)にする
                LoadingProgressBar.IsIndeterminate = true;
            };
            webView.NavigationCompleted += (s, e) => {
                // プログレスバーを通常表示(読み込み完了)にする
                LoadingProgressBar.IsIndeterminate = false;

                AddressBar.Text = webView.CoreWebView2.Source;
                this.Title = $"WebBrowser : {webView.CoreWebView2.DocumentTitle}";
                UpdateNavigationButtons();
            };
            webView.EnsureCoreWebView2Async(null);
        }

        /// <summary>
        /// 各ナビゲーションボタンの有効/無効を更新する
        /// </summary>
        private void UpdateNavigationButtons() {
            BackButton.IsEnabled = webView.CanGoBack;
            ForwordButton.IsEnabled = webView.CanGoForward;
        }

        /// <summary>
        /// WebView2の初期化完了時の処理
        /// </summary>
        private void WebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            if (e.IsSuccess) {
                // 開発者ツールの無効化
                webView.CoreWebView2.Settings.AreDevToolsEnabled = false;

                // 別ウィンドウ無効化
                webView.CoreWebView2.NewWindowRequested += NewWindowRequested;

                isWebView2Initialized = true;
                webView.CoreWebView2.Navigate("https://google.com");
            }
        }

        /// <summary>
        /// 新しいウィンドウが要求されたときの処理
        /// </summary>
        private void NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e) {
            //新しいウィンドウを開かなくする
            e.Handled = true;
            AddressBar.Text = e.Uri;
            GoButton_Click(sender, null);
        }

        /// <summary>
        /// 戻るボタンがクリックされたときの処理
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e) {
            if (isWebView2Initialized && webView.CanGoBack) {
                webView.GoBack();
            }
        }

        /// <summary>
        /// 進むボタンがクリックされたときの処理
        /// </summary>
        private void ForwordButton_Click(object sender, RoutedEventArgs e) {
            if (isWebView2Initialized && webView.CanGoForward) {
                webView.GoForward();
            }
        }

        /// <summary>
        /// 移動ボタンがクリックされたときの処理
        /// </summary>
        private void GoButton_Click(object sender, RoutedEventArgs e) {
            if (!isWebView2Initialized) return; // 初期化されていない場合は何もしない

            // ロード中の場合は停止する
            if (LoadingProgressBar.IsIndeterminate) {
                webView.CoreWebView2.Stop();
            }

            var url = AddressBar.Text.Trim();
            if (string.IsNullOrEmpty(url)) return;

            try {
                webView.CoreWebView2.Navigate(url);
            } catch (Exception ex) when (ex is UriFormatException || ex is ArgumentException) {
                // URLとして不正な場合はGoogle検索を行う
                webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + Uri.EscapeDataString(url));
            }
        }

        /// <summary>
        /// アドレスバーでEnterキーが押されたときの処理
        /// </summary>
        private void AddressBar_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                GoButton_Click(sender, e);
            }
        }
    }
}
