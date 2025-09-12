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
            LoadSaveData();
            InitializeComponent();
            slider_ValueChanged(null, null);
            colorComboBox.ItemsSource = MakeBrushesDictionary();
            setupPreview();
        }

        List<Border> stockBorder;           // Preview用のBorderリスト
        private bool isUpdating = false;    // コンボボックス更新中フラグ
        private Border selectedBorder;      // 選択中のBorder
        private int rowCount = 6;           // Borderの行数
        private int columnCount = 5;        // Borderの列数
        private SettingData settingData;    // 設定データ
        public const string settingFilePath = "setting.bin"; // 設定ファイルのパス
        private static string stockFilePath = "stocks.bin";  // ストックデータファイルのパス

        /// <summary>
        /// 設定データを読み込みます。
        /// </summary>
        private void LoadSaveData(){
            settingData = ObjectSaveAndLoad.LoadItem<SettingData>(settingFilePath);
            if (settingData != null) {
                rowCount = settingData.RowCount;
                columnCount = settingData.ColCount;
            }
        }

        // Preview用のBorderを生成
        private void setupPreview() {

            // Gridの行列を設定
            for (int i = 0; i < rowCount; i++) {
                stockGrid.RowDefinitions.Add(new RowDefinition());
            }
            for(int j = 0; j < columnCount; j++) {
                stockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Borderを生成してGridに追加
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

            // 保存データを読み込み
            if (File.Exists(stockFilePath)) {
                Color[] brush = ObjectSaveAndLoad.LoadItem<Color[]>(stockFilePath);
                if (brush != null) {
                    for (int i = 0; i < brush.Length && i < stockBorder.Count; i++) {
                        stockBorder[i].Background = new SolidColorBrush(brush[i]);
                        string colorName = getColorName(brush[i]);
                        updateBorderToolTip(stockBorder[i]);
                    }
                }
            }
        }

        /// <summary>
        /// ストック用のBorderを生成します。
        /// </summary>
        private Border setupBorder() {
            // Borderの基本設定
            Border border = new Border();
            border.Margin = new Thickness(3);
            border.BorderBrush = Brushes.Gray;
            border.BorderThickness = new Thickness(1);
            updateBorderToolTip(border);

            // 初期色:白
            border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            // 左クリック時
            border.MouseLeftButtonDown += (s, e) => {
                // シングルクリックは選択、ダブルクリックはプレビューに反映
                Color color = ((SolidColorBrush)this.Background).Color;
                if (e.ClickCount == 2) {
                    updatePreviewFromBorder(border);
                } else if (e.ClickCount == 1) {
                    updateSelectBorder(border);
                }
            };

            // ドラッグオーバー時
            border.DragEnter += (s, e) => {
                if (e.Data.GetDataPresent(typeof(Border))) {
                    // ドラッグ時に枠線をオレンジにする
                    Border sender = (Border)e.Data.GetData(typeof(Border));
                    if(sender == border) return;
                    border.BorderBrush = Brushes.Orange;
                    border.BorderThickness = new Thickness(3);
                }
            };

            // ドラッグリーブ時(ドラッグが外れたとき)
            border.DragLeave += (s, e) => {
                // 枠線を元に戻す
                Border sender = (Border)e.Data.GetData(typeof(Border));
                if (sender == border) return;
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(1);
            };

            // ドラッグ開始時
            border.MouseMove += (s, e) => {
                if (e.LeftButton == MouseButtonState.Pressed) {
                    if (border.Background is SolidColorBrush brush) {
                        // ドラッグBorderの枠線を青にしてドラッグ開始
                        border.BorderBrush = Brushes.Blue;
                        border.BorderThickness = new Thickness(3);
                        DragDrop.DoDragDrop(border, border, DragDropEffects.Copy);
                        // ドロップ完了後に枠線を元に戻す
                        border.BorderBrush = Brushes.Gray;
                        border.BorderThickness = new Thickness(1);
                        updateSelectBorder(selectedBorder);
                    }
                }
            };

            // ドロップ受け入れ設定
            border.AllowDrop = true;
            border.Drop += (s, e) => {
                if (e.Data.GetDataPresent(typeof(Border))) {
                    // ドロップされたBorderの色を入れ替える
                    Border sender = (Border)e.Data.GetData(typeof(Border));
                    Color newColor = ((SolidColorBrush)sender.Background).Color;
                    Color oldColor = ((SolidColorBrush)border.Background).Color;
                    border.Background = new SolidColorBrush(newColor);
                    sender.Background = new SolidColorBrush(oldColor);
                    // ツールチップと選択状態を更新
                    updateBorderToolTip(border);
                    updateBorderToolTip(sender);
                    selectedBorder = border;
                    updateSelectBorder(border);
                }
            };

            setupBorderContextMenu(border);
            return border;
        }

        /// <summary>
        /// 指定されたBorderのツールチップを更新します。
        /// </summary>
        private void updateBorderToolTip(Border border) {
            if (border.Background is SolidColorBrush brush) {
                Color color = brush.Color;
                border.ToolTip = $"R:{color.R},G:{color.G},B:{color.B}{getColorName(color)}";
            }
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
                // 10進コードをクリップボードにコピー
                Clipboard.SetText($"{color.R},{color.G},{color.B}");
            };
            item_copy_16code.Click += (s, e) => {
                Color color = ((SolidColorBrush)border.Background).Color;
                // 16進コードをクリップボードにコピー
                Clipboard.SetText($"#{color.R:X2}{color.G:X2}{color.B:X2}");
            };
            item_clear.Click += (s, e) => {
                // 色を白に戻す
                border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                updateBorderToolTip(border);
                // 選択状態を解除
                if (selectedBorder == border) selectedBorder = null;
                // 枠線を元に戻す
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(1);
            };
            item_clearAll.Click += (s, e) => {
                if (MessageBox.Show("すべての色をクリアします。よろしいですか？",
                    "確認", MessageBoxButton.YesNo, MessageBoxImage.Question)
                != MessageBoxResult.Yes) return;
                // すべての色を白に戻す
                foreach (Border b in stockBorder) {
                    b.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    updateBorderToolTip(b);
                    // 選択状態を解除
                    if (selectedBorder == b) selectedBorder = null;
                    // 枠線を元に戻す
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
            // コンボボックスの選択を解除
            colorComboBox.SelectedIndex = -1;
            // プレビューの色を更新
            colorPreview.Background = border.Background;
            SolidColorBrush brush = (SolidColorBrush)border.Background;
            // スライダーの値を更新
            redSlider.Value = brush.Color.R;
            greenSlider.Value = brush.Color.G;
            blueSlider.Value = brush.Color.B;
            // テキストボックス(10進・16進)を更新
            updateTextBox();
        }

        /// <summary>
        /// Borderの選択状態を更新します。
        /// </summary>
        /// <param name="highlight">選択状態にするBorder</param>
        private void updateSelectBorder(Border highlight) {
            foreach (Border b in stockBorder) {
                // 選択されたBorderの枠線を赤に、それ以外は灰色にする
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
            // 10進数コードと16進数コードを更新
            color10code.Text = $"{mycolor.R},{mycolor.G},{mycolor.B}";
            color16code.Text = $"#{mycolor.R:X2}{mycolor.G:X2}{mycolor.B:X2}";
        }

        /// <summary>
        /// ColorからBrushesクラスの名前を取得します。
        /// </summary>
        private string getColorName(Color color) {
            foreach (var item in MakeBrushesDictionary()) {
                SolidColorBrush brush = (SolidColorBrush)item.Value;
                if (brush.Color == color) {
                    // 連結用に先頭にスペースを付与して返す
                    return " " + item.Key;
                }
            }
            return "";
        }

        // カラースライダーの値変化時のイベント
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Color color = Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value);
            colorPreview.Background = new SolidColorBrush(color);
            if (!isUpdating) { // コンボボックスの更新中は選択解除しない
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
            updateBorderToolTip(selectedBorder);

        }

        // コンボボックスの選択変更時のイベント
        private void colorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorComboBox.SelectedItem == null) return; // 選択解除時は何もしない

            isUpdating = true; // スライダーのイベントで選択解除されないようにロックする。
            // 選択された色をプレビューに反映
            var selectItem = (KeyValuePair<string, Brush>)((ComboBox)sender).SelectedItem;
            SolidColorBrush brush = (SolidColorBrush)selectItem.Value;
            Color mycolor = brush.Color;
            colorPreview.Background = new SolidColorBrush(mycolor);
            // スライダーの値を更新
            redSlider.Value = mycolor.R;
            greenSlider.Value = mycolor.G;
            blueSlider.Value = mycolor.B;
            updateTextBox();
            isUpdating = false; // ロックの解除
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

                // 3つに分割できなかった場合はエラー
                if (rgb.Length != 3) {
                    MessageBox.Show("不正な数値形式です。", "形式エラー", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }

                // それぞれの値をbyte(0～255)に変換できなかった場合はエラー
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
                // 先頭に#がなければ付与
                if (!hex.StartsWith("#")) {
                    color16code.Text = "#" + hex;
                    hex = "#" + hex;
                }

                // #RRGGBB形式でなければエラー
                if (!Regex.IsMatch(hex, @"^#[0-9a-fA-F]{6}$")) {
                    MessageBox.Show("不正な数値形式です。", "形式エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                isUpdating = true;
                // 16進数コードをColorに変換してプレビューに反映
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
            SaveColors();
        }

        // 色のファイル保存
        private void SaveColors() {
            // Borderの色をColor配列に変換して保存  
            Color[] brush = stockBorder.Select(b => ((SolidColorBrush)b.Background).Color).ToArray();
            ObjectSaveAndLoad.SaveItem(stockFilePath, brush);
        }

        // メニューイベント（About、Setting）
        private void About_MenuItem_Click(object sender, RoutedEventArgs e) {
            Window about = new AboutWindow();
            about.ShowDialog();
        }
        private void Setting_MenuItem_Click(object sender, RoutedEventArgs e) {
            Window setting = new SettingWindow();
            setting.ShowDialog();
            LoadSaveData();
            SaveColors();
            stockGrid.Children.Clear();
            stockGrid.RowDefinitions.Clear();
            stockGrid.ColumnDefinitions.Clear();
            setupPreview();
        }
    }
}
