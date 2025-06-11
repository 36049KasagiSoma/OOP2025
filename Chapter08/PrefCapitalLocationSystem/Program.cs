namespace PrefCapitalLocationSystem {
    internal class Program {
        static private Dictionary<string, string> prefOfficeDict = new Dictionary<string, string>();

        static void Main(string[] args) {

            //入力処理
            Registration("県庁所在地の登録【入力終了：Ctrl + 'Z'】");

            //メニュー処理
            MenuEvents();


        }

        private static void Registration(string message) {
            string? pref, prefCaptalLocation;

            Console.WriteLine("県庁所在地の登録【入力終了：Ctrl + 'Z'】");
            while (true) {
                //①都道府県の入力
                Console.Write("都道府県:");
                pref = Console.ReadLine();

                if (pref is null) return;    //無限ループを抜ける(Ctrl + 'Z')

                //県庁所在地の入力
                Console.Write("県庁所在地:");
                prefCaptalLocation = Console.ReadLine();

                //既に都道府県が登録されているか？
                //ヒント：ContainsKeyを使用して調べる
                if (prefOfficeDict.ContainsKey(pref)) {

                    //登録済みなら確認して上書き処理、上書きしない場合はもう一度都道府県の入力…①へ
                    if (!GetAns("上書きしますか？(Y/N):")) {
                        Console.WriteLine($"登録をキャンセルしました。");
                        Console.WriteLine();//改行
                        continue;
                    } else {
                        Console.WriteLine($"要素{pref}を上書きしました。");
                    }

                }

                //県庁所在地登録処理

                prefOfficeDict[pref] = prefCaptalLocation ?? "";

                Console.WriteLine();//改行
            }
        }

        private static void MenuEvents() {
            while (true) {
                switch (menuDisp()) {
                    case "1"://一覧出力処理
                        allDisp();
                        break;


                    case "2"://検索処理
                        searchPrefCaptalLocation();
                        break;


                    case "9"://無限ループを抜ける
                        return;
                }
            }
        }


        private static bool GetAns(string message) {
            while (true) {
                Console.Write(message);
                string? ans = Console.ReadLine();
                if (ans != null) {
                    string tmp = ans.ToUpper();
                    if (tmp.Equals("N")) return false;
                    if (tmp.Equals("Y")) return true;
                }
            }
        }

        //メニュー表示
        private static string? menuDisp() {
            Console.WriteLine("\n**** メニュー ****");
            Console.WriteLine("1：一覧表示");
            Console.WriteLine("2：検索");
            Console.WriteLine("9：終了");
            Console.Write(">");
            var menuSelect = Console.ReadLine();
            return menuSelect;
        }


        //一覧表示処理
        private static void allDisp() {
            Console.WriteLine("=== 一覧 ====================");
            foreach (var pair in prefOfficeDict) {
                Console.WriteLine($"{pair.Key}:{pair.Value}");
            }
            Console.WriteLine("=============================");
        }

        //検索処理
        private static void searchPrefCaptalLocation() {
            Console.Write("都道府県:");
            String? searchPref = Console.ReadLine();
            if (searchPref != null && prefOfficeDict.ContainsKey(searchPref)) {
                Console.WriteLine($"検索結果:{searchPref}:{prefOfficeDict[searchPref]}");
            } else {
                Console.WriteLine("検索結果:該当なし");
            }
        }
    }
}
