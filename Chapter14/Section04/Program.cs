using System.Threading.Tasks;

namespace Section04 {
    internal class Program {
        static async Task Main(string[] args) {
            await GetHtmlExample(new HttpClient());
        }

        static async Task GetHtmlExample(HttpClient client) {
            var url = "https://abehiroshi.la.coocan.jp/";
            var text= await client.GetStringAsync(url);
            Console.WriteLine(text);
        }
    }
}
