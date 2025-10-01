using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ConverterApp {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        UnitObject[] units;
        public MainWindow() {
            InitializeComponent();
            units = new UnitObject[] {
                new UnitObject("mm", 0.001),
                new UnitObject("cm", 0.01),
                new UnitObject("m", 1.0),
                new UnitObject("km", 1000.0),
            };
            setupComboBox(units);
        }


        private void setupComboBox(UnitObject[] u) {
            foreach (var unit in u) {
                MetricUnit.Items.Add(unit);
                ImperialUnit.Items.Add(unit);
            }
            MetricUnit.SelectedIndex = 0;
            ImperialUnit.SelectedIndex = 1;
        }

        class UnitObject {
            public string Name { get; set; }
            public double Value { get; set; }
            public UnitObject(string name, double value) {
                Name = name;
                Value = value;
            }

            public override string ToString() {
                return Name;
            }
        }

        private void MetricImperialUnit_Click(object sender, RoutedEventArgs e) {
            UnitObject to = MetricUnit.SelectedItem is UnitObject metric ? metric : null;
            UnitObject from = ImperialUnit.SelectedItem is UnitObject imperial ? imperial : null;
            SetCalcVavlueText(MetricValue, to, ImperialValue, from);
        }

        private void ImperialUnitToMetric_Click(object sender, RoutedEventArgs e) {
            UnitObject from = MetricUnit.SelectedItem is UnitObject metric ? metric : null;
            UnitObject to = ImperialUnit.SelectedItem is UnitObject imperial ? imperial : null;
            SetCalcVavlueText(ImperialValue, to, MetricValue, from);
        }

        private void SetCalcVavlueText(TextBox toTextBox, UnitObject toUnit, TextBox fromTextBox, UnitObject fromUnit) {
            if (double.TryParse(toTextBox.Text, out double metricValue)) {
                double? magnifier = CalcUnit(metricValue, toUnit, fromUnit);
                if (magnifier.HasValue) {
                    fromTextBox.Text = magnifier.Value.ToString();
                } else {
                    MessageBox.Show($"変換できませんでした。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } else {
                MessageBox.Show("値を解析できませんでした。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double? CalcUnit(double value, UnitObject toUnit, UnitObject fromUnit) {
            if (toUnit == null || fromUnit == null) {
                return null;
            }
            double to = toUnit.Value;
            double from = fromUnit.Value;

            double magnifier = to / from;
            return value * magnifier;

        }

        private void textBoxPrice_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            // 0-9もしくは「.」のみ
            e.Handled = !new Regex("[0-9]|\\.").IsMatch(e.Text);
        }
        private void textBoxPrice_PreviewExecuted(object sender, ExecutedRoutedEventArgs e) {
            // 貼り付けを許可しない
            if (e.Command == ApplicationCommands.Paste) {
                e.Handled = true;
            }
        }
    }
}