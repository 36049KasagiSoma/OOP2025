using System.Diagnostics;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            int sleapTime = 1500;
#if false
            var tw = new TimeWatch();
            tw.Start();
            // スリープする
            Thread.Sleep(sleapTime);
            TimeSpan duration = tw.Stop();
            Console.WriteLine("処理時間は{0}ミリ秒でした", duration.TotalMilliseconds);
#else
            var tw = new Stopwatch();
            tw.Start();
            // スリープする
            Thread.Sleep(sleapTime);
            tw.Stop();
            TimeSpan duration = tw.Elapsed;
            Console.WriteLine("処理時間は{0}ミリ秒でした", duration.TotalMilliseconds);
#endif
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
}
