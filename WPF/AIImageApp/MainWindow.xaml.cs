using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AIImageApp {
    public partial class MainWindow : Window {

        // ★★★ ここに自分の OpenAI APIキーを貼り付ける ★★★
        // 例: "sk-xxxx..." で始まる文字列
        private const string ApiKey = "APIKey";

        private static readonly HttpClient httpClient = new HttpClient();

        public MainWindow() {
            InitializeComponent();
        }

        // 「生成」ボタン
        private async void GenerateImage_Click(object sender, RoutedEventArgs e) {
            string prompt = PromptBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(prompt)) {
                MessageBox.Show("プロンプトを入力してください。", "入力エラー",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(ApiKey) || ApiKey.StartsWith("sk-ここに")) {
                MessageBox.Show("MainWindow.xaml.cs の ApiKey を自分のキーに書き換えてください。",
                    "APIキー未設定", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            StatusText.Text = "AIが画像を生成しています…";

            try {
                // ========= ① リクエスト JSON を作成 =========
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", ApiKey);

                var requestBody = new ImageGenerationRequest {
                    model = "gpt-image-1",
                    prompt = prompt,
                    n = 1,
                    size = "1024x1024"
                    // ★ response_format は付けない
                };

                string json = JsonSerializer.Serialize(requestBody);
                using var content = new StringContent(json, Encoding.UTF8, "application/json");

                // ========= ② API 呼び出し =========
                var response = await httpClient.PostAsync(
                    "https://api.openai.com/v1/images/generations",
                    content
                );

                string responseJson = await response.Content.ReadAsStringAsync();

                // エラーならその内容を表示して終わり
                if (!response.IsSuccessStatusCode) {
                    StatusText.Text = $"APIエラー: {(int)response.StatusCode} {response.ReasonPhrase}\n{responseJson}";
                    return;
                }

                // ========= ③ レスポンスをパース =========
                var result = JsonSerializer.Deserialize<ImageGenerationResponse>(
                    responseJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                var first = result?.data?[0];
                if (first == null) {
                    StatusText.Text = "画像データが空でした。";
                    return;
                }

                // ========= ④ URL or Base64 から画像バイト列を取得 =========
                byte[] bytes;

                if (!string.IsNullOrWhiteSpace(first.b64_json)) {
                    // 将来 b64_json が返ってくる場合に備えて
                    bytes = Convert.FromBase64String(first.b64_json);
                } else if (!string.IsNullOrWhiteSpace(first.url)) {
                    // 通常はこちら：URL から画像ファイルをダウンロード
                    bytes = await httpClient.GetByteArrayAsync(first.url);
                } else {
                    StatusText.Text = "画像データが取得できませんでした。";
                    return;
                }

                // ========= ⑤ BitmapImage に変換して表示 =========
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(bytes);
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.EndInit();
                bmp.Freeze();

                ResultImage.Source = bmp;
                StatusText.Text = "生成完了！";
            } catch (Exception ex) {
                StatusText.Text = "エラー: " + ex.Message;
            }
        }


        // ====== OpenAI Images API 用のシンプルなモデルクラス ======

        public class ImageGenerationRequest {
            public string model { get; set; } = "";
            public string prompt { get; set; } = "";
            public int n { get; set; } = 1;
            public string size { get; set; } = "1024x1024";
        }

        public class ImageGenerationResponse {
            public List<ImageData> data { get; set; } = new();
        }

        public class ImageData {
            public string url { get; set; } = "";
            public string b64_json { get; set; } = "";
        }

    }
}



