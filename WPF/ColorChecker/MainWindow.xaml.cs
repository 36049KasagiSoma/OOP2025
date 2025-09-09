using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            slider_ValueChanged(null, null);
            colorComboBox.ItemsSource = MakeBrushesDictionary();
            setupPreview();
        }

        List<Border> stockBorder;           // Preview用のBorderリスト
        private bool isUpdating = false;    // コンボボックス更新中フラグ
        private Border selectedBorder;      // 選択中のBorder

        // Preview用のBorderを生成
        private void setupPreview() { 
            stockBorder = new List<Border>();
            for (int i = 0; i < stockGrid.RowDefinitions.Count; i++) {
                for (int j = 0; j < stockGrid.ColumnDefinitions.Count; j++) {
                    Border border = setupBorder();
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    stockGrid.Children.Add(border);
                    stockBorder.Add(border);
                }
            }

            //Jsonから保存データを読み込み
            if (File.Exists("stocks.json")) {
                Color[] brush = JsonEvent.LoadItem<Color[]>("stocks.json");
                if (brush != null) {
                    for (int i = 0; i < brush.Length && i < stockBorder.Count; i++) {
                        stockBorder[i].Background = new SolidColorBrush(brush[i]);
                        stockBorder[i].ToolTip = $"R:{brush[i].R},G:{brush[i].G},B:{brush[i].B}";
                    }
                }
            }
        }

        /// <summary>
        /// ストック用のBorderを生成します。
        /// </summary>
        private Border setupBorder() {
            Border border = new Border();
            border.Margin = new Thickness(3);
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            border.ToolTip = $"R:255,G:255,B:255";  // 初期ToolTip

            border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            border.MouseLeftButtonDown += (s, e) => {
                Color color = ((SolidColorBrush)this.Background).Color;
                if (e.ClickCount == 2) {
                    updatePreviewFromBorder(border);
                } else if (e.ClickCount == 1) {
                    updateSelectBorder(border);
                }
            };

            setupBorderContextMenu(border);
            return border;
        }

        /// <summary>
        /// 指定されたBorderに右クリックメニューを設定します。
        /// </summary>
        private void setupBorderContextMenu(Border border) {
            /*
            * 右クリック
            *   L 選択
            *   L 反映
            *   L コピー
            *     L 10進コード
            *     L 16進コード
            *   --------------------
            *   L クリア
            *   L すべてクリア
            */
            ContextMenu contextMenu = new ContextMenu(); // 右クリックメニュー
            MenuItem item_select = new MenuItem { Header = "選択" };
            MenuItem item_reflection = new MenuItem { Header = "反映" };
            MenuItem item_copy = new MenuItem { Header = "コピー" };
            MenuItem item_copy_10code = new MenuItem { Header = "10進コード" };
            MenuItem item_copy_16code = new MenuItem { Header = "16進コード" };
            MenuItem item_clear = new MenuItem { Header = "クリア" };
            MenuItem item_clearAll = new MenuItem { Header = "すべてクリア" };

            // アイコン設定
            item_select.Icon = new Image {
                Source = new BitmapImage(new Uri("pack://application:,,,/ColorChecker;component/Image/selectIcon.png", UriKind.Absolute)),
                Width = 16,
                Height = 16
            };
            item_reflection.Icon = new Image {
                Source = new BitmapImage(new Uri("pack://application:,,,/ColorChecker;component/Image/checkIcon.png", UriKind.Absolute)),
                Width = 16,
                Height = 16
            };
            item_copy.Icon = new Image {
                Source = new BitmapImage(new Uri("pack://application:,,,/ColorChecker;component/Image/copyIcon.png", UriKind.Absolute)),
                Width = 16,
                Height = 16
            };
            item_clear.Icon = new Image {
                Source = new BitmapImage(new Uri("pack://application:,,,/ColorChecker;component/Image/clearIcon.png", UriKind.Absolute)),
                Width = 16,
                Height = 16
            };
            item_clearAll.Icon = new Image {
                Source = new BitmapImage(new Uri("pack://application:,,,/ColorChecker;component/Image/clearAllIcon.png", UriKind.Absolute)),
                Width = 16,
                Height = 16
            };

            // イベント設定
            item_select.Click += (s, e) => updateSelectBorder(border);
            item_reflection.Click += (s, e) => {
                updateSelectBorder(border);         // 選択して
                updatePreviewFromBorder(border);    // 色を反映
            };
            item_copy_10code.Click += (s, e) => {
                Color color = ((SolidColorBrush)border.Background).Color;
                Clipboard.SetText($"{color.R},{color.G},{color.B}");
            };
            item_copy_16code.Click += (s, e) => {
                Color color = ((SolidColorBrush)border.Background).Color;
                Clipboard.SetText($"#{color.R:X2}{color.G:X2}{color.B:X2}");
            };
            item_clear.Click += (s, e) => {
                border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                border.ToolTip = $"R:255,G:255,B:255";
                if (selectedBorder == border) selectedBorder = null;
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(1);
            };
            item_clearAll.Click += (s, e) => {
                if (MessageBox.Show("すべての色をクリアします。よろしいですか？",
                    "確認", MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes) return;
                foreach (Border b in stockBorder) {
                    b.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    b.ToolTip = $"R:255,G:255,B:255";
                    if (selectedBorder == b) selectedBorder = null;
                    b.BorderBrush = Brushes.Gray;
                    b.BorderThickness = new Thickness(1);
                }
            };

            // メニューに追加
            contextMenu.Items.Add(item_select);
            contextMenu.Items.Add(item_reflection);
            contextMenu.Items.Add(item_copy);
            item_copy.Items.Add(item_copy_10code);
            item_copy.Items.Add(item_copy_16code);
            contextMenu.Items.Add(new Separator());
            contextMenu.Items.Add(item_clear);
            contextMenu.Items.Add(item_clearAll);
            border.ContextMenu = contextMenu;
        }

        /// <summary>
        /// ストックのBorderからプレビューを更新します。
        /// </summary>
        /// <param name="border"></param>
        private void updatePreviewFromBorder(Border border) {
            colorComboBox.SelectedIndex = -1;
            colorPreview.Background = border.Background;
            SolidColorBrush brush = (SolidColorBrush)border.Background;
            redSlider.Value = brush.Color.R;
            greenSlider.Value = brush.Color.G;
            blueSlider.Value = brush.Color.B;
            updateTextBox();
        }

        /// <summary>
        /// Borderの選択状態を更新します。
        /// </summary>
        /// <param name="highlight">選択状態にするBorder</param>
        private void updateSelectBorder(Border highlight) {
            foreach (Border b in stockBorder) {
                if (b == highlight) {
                    b.BorderBrush = Brushes.Red;
                    b.BorderThickness = new Thickness(2);
                    selectedBorder = b;
                } else {
                    b.BorderBrush = Brushes.Gray;
                    b.BorderThickness = new Thickness(1);
                }
            }
        }

        /// <summary>
        /// カラーコード表示用のTextBoxを更新します。
        /// </summary>
        private void updateTextBox() {
            SolidColorBrush brush = (SolidColorBrush)colorPreview.Background;
            Color mycolor = brush.Color;
            color10code.Text = $"{mycolor.R},{mycolor.G},{mycolor.B}";
            color16code.Text = $"#{mycolor.R:X2}{mycolor.G:X2}{mycolor.B:X2}";
        }

        // カラースライダーの値変化時のイベント
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Color color = Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value);
            colorPreview.Background = new SolidColorBrush(color);
            if (!isUpdating) {
                colorComboBox.SelectedIndex = -1;
            }
            updateTextBox();
        }

        // STOCKボタン押下時のイベント
        private void Button_Click(object sender, RoutedEventArgs e) {
            if (selectedBorder == null) {
                MessageBox.Show("色を設定する枠を選択してください。", "情報", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            selectedBorder.Background = colorPreview.Background;
            SolidColorBrush brush = (SolidColorBrush)selectedBorder.Background;
            selectedBorder.ToolTip = $"R:{brush.Color.R},G:{brush.Color.G},B:{brush.Color.B}";

        }

        // コンボボックスの選択変更時のイベント
        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorComboBox.SelectedItem == null) return;
            isUpdating = true;
            var selectItem = (KeyValuePair<string, Brush>)((ComboBox)sender).SelectedItem;
            SolidColorBrush brush = (SolidColorBrush)selectItem.Value;
            Color mycolor = brush.Color;
            colorPreview.Background = new SolidColorBrush(mycolor);
            redSlider.Value = mycolor.R;
            greenSlider.Value = mycolor.G;
            blueSlider.Value = mycolor.B;
            updateTextBox();
            isUpdating = false;
        }

        /// <summary>
        /// テンプレートのBrushesクラスからブラシの辞書を生成します。
        /// </summary>
        public static Dictionary<string, Brush> MakeBrushesDictionary() {
            //Brushesの情報(PublicとStaticなプロパティ全部)取得
            PropertyInfo[] brushInfos =
                typeof(Brushes).GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Static);

            Dictionary<string, Brush> dict = new Dictionary<string, Brush>();
            foreach (var item in brushInfos) {
                if (item.GetValue(null) is Brush b) {
                    dict.Add(item.Name, b);
                }
            }
            return dict;
        }

        // 10進数コードのTextBoxでEnterキー押下時のイベント
        private void color10code_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                string[] rgb = color10code.Text.Split(',');
                if (rgb.Length != 3) {
                    MessageBox.Show("不正な数値形式です。", "形式エラー", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
                if (byte.TryParse(rgb[0], out byte r) &&
                    byte.TryParse(rgb[1], out byte g) &&
                    byte.TryParse(rgb[2], out byte b)) {
                    isUpdating = true;
                    colorPreview.Background = new SolidColorBrush(Color.FromRgb(r, g, b));
                    redSlider.Value = r;
                    greenSlider.Value = g;
                    blueSlider.Value = b;
                    colorComboBox.SelectedIndex = -1;
                    isUpdating = false;
                } else {
                    MessageBox.Show("不正な数値形式です。", "形式エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // 16進数コードのTextBoxでEnterキー押下時のイベント
        private void color16code_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                string hex = color16code.Text.Trim();
                if (!hex.StartsWith("#")) {
                    color16code.Text = "#" + hex;
                    hex = "#" + hex;
                }
                if (!Regex.IsMatch(hex, @"^#[0-9a-fA-F]{6}$")) {
                    MessageBox.Show("不正な数値形式です。", "形式エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                isUpdating = true;
                object obj = ColorConverter.ConvertFromString(hex);
                var color = (Color)obj;
                colorPreview.Background = new SolidColorBrush(color);
                redSlider.Value = color.R;
                greenSlider.Value = color.G;
                blueSlider.Value = color.B;
                colorComboBox.SelectedIndex = -1;
                isUpdating = false;
            }
        }

        // コピー用ボタン押下時のイベント
        private void Button_10Code_Copy_Click(object sender, RoutedEventArgs e)
            => Clipboard.SetText(color10code.Text);
        private void Button_16Code_Copy_Click(object sender, RoutedEventArgs e)
            => Clipboard.SetText(color16code.Text);

        // ウィンドウ終了時のイベント
        private void Window_Closed(object sender, EventArgs e) {
            Color[] brush = stockBorder.Select(b => ((SolidColorBrush)b.Background).Color).ToArray();
            JsonEvent.SaveItem("stocks.json", brush);
        }
    }
}
