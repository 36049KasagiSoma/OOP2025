using System.Configuration;
using System.Data;
using System.Windows;

namespace Sample {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        const string databaceName = "Persons.db";
        static readonly string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static readonly string databacePath = System.IO.Path.Combine(folderPath, databaceName);
    }

}
