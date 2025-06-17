namespace Test01 {
    public class Program {
        static void Main(string[] args) {
            //情報の取り込み
            var score = new ScoreCounter("StudentScore.csv");

            //教科別集計結果取得
            var TotalBySubject = score.GetPerStudentScore();
            foreach (var obj in TotalBySubject) {
                Console.WriteLine("{0} {1}", obj.Key, obj.Value);
            }
        }
    }
}

//実行結果
//英語 520
//数学 550
//国語 500