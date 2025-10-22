using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
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
using static System.Net.Mime.MediaTypeNames;


namespace CustomerApp {
    public partial class MainWindow : Window {
        private ICollection<Customer> _customers = new ObservableCollection<Customer>();
        private Customer? _selectCus = null;
        private int selectIndex = -1;
        private bool isUpdate = false;
        private bool isChangeImage = false;

        public MainWindow() {
            InitializeComponent();
            ReadDatabace();
        }

        private byte[]? getViewImageToByteArray() {
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
            return byteArray;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (NameTextBox.Text.Trim() == string.Empty) {
                NameTextBox.Focus();
                NameTextBox.SelectAll();
                MessageBox.Show("名前は必須です。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            byte[]? byteArray = getViewImageToByteArray();

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
            var item = CustomerListView.SelectedItems;
            if (item is null || item.Count == 0) return;
            if (MessageBox.Show($"{item.Count}件の選択された要素を削除しますか?", "確認", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Customer>();
                foreach (var customer in item) {
                    var cus = customer as Customer;
                    if (cus != null) {
                        connection.Delete(cus); // 削除
                    }
                }
            }
            ResetField();
            ReadDatabace();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) {
            if (CustomerListView.SelectedItem is null) return;
            if (NameTextBox.Text.Trim() == string.Empty) {
                NameTextBox.Focus();
                NameTextBox.SelectAll();
                MessageBox.Show("名前は必須です。", "入力エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var cus = (CustomerListView.SelectedItem as Customer) ?? new Customer();
            int targetId = cus.Id;
            byte[]? byteArray = getViewImageToByteArray();

            var customer = new Customer {
                Id = targetId,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = byteArray
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
            AddressTextBox.Text = string.Empty;
            ImagePathTextBox.Text = string.Empty;
            CustomerImageView.Source = null;
            _selectCus = null;
            selectIndex = -1;
            isChangeImage = false;
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
            string? path = ShowOpenFileDiarog("画像ファイル(*.png,*.jpg,*.jpeg)|*.png;*.jpg;*.jpeg");

            if (path is not null) {
                CustomerImageView.Source = new BitmapImage(new Uri(path));
                ImagePathTextBox.Text = path;
                isChangeImage = true;
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
            if (isUpdate) return;
            if (CustomerListView.SelectedItems.Count > 1) return;

            Customer? c = CustomerListView.SelectedItem as Customer;
            Customer old = new Customer {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
            };
            if (c is not null) {
                if (_selectCus != null && (!_selectCus.EqualsParam(old) || isChangeImage)) {

                    bool isReturn = MessageBox.Show("フィールドの値が変更されています。新しく選択した要素で上書きしますか?", "確認", MessageBoxButton.YesNo, MessageBoxImage.Information)
                        == MessageBoxResult.Yes;
                    if (!isReturn) {
                        isUpdate = true;
                        CustomerListView.SelectedIndex = selectIndex;
                        isUpdate = false;
                        return;
                    }

                }
                SetParam(c);
                _selectCus = c;
                selectIndex = CustomerListView.SelectedIndex;
                isChangeImage = false;
            }
        }

        private void SetParam(Customer c) {
            ResetField();
            if (c == null) return;
            NameTextBox.Text = c.Name;
            PhoneTextBox.Text = c.Phone;
            AddressTextBox.Text = c.Address;
            if (c.Picture is not null) {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(c.Picture)) {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                }
                CustomerImageView.Source = image;
            }
        }

        private void JsonExp_Click(object sender, RoutedEventArgs e) {
            string? path = ShowSaveFileDiarog("JSONファイル(*.json)|*.json|すべてのファイル (*.*)|*.*");
            if (path == null) return;
            var opt = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonText = JsonSerializer.Serialize(_customers, opt);
            File.WriteAllText(path, jsonText);
        }

        private void DbExp_Click(object sender, RoutedEventArgs e) {
            string? path = ShowSaveFileDiarog("DBファイル(*.db)|*.db|すべてのファイル (*.*)|*.*");
            if (path == null) return;
            File.Copy(App.databacePath, path, true);
        }

        private string? ShowSaveFileDiarog(string filter) {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Title = "ファイル保存ダイアログ";
            saveFileDialog.Filter = filter;


            //ファイル選択ダイアログを開く
            if (saveFileDialog.ShowDialog() ?? false) {
                return saveFileDialog.FileName;
            }
            return null;
        }
        private string? ShowOpenFileDiarog(string filter) {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "ファイル選択ダイアログ";
            openFileDialog.Filter = filter;


            //ファイル選択ダイアログを開く
            if (openFileDialog.ShowDialog() ?? false) {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void FileInp_Click(object sender, RoutedEventArgs e) {
            string? path = ShowOpenFileDiarog("すべてのファイル(*.*)|*.*|JSONファイル(*.json)|*.json|DBファイル(*.db)|*.db");
            try {
                if (path is not null) {
                    string exp = path.Substring(path.LastIndexOf(".") + 1).ToLower();
                    var Customers = new List<Customer>();
                    List<Customer>? cusList = null;
                    switch (exp) {
                        case "json":
                            var text = File.ReadAllText(path);
                            var opt = new JsonSerializerOptions {
                                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                                WriteIndented = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            };
                            cusList = JsonSerializer.Deserialize<List<Customer>>(text, opt);
                            break;
                        case "db":
                            using (var connection = new SQLiteConnection(path)) {
                                connection.CreateTable<Customer>();
                                cusList = connection.Table<Customer>().ToList();
                            }
                            break;
                    }
                    if (cusList is null || cusList.Count == 0) throw new Exception("顧客リストが存在しません。");
                    using (var connection = new SQLiteConnection(App.databacePath)) {
                        connection.CreateTable<Customer>();
                        foreach (Customer cs in cusList) {
                            Customer customer = new Customer { // Idを新しく振る
                                Name = cs.Name,
                                Phone = cs.Phone,
                                Address = cs.Address,
                                Picture = cs.Picture,
                            };
                            _customers.Add(customer);
                            connection.Insert(customer);
                        }
                        CustomerListView.ItemsSource = _customers;
                    }
                }
            } catch (Exception ex) {
                MessageBox.Show("インポート中にエラーが発生しました。\nエラー:" + ex.Message, "インポートエラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}