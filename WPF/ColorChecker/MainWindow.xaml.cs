using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        private void setupPreview() {
            stockBorder = new List<Border>();
            for (int i = 0; i < stockGrid.RowDefinitions.Count; i++) {
                for (int j = 0; j < stockGrid.ColumnDefinitions.Count; j++) {
                    Border border = new Border();
                    border.Margin = new Thickness(3);
                    border.BorderBrush = Brushes.Gray;
                    border.BorderThickness = new Thickness(1);
                    border.ToolTip = $"R:255,G:255,B:255";  // 初期ToolTip
                    //border.Background = null;
                    border.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    border.MouseLeftButtonDown += (s, e) => {
                        Color color = ((SolidColorBrush)this.Background).Color;
                        if (e.ClickCount == 2) {
                            colorComboBox.SelectedIndex = -1;
                            colorPreview.Background = border.Background;
                            SolidColorBrush brush = (SolidColorBrush)border.Background;
                            redSlider.Value = brush.Color.R;
                            greenSlider.Value = brush.Color.G;
                            blueSlider.Value = brush.Color.B;
                            updateTextBox();
                        } else if (e.ClickCount == 1) {
                            updateSelectBorder(border);
                        }
                    };

                    stockGrid.Children.Add(border);
                    stockBorder.Add(border);
                }
            }
        }

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

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            Color color = Color.FromRgb((byte)redSlider.Value, (byte)greenSlider.Value, (byte)blueSlider.Value);
            colorPreview.Background = new SolidColorBrush(color);
            if (!isUpdating) {
                colorComboBox.SelectedIndex = -1;
            }
            updateTextBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            //shiftPanel();
            if (selectedBorder == null) return;
            selectedBorder.Background = colorPreview.Background;
            SolidColorBrush brush = (SolidColorBrush)selectedBorder.Background;
            selectedBorder.ToolTip = $"R:{brush.Color.R},G:{brush.Color.G},B:{brush.Color.B}";

        }
        //private void shiftPanel() {
        //    for (int i = stockBorder.Count - 1; i > 0; i--) {
        //        Border c = stockBorder[i];
        //        Border p = stockBorder[i - 1];
        //        c.Background = p.Background;
        //    }
        //}

        private void updateTextBox() {
            SolidColorBrush brush = (SolidColorBrush)colorPreview.Background;
            Color mycolor = brush.Color;
            color10code.Text = $"{mycolor.R},{mycolor.G},{mycolor.B}";
            color16code.Text = $"#{mycolor.R:X2}{mycolor.G:X2}{mycolor.B:X2}";
        }

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

        public static Dictionary<string, Brush> MakeBrushesDictionary() {
            //Brushesの情報(PublicとStaticなプロパティ全部)取得
            System.Reflection.PropertyInfo[] brushInfos =
                typeof(Brushes).GetProperties(
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Static);

            Dictionary<string, Brush> dict = new Dictionary<string, Brush>();
            foreach (var item in brushInfos) {
                if (item.GetValue(null) is Brush b) {
                    dict.Add(item.Name, b);
                }
            }
            return dict;
        }

        private void color10code_KeyDown(object sender, KeyEventArgs e) {

        }
    }
}
