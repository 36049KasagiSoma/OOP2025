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

            Console.WriteLine(message);
            while (true) {
                Console.Write("都道府県:");  //①都道府県の入力
                pref = Console.ReadLine();

                if (pref is null) return;    //無限ループを抜ける(Ctrl + 'Z')

                Console.Write("県庁所在地:");//県庁所在地の入力
                prefCaptalLocation = Console.ReadLine();

                //既に都道府県が登録されているか？
                if (prefOfficeDict.ContainsKey(pref)) {

                    //登録済みなら確認して上書き処理、上書きしない場合はもう一度都道府県の入力…①へ
                    if (!GetAns("上書きしますか？(Y/N):")) {
                        Console.WriteLine($"登録をキャンセルしました。");
                        Console.WriteLine();//改行
                        continue;
                    } else {
                        Console.WriteLine($"要素[{pref}]を上書きしました。");
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
                    case SelectNemu.ALL_DISP://一覧出力処理
                        allDisp();
                        break;
                    case SelectNemu.SEARCH://検索処理
                        searchPrefCaptalLocation();
                        break;
                    case SelectNemu.EXIT://無限ループを抜ける
                        Console.WriteLine("終了しました。");
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
        private static SelectNemu menuDisp() {
            Console.WriteLine("\n**** メニュー ****");
            Console.WriteLine("1：一覧表示");
            Console.WriteLine("2：検索");
            Console.WriteLine("9：終了");
            Console.Write(">");
            try {
                var menuSelect = (SelectNemu)int.Parse(Console.ReadLine());
                return menuSelect;
            } catch (Exception e) {
                return SelectNemu.NONE;
            }
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
            if (searchPref != null && prefOfficeDict.TryGetValue(searchPref, out string? value)) {
                Console.WriteLine($"検索結果:{searchPref}:{value}");
            } else {
                Console.WriteLine("検索結果:該当なし");
            }
        }

        public enum SelectNemu {
            ALL_DISP = 1, SEARCH = 2, EXIT = 9, NONE = -1,
        }

    }
}
