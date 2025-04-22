using System;
using System.Collections.Generic;

namespace DistanceConverter {
    /// <summary>迷路を生成・表示するためのクラスです。</summary>
    class Maze {
        private int width, height;                     // 迷路の幅と高さ
        private bool[,] map;                           // 迷路のマップ（true = 壁, false = 通路）
        private Random rand = new Random();            // ランダム生成用
        private char[] printChar = { '■', '　' };         // 表示用キャラクター（壁と通路）

        /// <summary>Maze クラスの新しいインスタンスを初期化します。</summary>
        /// <param name="_x">迷路の横幅（奇数に自動調整）</param>
        /// <param name="_y">迷路の高さ（奇数に自動調整）</param>
        public Maze(int _x, int _y) {
            width = _x % 2 == 0 ? _x + 1 : _x;          // 偶数なら+1して奇数にする
            height = _y % 2 == 0 ? _y + 1 : _y;
            map = new bool[width, height];             // マップの初期化
        }

        /// <summary>迷路を生成します。</summary>
        public void CreateMaze() {
            FillBoolArray(map, true);                  // 全てを壁で埋める

            Stack<(int x, int y)> stack = new Stack<(int x, int y)>(); // 移動経路の履歴
            int x = 1, y = 1;                           // 開始位置
            map[x, y] = false;
            stack.Push((x, y));

            // 再帰的バックトラッキングによる迷路生成
            while (stack.Count > 0) {
                var (cx, cy) = stack.Peek();            // 現在位置
                var directions = GetShuffledDirections(); // 方向をシャッフル

                bool moved = false;
                foreach (var (dx, dy) in directions) {
                    int nx = cx + dx * 2;
                    int ny = cy + dy * 2;

                    // 移動先がマップ内かつ未訪問なら通路を掘る
                    if (IsInBounds(nx, ny) && map[nx, ny]) {
                        map[cx + dx, cy + dy] = false;  // 間の壁を壊す
                        map[nx, ny] = false;            // 新しいマスを通路に
                        stack.Push((nx, ny));
                        moved = true;
                        break;
                    }
                }

                if (!moved) {
                    stack.Pop(); // 戻る（バックトラック）
                }
            }

            // スタートとゴールを設定
            map[1, 0] = false; // 入り口
            map[map.GetLength(0) - 2, map.GetLength(1) - 1] = false; // 出口
        }

        /// <summary>指定した座標がマップ内かどうかを判定します。</summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>マップ内であれば true、そうでなければ false</returns>
        private bool IsInBounds(int x, int y) {
            return x > 0 && y > 0 && x < width && y < height;
        }

        /// <summary>移動方向のリストをランダムにシャッフルして返します。</summary>
        /// <returns>シャッフルされた方向リスト</returns>
        private List<(int dx, int dy)> GetShuffledDirections() {
            var dirs = new List<(int dx, int dy)> {
                (1, 0), (-1, 0), (0, 1), (0, -1)        // 右、左、下、上
            };

            // Fisher-Yatesアルゴリズムによるシャッフル
            for (int i = 0; i < dirs.Count; i++) {
                int j = rand.Next(i, dirs.Count);
                var temp = dirs[i];
                dirs[i] = dirs[j];
                dirs[j] = temp;
            }

            return dirs;
        }

        /// <summary>二次元の真偽値配列を指定した値で埋めます。</summary>
        /// <param name="array">対象の配列</param>
        /// <param name="value">設定する値</param>
        private void FillBoolArray(bool[,] array, bool value) {
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    array[i, j] = value;
                }
            }
        }

        /// <summary>現在の迷路をコンソールに出力します。</summary>
        public void Print() {
            for (int i = 0; i < height; i++) {
                for (int j = 0; j < width; j++) {
                    Console.Write(map[j, i] ? printChar[0] : printChar[1]);
                }
                Console.WriteLine();
            }
        }
    }
}
