using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings;
using System.Text.Encodings.Web;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var emp = new Employee {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };

            // 1
            saveSerializeObject<Employee>("q1.json", emp);


            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];

            // 2
            saveSerializeObject<Employee[]>("q2.json", employees);

            // 3
            Employee[]? loadObj = loadDeserializeObject<Employee[]>("q2.json");
            loadObj?.ToList().ForEach(Console.WriteLine);
        }



        private static void saveSerializeObject<T>(string path, T obj) {
            var opt = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string jsonStr = JsonSerializer.Serialize(obj, opt);
            File.WriteAllText(path, jsonStr);
        }

        private static T? loadDeserializeObject<T>(string path) {
            string jsonStr = File.ReadAllText(path);
            var opt = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<T>(jsonStr,opt);
        }
    }
}
