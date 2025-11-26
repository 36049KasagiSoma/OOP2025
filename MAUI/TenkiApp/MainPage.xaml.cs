using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Graphics;

namespace TenkiApp;

public partial class MainPage : ContentPage {
    private Dictionary<string, CityInfo> cities;
    private string selectedCity = "東京（東京）";
    private MapDrawable mapDrawable;
    private PointF lastPanPosition;
    private bool isPanning = false;

    public MainPage() {
        InitializeComponent();
        InitializeCities();
        InitializeCityPicker();

        // 地図の初期化
        mapDrawable = new MapDrawable();
        MapGraphicsView.Drawable = mapDrawable;

        // MapGraphicsViewではなくMapCanvasにジェスチャーを設定（既存のまま）
        var tapGesture = new TapGestureRecognizer { NumberOfTapsRequired = 1 }; // 1に変更
        tapGesture.Tapped += MapCanvas_Tapped; // メソッド名変更
        MapCanvas.GestureRecognizers.Add(tapGesture);

        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += MapCanvas_PanUpdated;
        MapCanvas.GestureRecognizers.Add(panGesture);

        var pinchGesture = new PinchGestureRecognizer();
        pinchGesture.PinchUpdated += MapCanvas_PinchUpdated;
        MapCanvas.GestureRecognizers.Add(pinchGesture);

        // GraphicsViewにもタップ処理を追加
        MapGraphicsView.StartInteraction += MapGraphicsView_StartInteraction; // 追加
        MapGraphicsView.EndInteraction += MapGraphicsView_EndInteraction; // 追加

        // 初期天気取得
        _ = LoadMapAndGetWeatherAsync();
    }

    private PointF tapStartPoint; // クラスフィールドとして追加
    private DateTime tapStartTime; // クラスフィールドとして追加

    private void MapGraphicsView_StartInteraction(object sender, TouchEventArgs e) {
        tapStartPoint = e.Touches.FirstOrDefault();
        tapStartTime = DateTime.Now;
    }

    private async void MapGraphicsView_EndInteraction(object sender, TouchEventArgs e) {
        if (isPanning) return;

        var endPoint = e.Touches.FirstOrDefault();
        var duration = DateTime.Now - tapStartTime;

        // タップ判定：移動距離が小さく、時間が短い場合
        if (duration.TotalMilliseconds < 300 &&
            Math.Abs(endPoint.X - tapStartPoint.X) < 10 &&
            Math.Abs(endPoint.Y - tapStartPoint.Y) < 10) {

            var (lat, lon) = mapDrawable.ScreenToGeoCoordinates(
                endPoint.X,
                endPoint.Y,
                (float)MapGraphicsView.Width,
                (float)MapGraphicsView.Height
            );

            if (lat >= 23.0 && lat <= 46.5 && lon >= 122.0 && lon <= 146.0) {
                LoadingOverlay.IsVisible = true;

                try {
                    txtLatitude.Text = lat.ToString("F4");
                    txtLongitude.Text = lon.ToString("F4");

                    // 地名を取得
                    string addressName = await GetAddressFromCoordinates(lat, lon);
                    selectedCity = addressName; // 変更
                    txtCurrentCity.Text = selectedCity;

                    cmbCity.SelectedIndex = 0;

                    if (mapDrawable != null) {
                        mapDrawable.SetMarkerPosition(lat, lon);
                        MapGraphicsView.Invalidate();
                    }

                    await GetWeatherData(lat, lon);
                } finally {
                    LoadingOverlay.IsVisible = false;
                }
            }
        }
    }

