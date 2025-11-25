using System.Text.Json;
using Microsoft.Maui.Graphics;

namespace TenkiApp;

public class MapDrawable : IDrawable {
    private List<List<PointF>> polygons = new List<List<PointF>>();
    private double mapMinLat = 23.0;
    private double mapMaxLat = 46.5;
    private double mapMinLon = 122.0;
    private double mapMaxLon = 146.0;
    private double markerLat = 35.6762;
    private double markerLon = 139.6503;
    private bool mapLoaded = false;
    private bool isLoading = false;
    private object lockObj = new object();

    // ズーム・パン用
    public float ZoomLevel { get; set; } = 1.0f;
    public float OffsetX { get; set; } = 0f;
    public float OffsetY { get; set; } = 0f;

    public async Task LoadJapanMapAsync() {
        isLoading = true;
        try {
            using var stream = await FileSystem.OpenAppPackageFileAsync("japan.json");
            if (stream == null) {
                System.Diagnostics.Debug.WriteLine("japan.jsonが見つかりません");
                mapLoaded = false;
                isLoading = false;
                return;
            }

            using var reader = new StreamReader(stream);
            var geoJsonData = await reader.ReadToEndAsync();

            if (string.IsNullOrEmpty(geoJsonData)) {
                System.Diagnostics.Debug.WriteLine("japan.jsonが空です");
                mapLoaded = false;
                isLoading = false;
                return;
            }

            // JSONをパースしてポリゴンを事前計算（正規化座標で保存）
            await Task.Run(() => {
                var json = JsonDocument.Parse(geoJsonData);
                var mapFeatures = json.RootElement.GetProperty("features");

                lock (lockObj) {
                    polygons.Clear();

                    foreach (var feature in mapFeatures.EnumerateArray()) {
                        var geometry = feature.GetProperty("geometry");
                        var type = geometry.GetProperty("type").GetString();

                        if (type == "Polygon" || type == "MultiPolygon") {
                            ParsePolygonNormalized(geometry);
                        }
                    }
                }
            });

            mapLoaded = true;
            System.Diagnostics.Debug.WriteLine($"地図の読み込み成功: {polygons.Count}個のポリゴン");
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"地図の読み込みに失敗: {ex.Message}");
            mapLoaded = false;
        } finally {
            isLoading = false;
        }
    }

    private void ParsePolygonNormalized(JsonElement geometry) {
        var type = geometry.GetProperty("type").GetString();
        var coordinates = geometry.GetProperty("coordinates");

        if (type == "Polygon") {
            foreach (var ring in coordinates.EnumerateArray()) {
                ParseRingNormalized(ring);
            }
        } else if (type == "MultiPolygon") {
            foreach (var polygon in coordinates.EnumerateArray()) {
                foreach (var ring in polygon.EnumerateArray()) {
                    ParseRingNormalized(ring);
                }
            }
        }
    }

    private void ParseRingNormalized(JsonElement ring) {
        var points = new List<PointF>();

        foreach (var coord in ring.EnumerateArray()) {
            var lon = coord[0].GetDouble();
            var lat = coord[1].GetDouble();

            // 正規化座標（0.0～1.0の範囲）で保存
            float x = (float)((lon - mapMinLon) / (mapMaxLon - mapMinLon));
            float y = (float)((mapMaxLat - lat) / (mapMaxLat - mapMinLat));

            points.Add(new PointF(x, y));
        }

        if (points.Count > 0) {
            polygons.Add(points);
        }
    }

    public void SetMarkerPosition(double latitude, double longitude) {
        markerLat = latitude;
        markerLon = longitude;
    }

    public (double latitude, double longitude) ScreenToGeoCoordinates(float screenX, float screenY, float canvasWidth, float canvasHeight) {
        // ズームとオフセットを考慮して画面座標を正規化座標に変換
        float normalizedX = (screenX - OffsetX) / (canvasWidth * ZoomLevel);
        float normalizedY = (screenY - OffsetY) / (canvasHeight * ZoomLevel);

        // 正規化座標を緯度経度に変換
        double lon = normalizedX * (mapMaxLon - mapMinLon) + mapMinLon;
        double lat = mapMaxLat - normalizedY * (mapMaxLat - mapMinLat);

        return (lat, lon);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect) {
        canvas.FillColor = Color.FromArgb("#E0F2FE");
        canvas.FillRectangle(dirtyRect);

        if (!mapLoaded || polygons.Count == 0) {
            DrawSimpleMap(canvas, dirtyRect);
            DrawMarkerSimple(canvas, dirtyRect.Width, dirtyRect.Height);
            return;
        }

        // キャンバスのサイズ
        double canvasWidth = dirtyRect.Width;
        double canvasHeight = dirtyRect.Height;

        // 日本地図の範囲
        double latRange = mapMaxLat - mapMinLat;
        double lonRange = mapMaxLon - mapMinLon;
        double mapAspect = lonRange / latRange;
        double canvasAspect = canvasWidth / canvasHeight;

        double scale, offsetX = 0, offsetY = 0;

        // アスペクト比を維持してスケーリング
        if (canvasAspect > mapAspect) {
            // キャンバスが横長
            scale = canvasHeight;
            offsetX = (canvasWidth - lonRange / latRange * canvasHeight) / 2;
        } else {
            // キャンバスが縦長
            scale = canvasWidth * latRange / lonRange;
            offsetY = (canvasHeight - scale) / 2;
            scale = canvasWidth / (lonRange / latRange);
        }

        // ポリゴンを描画（正規化座標から実座標へ変換）
        canvas.FillColor = Color.FromArgb("#BAE6FD");
        canvas.StrokeColor = Color.FromArgb("#94A3B8");
        canvas.StrokeSize = 0.5f;

        lock (lockObj) {
            foreach (var points in polygons) {
                if (points.Count < 3) continue;

                var path = new PathF();
                bool isFirst = true;

                foreach (var point in points) {
                    float x = (float)(point.X * canvasWidth);
                    float y = (float)(point.Y * canvasHeight);

                    if (isFirst) {
                        path.MoveTo(x, y);
                        isFirst = false;
                    } else {
                        path.LineTo(x, y);
                    }
                }

                path.Close();
                canvas.FillPath(path);
                canvas.DrawPath(path);
            }
        }

        // マーカーを描画
        DrawMarker(canvas, canvasWidth, canvasHeight);
    }

    private void DrawMarker(ICanvas canvas, double canvasWidth, double canvasHeight) {
        // 正規化座標でマーカー位置を計算
        float x = (float)((markerLon - mapMinLon) / (mapMaxLon - mapMinLon) * canvasWidth);
        float y = (float)((mapMaxLat - markerLat) / (mapMaxLat - mapMinLat) * canvasHeight);

        canvas.FillColor = Color.FromArgb("#EF4444");
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 2;
        canvas.FillCircle(x, y, 8);
        canvas.DrawCircle(x, y, 8);
    }

    private void DrawMarkerSimple(ICanvas canvas, double canvasWidth, double canvasHeight) {
        // 簡易地図用のマーカー（仮の位置）
        float x = (float)(canvasWidth * 0.7);
        float y = (float)(canvasHeight * 0.5);

        canvas.FillColor = Color.FromArgb("#EF4444");
        canvas.StrokeColor = Colors.White;
        canvas.StrokeSize = 2;
        canvas.FillCircle(x, y, 8);
        canvas.DrawCircle(x, y, 8);
    }

    private void DrawSimpleMap(ICanvas canvas, RectF dirtyRect) {
        // 簡易的な日本地図（GeoJSON読み込み失敗時）
        var path = new PathF();

        //// 北海道
        //path.MoveTo(160, 20);
        //path.LineTo(200, 15);
        //path.LineTo(220, 25);
        //path.LineTo(215, 45);
        //path.LineTo(210, 55);
        //path.LineTo(190, 60);
        //path.LineTo(170, 50);
        //path.LineTo(165, 35);
        //path.Close();

        //// 本州
        //path.MoveTo(185, 80);
        //path.LineTo(195, 85);
        //path.LineTo(200, 95);
        //path.LineTo(205, 110);
        //path.LineTo(200, 130);
        //path.LineTo(195, 145);
        //path.LineTo(190, 160);
        //path.LineTo(195, 175);
        //path.LineTo(200, 185);
        //path.LineTo(195, 200);
        //path.LineTo(185, 210);
        //path.LineTo(175, 215);
        //path.LineTo(160, 220);
        //path.LineTo(145, 225);
        //path.LineTo(130, 228);
        //path.LineTo(115, 230);
        //path.LineTo(100, 228);
        //path.LineTo(90, 220);
        //path.LineTo(85, 205);
        //path.LineTo(90, 190);
        //path.LineTo(100, 180);
        //path.LineTo(110, 175);
        //path.LineTo(125, 170);
        //path.LineTo(140, 165);
        //path.LineTo(150, 155);
        //path.LineTo(155, 145);
        //path.LineTo(160, 130);
        //path.LineTo(165, 115);
        //path.LineTo(170, 100);
        //path.LineTo(175, 90);
        //path.Close();

        //canvas.FillColor = Color.FromArgb("#BAE6FD");
        //canvas.FillPath(path);
        //canvas.StrokeColor = Color.FromArgb("#94A3B8");
        //canvas.StrokeSize = 2;
        canvas.DrawPath(path);
    }
}