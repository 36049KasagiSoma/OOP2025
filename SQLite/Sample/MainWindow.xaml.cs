using Sample.Data;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Sample {
    public partial class MainWindow : Window {
        private ICollection<Person> _persons = new ObservableCollection<Person>();
        public static int windowCnt = 0;

        public MainWindow() {
            InitializeComponent();

            ReadDatabace();
            //RandomWindowSize();
        }

        private async void RandomWindowSize() {
            double l = Left;
            double t = Top;
            Left = l + Random.Shared.Next(-500, 500);
            Top = t + Random.Shared.Next(-500, 500);

            await Task.Run(() => {
                for (int i = 0; i < Random.Shared.Next(10, 100); i++) {
                    Dispatcher.Invoke(() => {
                        Width = Random.Shared.Next(10, 1000);
                        Height = Random.Shared.Next(10, 1000);
                    });
                    Thread.Sleep(100);
                }
                Dispatcher.Invoke(() => {
                    new MainWindow().Show();
                    if (windowCnt < 10) {
                        new MainWindow().Show();
                        windowCnt++;
                    }
                    Close();
                });
            });
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            var person = new Person {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text
            };

            _persons.Add(person);
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e) {
            ReadDatabace();
        }

        private ICollection<Person> GetPersons() {
            using (var connection = new SQLiteConnection(App.databacePath)) {
                connection.CreateTable<Person>();
                return connection.Table<Person>().ToList();
            }
        }

        private void ReadDatabace() {
            _persons.Clear();
            foreach(Person p in GetPersons()) {
                _persons.Add(p);
            }
            PersonListView.ItemsSource = _persons;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            //using (var connection = new SQLiteConnection(App.databacePath)) {
            //    connection.CreateTable<Person>();
            //    foreach (Person p in _persons) {
            //        if(connection.Find<Person>(p.Id) is null) {
            //            connection.Insert(p);
            //        } else {
            //            connection.Update(p);
            //        }
            //    }
            //}
        }
    }
}