    private async Task<string> GetAddressFromCoordinates(double latitude, double longitude) {
        try {
            using (var client = new HttpClient()) {
                // User-Agentヘッダーが必須
                client.DefaultRequestHeaders.Add("User-Agent", "TenkiApp/1.0");

                // OpenStreetMap Nominatim API（逆ジオコーディング）
                string url = $"https://nominatim.openstreetmap.org/reverse?" +
                    $"format=json&lat={latitude}&lon={longitude}&accept-language=ja";

                var response = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<JsonElement>(response);

                if (data.TryGetProperty("address", out var address)) {
                    string city = "";
                    string prefecture = "";

                    // 市区町村を取得
                    if (address.TryGetProperty("city", out var cityProp)) {
                        city = cityProp.GetString();
                    } else if (address.TryGetProperty("town", out var townProp)) {
                        city = townProp.GetString();
                    } else if (address.TryGetProperty("village", out var villageProp)) {
                        city = villageProp.GetString();
                    }

                    // 都道府県を取得
                    if (address.TryGetProperty("state", out var stateProp)) {
                        prefecture = stateProp.GetString();
                    }

                    // 表示用の文字列を作成
                    if (!string.IsNullOrEmpty(prefecture) && !string.IsNullOrEmpty(city)) {
                        return $"{prefecture} {city}";
                    } else if (!string.IsNullOrEmpty(prefecture)) {
                        return prefecture;
                    } else if (!string.IsNullOrEmpty(city)) {
                        return city;
                    }
                }

                return "カスタム位置";
            }
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"住所取得エラー: {ex.Message}");
            return "カスタム位置";
        }
    }

    private void InitializeCityPicker() {
        cmbCity.Items.Add("-");
        cmbCity.Items.Add("現在位置");
        cmbCity.Items.Add("北海道（札幌）");
        cmbCity.Items.Add("青森（青森）");
        cmbCity.Items.Add("岩手（盛岡）");
        cmbCity.Items.Add("宮城（仙台）");
        cmbCity.Items.Add("秋田（秋田）");
        cmbCity.Items.Add("山形（山形）");
        cmbCity.Items.Add("福島（福島）");
        cmbCity.Items.Add("茨城（水戸）");
        cmbCity.Items.Add("栃木（宇都宮）");
        cmbCity.Items.Add("群馬（前橋）");
        cmbCity.Items.Add("埼玉（さいたま）");
        cmbCity.Items.Add("千葉（千葉）");
        cmbCity.Items.Add("東京（東京）");
        cmbCity.Items.Add("神奈川（横浜）");
        cmbCity.Items.Add("新潟（新潟）");
        cmbCity.Items.Add("富山（富山）");
        cmbCity.Items.Add("石川（金沢）");
        cmbCity.Items.Add("福井（福井）");
        cmbCity.Items.Add("山梨（甲府）");
        cmbCity.Items.Add("長野（長野）");
        cmbCity.Items.Add("岐阜（岐阜）");
        cmbCity.Items.Add("静岡（静岡）");
        cmbCity.Items.Add("愛知（名古屋）");
        cmbCity.Items.Add("三重（津）");
        cmbCity.Items.Add("滋賀（大津）");
        cmbCity.Items.Add("京都（京都）");
        cmbCity.Items.Add("大阪（大阪）");
        cmbCity.Items.Add("兵庫（神戸）");
        cmbCity.Items.Add("奈良（奈良）");
        cmbCity.Items.Add("和歌山（和歌山）");
        cmbCity.Items.Add("鳥取（鳥取）");
        cmbCity.Items.Add("島根（松江）");
        cmbCity.Items.Add("岡山（岡山）");
        cmbCity.Items.Add("広島（広島）");
        cmbCity.Items.Add("山口（山口）");
        cmbCity.Items.Add("徳島（徳島）");
        cmbCity.Items.Add("香川（高松）");
        cmbCity.Items.Add("愛媛（松山）");
        cmbCity.Items.Add("高知（高知）");
        cmbCity.Items.Add("福岡（福岡）");
        cmbCity.Items.Add("佐賀（佐賀）");
        cmbCity.Items.Add("長崎（長崎）");
        cmbCity.Items.Add("熊本（熊本）");
        cmbCity.Items.Add("大分（大分）");
        cmbCity.Items.Add("宮崎（宮崎）");
        cmbCity.Items.Add("鹿児島（鹿児島）");
        cmbCity.Items.Add("沖縄（那覇）");

        cmbCity.SelectedIndex = 1;
    }

    private async Task LoadMapAndGetWeatherAsync() {
        await mapDrawable.LoadJapanMapAsync();
        MapGraphicsView.Invalidate();
        await GetWeatherData(double.Parse(txtLatitude.Text), double.Parse(txtLongitude.Text));
    }

    private void InitializeCities() {
        cities = new Dictionary<string, CityInfo>{
            { "現在位置", new CityInfo(0, 0) },
            { "北海道（札幌）", new CityInfo(43.0642, 141.3469) },
            { "青森（青森）", new CityInfo(40.8246, 140.7400) },
            { "岩手（盛岡）", new CityInfo(39.7036, 141.1527) },
            { "宮城（仙台）", new CityInfo(38.2682, 140.8694) },
            { "秋田（秋田）", new CityInfo(39.7186, 140.1024) },
            { "山形（山形）", new CityInfo(38.2404, 140.3633) },
            { "福島（福島）", new CityInfo(37.7608, 140.4747) },
            { "茨城（水戸）", new CityInfo(36.3659, 140.4712) },
            { "栃木（宇都宮）", new CityInfo(36.5551, 139.8828) },
            { "群馬（前橋）", new CityInfo(36.3895, 139.0640) },
            { "埼玉（さいたま）", new CityInfo(35.8617, 139.6455) },
            { "千葉（千葉）", new CityInfo(35.6073, 140.1063) },
            { "東京（東京）", new CityInfo(35.6762, 139.6503) },
            { "神奈川（横浜）", new CityInfo(35.4437, 139.6380) },
            { "新潟（新潟）", new CityInfo(37.9161, 139.0364) },
            { "富山（富山）", new CityInfo(36.6953, 137.2114) },
            { "石川（金沢）", new CityInfo(36.5947, 136.6256) },
            { "福井（福井）", new CityInfo(36.0641, 136.2196) },
            { "山梨（甲府）", new CityInfo(35.6639, 138.5684) },
            { "長野（長野）", new CityInfo(36.6513, 138.1810) },
            { "岐阜（岐阜）", new CityInfo(35.4233, 136.7607) },
            { "静岡（静岡）", new CityInfo(34.9769, 138.3831) },
            { "愛知（名古屋）", new CityInfo(35.1815, 136.9066) },
            { "三重（津）", new CityInfo(34.7303, 136.5086) },
            { "滋賀（大津）", new CityInfo(35.0045, 135.8686) },
            { "京都（京都）", new CityInfo(35.0116, 135.7681) },
            { "大阪（大阪）", new CityInfo(34.6937, 135.5023) },
            { "兵庫（神戸）", new CityInfo(34.6901, 135.1955) },
            { "奈良（奈良）", new CityInfo(34.6851, 135.8050) },
            { "和歌山（和歌山）", new CityInfo(34.2260, 135.1675) },
            { "鳥取（鳥取）", new CityInfo(35.5039, 134.2377) },
            { "島根（松江）", new CityInfo(35.4681, 133.0484) },
            { "岡山（岡山）", new CityInfo(34.6554, 133.9195) },
            { "広島（広島）", new CityInfo(34.3853, 132.4553) },
            { "山口（山口）", new CityInfo(34.1861, 131.4705) },
            { "徳島（徳島）", new CityInfo(34.0703, 134.5548) },
            { "香川（高松）", new CityInfo(34.3428, 134.0466) },
            { "愛媛（松山）", new CityInfo(33.8393, 132.7657) },
            { "高知（高知）", new CityInfo(33.5597, 133.5311) },
            { "福岡（福岡）", new CityInfo(33.5904, 130.4017) },
            { "佐賀（佐賀）", new CityInfo(33.2635, 130.3008) },
            { "長崎（長崎）", new CityInfo(32.7503, 129.8777) },
            { "熊本（熊本）", new CityInfo(32.8031, 130.7079) },
            { "大分（大分）", new CityInfo(33.2382, 131.6126) },
            { "宮崎（宮崎）", new CityInfo(31.9077, 131.4202) },
            { "鹿児島（鹿児島）", new CityInfo(31.5602, 130.5581) },
            { "沖縄（那覇）", new CityInfo(26.2124, 127.6809) }
        };
    }

    private async void CmbCity_SelectedIndexChanged(object sender, EventArgs e) {
        if (cmbCity.SelectedIndex < 0) return;

        string cityName = cmbCity.Items[cmbCity.SelectedIndex];
        if (cityName == "-") return; // 追加: "-"選択時は何もしない

        if (cities.ContainsKey(cityName)) {
            selectedCity = cityName;

            // UI更新を先に実行
            await Task.Delay(50); // UIスレッドに処理を譲る
            LoadingOverlay.IsVisible = true;

            try {
                if (cityName == "現在位置") {
                    await UpdateCoordinatesForCurrentLocation();
                } else {
                    await UpdateCoordinates(); // awaitに変更
                }
            } catch (Exception ex) {
                await DisplayAlert("エラー", $"天気情報の取得に失敗しました: {ex.Message}", "OK");
            } finally {
                LoadingOverlay.IsVisible = false;
            }
        }
    }

    private async Task UpdateCoordinates() { // Task返り値に変更
        if (cities.ContainsKey(selectedCity)) {
            var city = cities[selectedCity];
            txtLatitude.Text = city.Latitude.ToString("F4");
            txtLongitude.Text = city.Longitude.ToString("F4");
            txtCurrentCity.Text = selectedCity;

            if (mapDrawable != null) {
                mapDrawable.SetMarkerPosition(city.Latitude, city.Longitude);
                MapGraphicsView.Invalidate();
            }

            await GetWeatherData(city.Latitude, city.Longitude);
        }
    }

    private async Task UpdateCoordinatesForCurrentLocation() {
        try {
            var location = await Geolocation.GetLocationAsync(new GeolocationRequest {
                DesiredAccuracy = GeolocationAccuracy.Medium,
                Timeout = TimeSpan.FromSeconds(10)
            });

            if (location != null) {
                txtLatitude.Text = location.Latitude.ToString("F4");
                txtLongitude.Text = location.Longitude.ToString("F4");

                // 地名を取得
                string addressName = await GetAddressFromCoordinates(location.Latitude, location.Longitude);
                txtCurrentCity.Text = addressName;

                if (mapDrawable != null) {
                    mapDrawable.SetMarkerPosition(location.Latitude, location.Longitude);
                    MapGraphicsView.Invalidate();
                }

                await GetWeatherData(location.Latitude, location.Longitude);
            }
        } catch (Exception ex) {
            await DisplayAlert("エラー", $"位置情報の取得に失敗しました: {ex.Message}", "OK");
        }
    }

    private async void BtnGetWeather_Clicked(object sender, EventArgs e) {
        LoadingOverlay.IsVisible = true;

        try {
            double lat = double.Parse(txtLatitude.Text);
            double lon = double.Parse(txtLongitude.Text);
            await GetWeatherData(lat, lon);
        } catch (Exception ex) {
            await DisplayAlert("エラー", $"エラーが発生しました: {ex.Message}", "OK");
        } finally {
            LoadingOverlay.IsVisible = false;
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
            txtWindSpeed.Text = $"{current.GetProperty("wind_speed_10m").GetDouble():F1}";
            txtPressure.Text = $"{current.GetProperty("surface_pressure").GetDouble():F0}";

            var weatherCode = current.GetProperty("weather_code").GetInt32();
            txtWeatherDescription.Text = GetWeatherDescription(weatherCode);

            // 画像の設定
            imgCurrentWeather.Source = GetWeatherImage(weatherCode);

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

            // 7日間予報
            var forecastList = new ObservableCollection<ForecastItem>();
            for (int i = 0; i < Math.Min(7, dates.Count); i++) {
                forecastList.Add(new ForecastItem {
                    Date = DateTime.Parse(dates[i].GetString()).ToString("MM/dd (ddd)"),
                    Description = GetWeatherDescription(weatherCodes[i].GetInt32()),
                    MaxTemp = $"{maxTemps[i].GetDouble():F1}°C",
                    MinTemp = $"{minTemps[i].GetDouble():F1}°C",
                    WeatherImage = GetWeatherImage(weatherCodes[i].GetInt32())
                });
            }
            lstForecast.ItemsSource = forecastList;

            txtCurrentDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
        }
    }

    private async void MapCanvas_Tapped(object sender, EventArgs e) {
        if (isPanning) return; // パン中は無視

        if (e is TappedEventArgs tappedArgs) {
            var position = tappedArgs.GetPosition(MapCanvas);
            if (position.HasValue) {
                var point = position.Value;

                var (lat, lon) = mapDrawable.ScreenToGeoCoordinates(
                    (float)point.X,
                    (float)point.Y,
                    (float)MapCanvas.Width,
                    (float)MapCanvas.Height
                );

                if (lat >= 23.0 && lat <= 46.5 && lon >= 122.0 && lon <= 146.0) {
                    LoadingOverlay.IsVisible = true;

                    try {
                        txtLatitude.Text = lat.ToString("F4");
                        txtLongitude.Text = lon.ToString("F4");
                        selectedCity = "カスタム位置";
                        txtCurrentCity.Text = selectedCity;

                        cmbCity.SelectedIndex = 0;

                        if (mapDrawable != null) {
                            mapDrawable.SetMarkerPosition(lat, lon);
                            MapGraphicsView.Invalidate();
                        }

                        await GetWeatherData(lat, lon);
                    } finally {
                        LoadingOverlay.IsVisible = false;
                    }
                }
            }
        }
    }

    private void MapCanvas_PanUpdated(object sender, PanUpdatedEventArgs e) {
        switch (e.StatusType) {
            case GestureStatus.Started:
                isPanning = true;
                lastPanPosition = new PointF((float)e.TotalX, (float)e.TotalY);
                break;

            case GestureStatus.Running:
                if (isPanning) {
                    float deltaX = (float)e.TotalX - lastPanPosition.X;
                    float deltaY = (float)e.TotalY - lastPanPosition.Y;

                    mapDrawable.OffsetX += deltaX;
                    mapDrawable.OffsetY += deltaY;

                    lastPanPosition = new PointF((float)e.TotalX, (float)e.TotalY);
                    MapGraphicsView.Invalidate();
                }
                break;

            case GestureStatus.Completed:
            case GestureStatus.Canceled:
                isPanning = false;
                break;
        }
    }

    private void MapCanvas_PinchUpdated(object sender, PinchGestureUpdatedEventArgs e) {
        if (e.Status == GestureStatus.Running) {
            float newZoom = mapDrawable.ZoomLevel * (float)e.Scale;

            // ズームレベルの制限（0.5倍～3倍）
            if (newZoom >= 0.5f && newZoom <= 3.0f) {
                // ピンチの中心点を考慮してオフセットを調整
                float pivotX = (float)(e.ScaleOrigin.X * MapCanvas.Width);
                float pivotY = (float)(e.ScaleOrigin.Y * MapCanvas.Height);

                float deltaZoom = newZoom / mapDrawable.ZoomLevel;

                mapDrawable.OffsetX = pivotX - (pivotX - mapDrawable.OffsetX) * deltaZoom;
                mapDrawable.OffsetY = pivotY - (pivotY - mapDrawable.OffsetY) * deltaZoom;
                mapDrawable.ZoomLevel = newZoom;

                MapGraphicsView.Invalidate();
            }
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
            0 => "weather_clear.png",
            1 => "weather_sunny.png",
            2 or 3 => "weather_cloudy.png",
            45 or 48 => "weather_fog.png",
            51 or 53 or 55 or 56 or 57 => "weather_drizzle.png",
            61 or 63 or 65 or 66 or 67 => "weather_rain.png",
            71 or 73 or 75 or 77 => "weather_snow.png",
            80 or 81 or 82 => "weather_shower.png",
            85 or 86 => "weather_snow_shower.png",
            95 => "weather_thunderstorm.png",
            96 or 99 => "weather_thunderstorm_hail.png",
            _ => "weather_unknown.png"
        };

        return imageName;
    }
}