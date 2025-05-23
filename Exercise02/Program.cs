using System;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1();
            Console.WriteLine("---");
            Exercise2();
            Console.WriteLine("---");
            Exercise3();
        }

        private static void Exercise1() {
            int value = GetInputInt("数値を入力:");

            if (value < 0) {
                Console.WriteLine(value);
            } else if (value < 100) {
                Console.WriteLine(value * 2);
            } else if (value < 500) {
                Console.WriteLine(value * 3);
            } else {
                Console.WriteLine(value);
            }

        }

        private static void Exercise2() {
            int value = GetInputInt("数値を入力:");

            switch (value) {
                case < 0:
                default:
                    Console.WriteLine(value);
                    break;
                case < 100:
                    Console.WriteLine(value * 2);
                    break;
                case < 500:
                    Console.WriteLine(value * 3);
                    break;
            }
        }

        private static void Exercise3() {
            int value = GetInputInt("数値を入力:");

            Console.WriteLine(value switch {
                < 0 => value,
                < 100 => value * 2,
                < 500 => value * 3,
                _ => value
            });
        }

        private static int GetInputInt(string tips) {
            int value;
            string line;
            do {
                Console.Write(tips);
                line = Console.ReadLine() ?? "";
            } while (!int.TryParse(line, out value));

            return value;
        }
    }
}
