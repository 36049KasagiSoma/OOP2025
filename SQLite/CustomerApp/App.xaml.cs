using System.Configuration;
using System.Data;
using System.Windows;

namespace CustomerApp {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        const string databaceName = "Customers.db";
        static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static readonly string databacePath = System.IO.Path.Combine(folderPath, databaceName);
    }

}
