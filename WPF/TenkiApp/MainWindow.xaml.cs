using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private Dictionary<string, CityInfo> cities;
        private string selectedCity = "東京";
        private double mapMinLat, mapMaxLat, mapMinLon, mapMaxLon;
        private double mapCanvasWidth, mapCanvasHeight;
        private double mapScale, mapOffsetX, mapOffsetY;
        private Ellipse currentMarker;
        private double zoomLevel = 1.0;
        private Point lastMousePosition;
        private bool isDragging = false;
        private JsonElement mapFeatures;
        private double baseScale, baseOffsetX, baseOffsetY;

        public MainWindow() {
            InitializeComponent();
            InitializeCities();

            cmbCity.SelectionChanged += CmbCity_SelectionChanged;
            btnGetWeather.Click += BtnGetWeather_Click;
            MapCanvas.SizeChanged += MapCanvas_SizeChanged;
            MapCanvas.MouseLeftButtonDown += MapCanvas_MouseLeftButtonDown;
            MapCanvas.MouseWheel += MapCanvas_MouseWheel;
            MapCanvas.MouseMove += MapCanvas_MouseMove;
            MapCanvas.MouseLeftButtonUp += MapCanvas_MouseLeftButtonUp;

            // 初期表示
            UpdateCoordinates();
        }

        private void MapCanvas_SizeChanged(object sender, SizeChangedEventArgs e) {
            MapCanvas.Children.Clear();
            LoadJapanMapAsync();
        }

        private void InitializeCities() {
            cities = new Dictionary<string, CityInfo>
            {
                { "札幌", new CityInfo(43.0642, 141.3469) },
                { "仙台", new CityInfo(38.2682, 140.8694) },
                { "東京", new CityInfo(35.6762, 139.6503) },
                { "横浜", new CityInfo(35.4437, 139.6380) },
                { "名古屋", new CityInfo(35.1815, 136.9066) },
                { "京都", new CityInfo(35.0116, 135.7681) },
                { "大阪", new CityInfo(34.6937, 135.5023) },
                { "神戸", new CityInfo(34.6901, 135.1955) },
                { "広島", new CityInfo(34.3853, 132.4553) },
                { "福岡", new CityInfo(33.5904, 130.4017) },
                { "那覇", new CityInfo(26.2124, 127.6809) }
            };
        }

        private async void LoadJapanMapAsync() {
            try {
                if (mapFeatures.ValueKind == JsonValueKind.Undefined) {
                    string geoJsonPath = "japan.json";
                    string geoJsonData = await File.ReadAllTextAsync(geoJsonPath);

                    ParseAndDrawGeoJson(geoJsonData);
                } else {
                    // キャンバスサイズが変わった場合は基準値を再計算
                    mapCanvasWidth = MapCanvas.ActualWidth;
                    mapCanvasHeight = MapCanvas.ActualHeight;

                    double latRange = mapMaxLat - mapMinLat;
                    double lonRange = mapMaxLon - mapMinLon;
                    double mapAspect = lonRange / latRange;
                    double canvasAspect = mapCanvasWidth / mapCanvasHeight;

                    baseOffsetX = 0;
                    baseOffsetY = 0;
                    baseScale = 1.0;

                    if (canvasAspect > mapAspect) {
                        baseScale = mapCanvasHeight / latRange;
                        baseOffsetX = (mapCanvasWidth - lonRange * baseScale) / 2;
                    } else {
                        baseScale = mapCanvasWidth / lonRange;
                        baseOffsetY = (mapCanvasHeight - latRange * baseScale) / 2;
                    }

                    // ズームレベルをリセット
                    zoomLevel = 1.0;
                    mapScale = baseScale;
                    mapOffsetX = baseOffsetX;
                    mapOffsetY = baseOffsetY;

                    RedrawMap();
                }
            } catch (Exception ex) {
                MessageBox.Show($"地図の読み込みに失敗しました: {ex.Message}\n簡易地図を表示します。",
                    "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                DrawSimpleMap();
            }
        }

        private void ParseAndDrawGeoJson(string geoJsonData) {
            var json = JsonDocument.Parse(geoJsonData);
            mapFeatures = json.RootElement.GetProperty("features");

            // 日本の実際の境界値（マージンを追加）
            mapMinLat = 23.0;
            mapMaxLat = 46.5;
            mapMinLon = 122.0;
            mapMaxLon = 146.0;

            mapCanvasWidth = MapCanvas.ActualWidth;
            mapCanvasHeight = MapCanvas.ActualHeight;

            // アスペクト比を考慮した調整
            double latRange = mapMaxLat - mapMinLat;
            double lonRange = mapMaxLon - mapMinLon;
            double mapAspect = lonRange / latRange;
            double canvasAspect = mapCanvasWidth / mapCanvasHeight;

            baseOffsetX = 0;
            baseOffsetY = 0;
            baseScale = 1.0;

            if (canvasAspect > mapAspect) {
                baseScale = mapCanvasHeight / latRange;
                baseOffsetX = (mapCanvasWidth - lonRange * baseScale) / 2;
            } else {
                baseScale = mapCanvasWidth / lonRange;
                baseOffsetY = (mapCanvasHeight - latRange * baseScale) / 2;
            }

            // 初期値を設定
            mapScale = baseScale;
            mapOffsetX = baseOffsetX;
            mapOffsetY = baseOffsetY;

            RedrawMap();
        }

        private void RedrawMap() {
            MapCanvas.Children.Clear();

            // 現在のズームとオフセットを適用してポリゴンを描画
            foreach (var feature in mapFeatures.EnumerateArray()) {
                var geometry = feature.GetProperty("geometry");
                var type = geometry.GetProperty("type").GetString();

                if (type == "Polygon" || type == "MultiPolygon") {
                    DrawPolygon(geometry, mapScale, mapOffsetX, mapOffsetY);
                }
            }

            UpdateMarkerPosition();
        }

        private void UpdateMarkerPosition() {
            double lat = double.Parse(txtLatitude.Text);
            double lon = double.Parse(txtLongitude.Text);

            // マーカーが既に存在する場合は削除
            if (currentMarker != null) {
                MapCanvas.Children.Remove(currentMarker);
            }

            // 緯度経度をキャンバス座標に変換
            double x = (lon - mapMinLon) * mapScale + mapOffsetX;
            double y = (mapMaxLat - lat) * mapScale + mapOffsetY;

            // 新しいマーカーを作成
            currentMarker = new Ellipse {
                Width = 16,
                Height = 16,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EF4444")),
                Stroke = Brushes.White,
                StrokeThickness = 2
            };

            Canvas.SetLeft(currentMarker, x - 8);
            Canvas.SetTop(currentMarker, y - 8);

            MapCanvas.Children.Add(currentMarker);
        }

        private void DrawPolygon(JsonElement geometry, double scale, double offsetX, double offsetY) {
            var type = geometry.GetProperty("type").GetString();
            var coordinates = geometry.GetProperty("coordinates");

            if (type == "Polygon") {
                foreach (var ring in coordinates.EnumerateArray()) {
                    DrawRing(ring, scale, offsetX, offsetY);
                }
            } else if (type == "MultiPolygon") {
                foreach (var polygon in coordinates.EnumerateArray()) {
                    foreach (var ring in polygon.EnumerateArray()) {
                        DrawRing(ring, scale, offsetX, offsetY);
                    }
                }
            }
        }

        private void MapCanvas_MouseWheel(object sender, MouseWheelEventArgs e) {
            Point mousePos = e.GetPosition(MapCanvas);

            double zoomFactor = e.Delta > 0 ? 1.1 : 0.9;
            double newZoomLevel = zoomLevel * zoomFactor;

            // ズームレベルの制限（0.5倍～3倍）
            if (newZoomLevel >= 0.5 && newZoomLevel <= 3.0) {
                // マウス位置を中心にズーム
                double oldScale = baseScale * zoomLevel;
                double newScale = baseScale * newZoomLevel;

                // マウス位置での座標を維持するようにオフセットを調整
                mapOffsetX = mousePos.X - (mousePos.X - mapOffsetX) * (newScale / oldScale);
                mapOffsetY = mousePos.Y - (mousePos.Y - mapOffsetY) * (newScale / oldScale);

                zoomLevel = newZoomLevel;
                mapScale = newScale;

                RedrawMap();
            }

            e.Handled = true;
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e) {
            if (isDragging && e.LeftButton == MouseButtonState.Pressed) {
                Point currentPosition = e.GetPosition(MapCanvas);
                double deltaX = currentPosition.X - lastMousePosition.X;
                double deltaY = currentPosition.Y - lastMousePosition.Y;

                mapOffsetX += deltaX;
                mapOffsetY += deltaY;

                lastMousePosition = currentPosition;

                RedrawMap();

                e.Handled = true;
            }
        }

        private void MapCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            if (isDragging) {
                isDragging = false;
                MapCanvas.ReleaseMouseCapture();
                MapCanvas.Cursor = Cursors.Arrow;
            }
        }

        private void DrawRing(JsonElement ring, double scale, double offsetX, double offsetY) {
            var points = new PointCollection();

            foreach (var coord in ring.EnumerateArray()) {
                var lon = coord[0].GetDouble();
                var lat = coord[1].GetDouble();

                // 緯度経度をキャンバス座標に変換（アスペクト比を維持）
                double x = (lon - mapMinLon) * scale + offsetX;
                double y = (mapMaxLat - lat) * scale + offsetY;

                points.Add(new Point(x, y));
            }

            if (points.Count > 0) {
                var polygon = new Polygon {
                    Points = points,
                    Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8")),
                    StrokeThickness = 1,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BAE6FD")),
                    Opacity = 0.6
                };

                MapCanvas.Children.Add(polygon);
            }
        }

        private void DrawSimpleMap() {
            // GeoJSON読み込み失敗時の簡易地図
            var japanPath = new System.Windows.Shapes.Path {
                Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#94A3B8")),
                StrokeThickness = 2,
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#BAE6FD")),
                Opacity = 0.4,
                Data = Geometry.Parse(@"
                    M 160,20 L 200,15 L 220,25 L 215,45 L 210,55 L 190,60 L 170,50 L 165,35 Z
                    M 185,80 L 195,85 L 200,95 L 205,110 L 200,130 L 195,145 L 190,160 
                    L 195,175 L 200,185 L 195,200 L 185,210 L 175,215 L 160,220 
                    L 145,225 L 130,228 L 115,230 L 100,228 L 90,220 L 85,205 
                    L 90,190 L 100,180 L 110,175 L 125,170 L 140,165 L 150,155 
                    L 155,145 L 160,130 L 165,115 L 170,100 L 175,90 Z
                    M 105,235 L 120,233 L 135,235 L 145,240 L 140,248 L 125,250 L 110,248 Z
                    M 35,250 L 55,245 L 70,248 L 85,255 L 95,265 L 90,280 
                    L 80,290 L 65,295 L 50,293 L 40,285 L 35,270 Z
                    M 70,380 L 85,378 L 95,385 L 90,395 L 75,398 L 65,392 Z")
            };
            MapCanvas.Children.Add(japanPath);
        }

        private void MapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            lastMousePosition = e.GetPosition(MapCanvas);

            // ダブルクリックで位置選択
            if (e.ClickCount == 2) {
                Point clickPos = e.GetPosition(MapCanvas);

                // クリック座標を緯度経度に変換
                double lon = (clickPos.X - mapOffsetX) / mapScale + mapMinLon;
                double lat = mapMaxLat - (clickPos.Y - mapOffsetY) / mapScale;

                // 日本の範囲内かチェック
                if (lat >= mapMinLat && lat <= mapMaxLat && lon >= mapMinLon && lon <= mapMaxLon) {
                    txtLatitude.Text = lat.ToString("F4");
                    txtLongitude.Text = lon.ToString("F4");
                    selectedCity = "カスタム位置";
                    txtCurrentCity.Text = selectedCity;

                    cmbCity.SelectedIndex = 0;

                    UpdateMarkerPosition();

                    ButtonExtensions.PerformClick(btnGetWeather);
                }
            } else {
                // シングルクリックでドラッグ開始
                isDragging = true;
                MapCanvas.CaptureMouse();
                MapCanvas.Cursor = Cursors.Hand;
            }

            e.Handled = true;
        }

        private void CmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (cmbCity.SelectedItem is ComboBoxItem item) {
                string cityName = item.Content.ToString();
                if (cities.ContainsKey(cityName)) {
                    selectedCity = cityName;
                    UpdateCoordinates();
                    UpdateMarkerPosition();
                }
            }
        }

        private void UpdateCoordinates() {
            if (cities.ContainsKey(selectedCity)) {
                var city = cities[selectedCity];
                txtLatitude.Text = city.Latitude.ToString("F4");
                txtLongitude.Text = city.Longitude.ToString("F4");
                txtCurrentCity.Text = selectedCity;
                ButtonExtensions.PerformClick(btnGetWeather);
            }
        }


        private async void BtnGetWeather_Click(object sender, RoutedEventArgs e) {
            LoadingOverlay.Visibility = Visibility.Visible;

            try {
                double lat = double.Parse(txtLatitude.Text);
                double lon = double.Parse(txtLongitude.Text);

                await GetWeatherData(lat, lon);
            } catch (Exception ex) {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            } finally {
                LoadingOverlay.Visibility = Visibility.Collapsed;
            }
        }

        private async Task GetWeatherData(double latitude, double longitude) {
            using (var client = new HttpClient()) {
                string url = $"https://api.open-meteo.com/v1/forecast?" +
                    $"latitude={latitude}&longitude={longitude}" +
                    $"&current=temperature_2m,relative_humidity_2m,weather_code,wind_speed_10m,surface_pressure" +
                    $"&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max" +
                    $"&timezone=Asia/Tokyo";

                var response = await client.GetStringAsync(url);
                var weatherData = JsonSerializer.Deserialize<JsonElement>(response);

                // 現在の天気
                var current = weatherData.GetProperty("current");
                txtTemperature.Text = $"{current.GetProperty("temperature_2m").GetDouble():F1}°C";
                txtHumidity.Text = $"{current.GetProperty("relative_humidity_2m").GetInt32()}%";
                txtWindSpeed.Text = $"{current.GetProperty("wind_speed_10m").GetDouble():F1} km/h";
                txtPressure.Text = $"{current.GetProperty("surface_pressure").GetDouble():F0} hPa";

                var weatherCode = current.GetProperty("weather_code").GetInt32();
                txtWeatherDescription.Text = GetWeatherDescription(weatherCode);

                // 現在の天気画像を設定
                try {
                    string imageUri = GetWeatherImage(weatherCode);
                    imgCurrentWeather.Source = new System.Windows.Media.Imaging.BitmapImage(
                        new Uri(imageUri, UriKind.Absolute));
                } catch (Exception ex) {
                    System.Diagnostics.Debug.WriteLine($"画像読み込みエラー: {ex.Message}");
                }

                // 日次予報
                var daily = weatherData.GetProperty("daily");
                var dates = daily.GetProperty("time").EnumerateArray().ToList();
                var maxTemps = daily.GetProperty("temperature_2m_max").EnumerateArray().ToList();
                var minTemps = daily.GetProperty("temperature_2m_min").EnumerateArray().ToList();
                var weatherCodes = daily.GetProperty("weather_code").EnumerateArray().ToList();
                var precipProbs = daily.GetProperty("precipitation_probability_max").EnumerateArray().ToList();

                if (dates.Count > 0) {
                    txtMaxTemp.Text = $"{maxTemps[0].GetDouble():F1}°C";
                    txtMinTemp.Text = $"{minTemps[0].GetDouble():F1}°C";
                    txtPrecipitation.Text = $"{precipProbs[0].GetInt32()}%";
                }

                // 7日間予報のリスト
                var forecastList = new List<ForecastItem>();
                for (int i = 0; i < Math.Min(7, dates.Count); i++) {
                    forecastList.Add(new ForecastItem {
                        Date = DateTime.Parse(dates[i].GetString()).ToString("MM/dd (ddd)"),
                        Description = GetWeatherDescription(weatherCodes[i].GetInt32()),
                        MaxTemp = $"{maxTemps[i].GetDouble():F1}°C",
                        MinTemp = $"{minTemps[i].GetDouble():F1}°C",
                        WeatherImage = GetWeatherImage(weatherCodes[i].GetInt32()) // 追加
                    });
                }
                lstForecast.ItemsSource = forecastList;

                txtCurrentDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            }
        }

        private string GetWeatherDescription(int code) {
            return code switch {
                0 => "快晴",
                1 => "晴れ",
                2 => "薄曇り",
                3 => "曇り",
                45 or 48 => "霧",
                51 or 53 or 55 or 56 or 57 => "霧雨",
                61 or 63 or 65 or 66 or 67 => "雨",
                71 or 73 or 75 or 77 => "雪",
                80 or 81 or 82 => "にわか雨",
                85 or 86 => "にわか雪",
                95 => "雷雨",
                96 or 99 => "雷雨(雹)",
                _ => "不明"
            };
        }

        private string GetWeatherImage(int code) {
            string imageName = code switch {
                0 => "weather_clear.png",                         // 快晴
                1 => "weather_sunny.png",                         // 晴れ
                2 or 3 => "weather_cloudy.png",                   // 薄曇り〜曇り
                45 or 48 => "weather_fog.png",                    // 霧
                51 or 53 or 55 or 56 or 57 => "weather_drizzle.png", // 霧雨・着氷性霧雨
                61 or 63 or 65 or 66 or 67 => "weather_rain.png", // 雨・凍雨（氷雨）
                71 or 73 or 75 or 77 => "weather_snow.png",       // 雪（雪粒含む）
                80 or 81 or 82 => "weather_shower.png",           // にわか雨
                85 or 86 => "weather_snow_shower.png",            // にわか雪
                95 => "weather_thunderstorm.png",                 // 雷雨
                96 or 99 => "weather_thunderstorm_hail.png",      // 雷雨（雹）
                _ => "weather_unknown.png"                        // 不明
            };

            return $"pack://application:,,,/icons/{imageName}";

        }
    }
}