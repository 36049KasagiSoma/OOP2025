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

namespace SampleApplication {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            setupDefaultSelection();
        }

        // 初期値の設定
        private void setupDefaultSelection() {
            checkBox.IsChecked = false;
            redRadioButton.IsChecked = true;
            seasonConboBox.SelectedIndex = 0;
            checkBox_Unchecked(checkBox, null);
            radioButton_Checked(redRadioButton, null);
            seasonTextBlock.Text = ((ComboBoxItem)seasonConboBox.SelectedItem).Content.ToString();
        }

        private void seasonConboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            seasonTextBlock.Text = ((ComboBoxItem)seasonConboBox.SelectedItem).Content.ToString();
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e) {
            if (!(sender is RadioButton)) {
                Console.Error.WriteLine("sender is not RadioButton.");
                Console.Error.WriteLine($"sender:{sender ?? "null"}");
                Console.Error.WriteLine($"event:{e.ToString()}");
                return;
            }
            colorText.Text = ((RadioButton)sender).Content.ToString();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e) {
            chackbocTextBlock.Text = "チェックON";
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e) {
            chackbocTextBlock.Text = "チェックOFF";
        }
    }
}
