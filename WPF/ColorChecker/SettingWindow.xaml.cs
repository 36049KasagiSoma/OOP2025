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
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window {
        public SettingWindow() {
            InitializeComponent();
            SettingData setting = JsonEvent.LoadItem<SettingData>("setting.json");
            if (setting != null) {
                rowSlider.Value = setting.RowCount;
                columnSlider.Value = setting.ColCount;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e) {
            JsonEvent.SaveItem("setting.json", new SettingData() {
                RowCount = (int)rowSlider.Value,
                ColCount = (int)columnSlider.Value
            });
            Close();
        } 
        private void Cancel_Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
