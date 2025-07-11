using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings;
using System.Text.Encodings.Web;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Employee employee = new Employee {
                Id = 0,
                Name = "Sota Yanagida",
                HireDate = new DateTime(1051, 2, 3)
            };

            // 1
            saveSerializeObject<Employee>("q1.json", employee);


            Employee[] employees = new Employee[]{
                new Employee{
                    Id = 1,
                    Name = "Alice Tanaka",
                    HireDate = new DateTime(2020, 5, 1)
                },
                new Employee{
                    Id = 2,
                    Name = "Bob Yamada",
                    HireDate = new DateTime(2019, 3, 15)
                },
                new Employee{
                    Id = 3,
                    Name = "Charlie Suzuki",
                    HireDate = new DateTime(2022, 11, 30)
                }
            };

            // 2
            saveSerializeObject<Employee[]>("q2.json", employees);

            // 3
            Employee[]? loadObj = loadDeserializeObject<Employee[]>("q2.json");
            loadObj?.ToList().ForEach(Console.WriteLine);
        }



        private static void saveSerializeObject<T>(string path, T obj) {
            var opt = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            string jsonStr = JsonSerializer.Serialize(obj, opt);
            File.WriteAllText(path, jsonStr);
        }

        private static T? loadDeserializeObject<T>(string path) {
            string jsonStr = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(jsonStr);
        }
    }
}
