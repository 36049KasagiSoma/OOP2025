namespace Test01 {
    /// <summary>
    /// ファイルから情報を読み込み、集計をサポートするクラスです。
    /// </summary>
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        /// <param name="filePath">読み込み対象のファイルのパス</param>
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);
        }

        //メソッドの概要：
        //指定したファイルパスから、名前、教科、点数の一覧を取得する。
        private static IEnumerable<Student> ReadScore(string filePath) {
            var scores = new List<Student>();
            var lines = File.ReadAllLines(filePath);

            //分割、登録処理
            foreach (var line in lines) {
                var items = line.Split(',');

                //第三引数はintなのでint変換
                int score = int.Parse(items[2]);

                var student = new Student {
                    Name = items[0],
                    Subject = items[1],
                    Score = score
                };
                scores.Add(student);
            }
            return scores;
        }

        //メソッドの概要：
        //教科ごとの合計点数を求め、教科名をキーとした点数の一覧を返す。
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            
            //集計処理
            foreach (var scorePair in _score) {
                //登録済み、未登録での処理分岐
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
