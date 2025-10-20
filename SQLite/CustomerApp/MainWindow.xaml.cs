using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace CustomerApp {
    public partial class MainWindow : Window {
        private ICollection<Customer> _customers = new ObservableCollection<Customer>();

        public MainWindow() {
            InitializeComponent();
            ReadDatabace();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            byte[]? byteArray = null;
            if (CustomerImageView.Source is not null) {
                BitmapEncoder encoder = new JpegBitmapEncoder();
                BitmapImage image = (BitmapImage)CustomerImageView.Source;
                encoder.Frames.Add(BitmapFrame.Create(image));
                using (MemoryStream ms = new()) {
                    encoder.Save(ms);
                    byteArray = ms.ToArray();
                }
            }

            var customer = new Customer {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = byteArray
            };

            _customers.Add(customer);
            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Customer>();
                connection.Insert(customer);
            }
            ResetField();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var item = CustomerListView.SelectedItem as Customer;
            if (item is null) return;
            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Customer>();
                connection.Delete(item); // 削除

                ReadDatabace();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) {
            if (CustomerListView.SelectedItem is null) return;
            var customer = new Customer {
                Id = ((Customer)CustomerListView.SelectedItem).Id,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text
            };

            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Customer>();
                connection.Update(customer);
            }
            ResetField();
            ReadDatabace();
        }

        private void ResetField() {
            NameTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            ImagePathTextBox.Text = string.Empty;
            CustomerImageView.Source = null;
        }

        private void ReadDatabace() {
            _customers.Clear();
            foreach (Customer c in GetCustomers()) {
                _customers.Add(c);
            }
            CustomerListView.ItemsSource = _customers;
        }

        private ICollection<Customer> GetCustomers() {
            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Customer>();
                return connection.Table<Customer>().ToList();
            }
        }

        private void ImageSelectButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "ファイル選択ダイアログ";
            openFileDialog.Filter = "画像ファイル(*.png,*.jpg,*.jpeg)|*.png;*.jpg;*.jpeg";

            //ファイル選択ダイアログを開く
            if (openFileDialog.ShowDialog() == true) {
                CustomerImageView.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                ImagePathTextBox.Text = openFileDialog.FileName;
            }

        }

        private void ImagePathTextBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            try {
                CustomerImageView.Source = new BitmapImage(new Uri(ImagePathTextBox.Text));
            } catch (Exception ex) {
                MessageBox.Show($"画像読み込み時にエラーが発生しました。\r\n{ex.Message}", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string t = NameTextBox.Text + PhoneTextBox.Text + AddressTextBox.Text;
            if (t != string.Empty && CustomerImageView.Source != null &&
                MessageBox.Show("フィールドに値が入力されています。上書きしますか?", "確認", MessageBoxButton.YesNo, MessageBoxImage.Information)
                != MessageBoxResult.OK) return;
            Customer? c = CustomerListView.SelectedItem as Customer;
            if (c is not null) {
                SetParam(c);
            }
        }

        private void SetParam(Customer c) {
            if (c == null) return;
            NameTextBox.Text = c.Name;
            PhoneTextBox.Text = c.Phone;
            AddressTextBox.Text = c.Address;
            if (c.Picture is not null) {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(c.Picture)) {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                }

                // なぜか更新されない。なぜ？                      
                CustomerImageView.Source = image;
            }
        }

    }
}