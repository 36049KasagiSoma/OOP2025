﻿using System.Diagnostics.Metrics;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            //機能追加②
            Console.WriteLine("1:ヤードからメートル");
            Console.WriteLine("2:メートルからヤード");
            int type = GetInputInt(">", 1, 2);
            int inVal = GetInputInt($"変換前({(type == 1 ? "ヤード" : "メートル")})：");
            double outVal = type == 1 ? MerterConverter.YardToMeter(inVal) : MerterConverter.MeterToYard(inVal);
            Console.WriteLine($"変換後({(type == 1 ? "メートル" : "ヤード")}):{outVal:0.0000}");
            //(条件式)?(trueの場合):(falseの場合)
        }

        /// <summary>キーボードからのint型整数値入力を受け付けます。</summary>
        /// <param name="_printText">入力プロンプト文字列</param>
        /// <returns>有効なint型数値</returns>
        private static int GetInputInt(string _printText) {
            return GetInputInt(_printText, int.MinValue, int.MaxValue);
        }
        /// <summary>キーボードからのint型整数値入力を受け付けます。</summary>
        /// <param name="_printText">入力プロンプト文字列</param>
        /// <param name="_min">受付可能最小数値</param>
        /// <param name="_max">受付可能最大数値</param>
        /// <returns>有効なint型数値</returns>
        private static int GetInputInt(string _printText, int _min, int _max) {
            while (true) {
                Console.Write(_printText);
                if (int.TryParse(Console.ReadLine(), out int inVal) && inVal >= _min && inVal <= _max) {
                    return inVal;
                }
            }
        }

        /// <summary>インチからメートルの変換表を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        private static void PrintInchToMeter(int _min, int _max) {
            for (int i = _min; i <= _max; i++) {
                double m = MerterConverter.InchToMeter(i);
                int sp = _max.ToString().Length - i.ToString().Length;
                //FillSpace(sp)は、左詰めしようと{i,sp}としたら、「spの部分は定数にしろ」
                //...と怒られたため、無理やりスペース埋めで
                Console.WriteLine($"{FillSpace(sp)}{i}in = {m:0.0000}m");
            }
        }
        /// <summary>メートルからインチの変換表を出力します。</summary>
        /// <param name="_min">変換最小値</param>
        /// <param name="_max">変換最大値</param>
        private static void PrintMeterToInch(int _min, int _max) {
            for (int i = _min; i <= _max; i++) {
                double m = MerterConverter.MeterToInch(i);
                int sp = _max.ToString().Length - i.ToString().Length;
                Console.WriteLine($"{FillSpace(sp)}{i}m = {m:0.0000}in");
            }
        }

        /// <summary>任意文字数の空白文字列を作成します。</summary>
        /// <param name="_length">文字数</param>
        /// <returns>引数で指定した文字数のスペース文字列</returns>
        private static string FillSpace(int _length) {
            string rtn = "";
            for (int i = 0; i < _length; i++) {
                rtn += " ";
            }
            return rtn;
        }
    }
}
