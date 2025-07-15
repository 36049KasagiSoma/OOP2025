using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Xml;
using System.Xml.Serialization;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var employees = Deserialize("employees.json");
            if (employees is not null) {
                ToXmlFile(employees);
                Console.WriteLine("変換完了");
            } else {
                Console.WriteLine("empはnull");
            }
        }

        static Employee[]? Deserialize(string path) {
            string text = File.ReadAllText(path);   
            var opt = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                NumberHandling =
                JsonNumberHandling.AllowNamedFloatingPointLiterals |
                JsonNumberHandling.AllowReadingFromString,
            };

            return JsonSerializer.Deserialize<Employee[]>(text, opt);
        }

        static void ToXmlFile(Employee[] employees) {
            using (var writer = XmlWriter.Create("employees.xml")) {
                XmlRootAttribute root = new XmlRootAttribute {
                    ElementName = "Employees"
                };
                var ser = new XmlSerializer(employees.GetType(), root);
                ser.Serialize(writer, employees);
            }

        }
    }

    public record Employee {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime HireDate { get; set; }
    }
}
