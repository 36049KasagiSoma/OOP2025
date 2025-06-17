namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);
        }

        //メソッドの概要：
        //引数で指定されたパスのファイルから「,」区切りのデータを読み取り、Studentに変換してIEnumerableを返す。
        private static IEnumerable<Student> ReadScore(string filePath) {
            //点数リスト
            var scores = new List<Student>();
            var lines = File.ReadAllLines(filePath);
            //各行をカンマ区切りで分割、Scoreデータの作成、登録
            foreach (var line in lines) {
                var items = line.Split(',');
                int score = int.Parse(items[2]);
                var sale = new Student {
                    Name = items[0],
                    Subject = items[1],
                    Score = score
                };
                scores.Add(sale);
            }
            return scores;
        }

        //メソッドの概要：
        //保持しているStudentListから教科ごとの合計点数を求め、教科名をキーとした点数の一覧を返す。
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            foreach (var scorePair in _score) {
                if (dict.ContainsKey(scorePair.Subject)) {
                    dict[scorePair.Subject] += scorePair.Score;
                } else {
                    dict[scorePair.Subject] = scorePair.Score;
                }
            }
            return dict;
        }
    }
}
