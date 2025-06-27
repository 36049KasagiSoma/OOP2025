namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var tw = new TimeWatch();
            tw.Start();
            // スリープする
            Thread.Sleep(1000);
            TimeSpan duration = tw.Stop();
            Console.WriteLine("処理時間は{0}ミリ秒でした", duration.TotalMilliseconds);

        }
    }

    class TimeWatch {
        private DateTime _time;
        
        public void Start() {
            //現在の時間を_timeに設定
            _time = DateTime.Now;
        }

        public TimeSpan Stop() {
            //経過時間を返却する
            return DateTime.Now - _time;
        }
    }
}
