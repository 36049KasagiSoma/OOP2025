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
        private bool isWebView2Initialized = false;
        public MainWindow() {
            InitializeComponent();
            webView.CoreWebView2InitializationCompleted += this.WebView2InitializationCompleted;

            webView.NavigationStarting += (s, e) => {
                LoadingProgressBar.IsIndeterminate = true;
            };
            webView.NavigationCompleted += (s, e) => {
                LoadingProgressBar.IsIndeterminate = false;
                AddressBar.Text = webView.CoreWebView2.Source;
            };

            webView.EnsureCoreWebView2Async(null);
        }

        private void WebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            if (e.IsSuccess) {
                isWebView2Initialized = true;
                webView.CoreWebView2.Navigate("https://google.com");
            } else {
                // エラー処理
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            if (isWebView2Initialized && webView.CoreWebView2.CanGoBack) {
                webView.CoreWebView2.GoBack();
            }
        }

        private void ForwordButton_Click(object sender, RoutedEventArgs e) {
            if (isWebView2Initialized && webView.CoreWebView2.CanGoForward) {
                webView.CoreWebView2.GoForward();
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e) {
            if (!isWebView2Initialized) return;
            if (LoadingProgressBar.IsIndeterminate) {
                webView.CoreWebView2.Stop();
            }
            var url = AddressBar.Text;
            try {
                webView.CoreWebView2.Navigate(url);
            } catch (Exception ex) when (ex is UriFormatException || ex is ArgumentException) {
                webView.CoreWebView2.Navigate("https://www.google.com/search?q=" + Uri.EscapeDataString(url));
            }
            }

        private void AddressBar_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                GoButton_Click(sender, e);
            }
        }
    }
}
