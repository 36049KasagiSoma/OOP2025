using System;
using System.Collections.Generic;

namespace DistanceConverter {
    class Maze {
        private int width, height;
        private bool[,] map;
        private Random rand = new Random();
        private char[] printChar = { '■','　'};

        public Maze(int _x, int _y) {
            width = _x % 2 == 0 ? _x + 1 : _x;
            height = _y % 2 == 0 ? _y + 1 : _y;
            map = new bool[width, height];
        }

        public void CreateMaze() {
            FillBoolArray(map, true);

            Stack<(int x, int y)> stack = new Stack<(int x, int y)>();
            int x = 1, y = 1;
            map[x, y] = false;
            stack.Push((x, y));

            while (stack.Count > 0) {
                var (cx, cy) = stack.Peek();
                var directions = GetShuffledDirections();

                bool moved = false;
                foreach (var (dx, dy) in directions) {
                    int nx = cx + dx * 2;
                    int ny = cy + dy * 2;

                    if (IsInBounds(nx, ny) && map[nx, ny]) {
                        map[cx + dx, cy + dy] = false;
                        map[nx, ny] = false;
                        stack.Push((nx, ny));
                        moved = true;
                        break;
                    }
                }

                if (!moved) {
                    stack.Pop(); // 戻る
                }
            }
            map[1, 0] = false;
            map[map.GetLength(0) - 2, map.GetLength(1) - 1] = false;
        }

        private bool IsInBounds(int x, int y) {
            return x > 0 && y > 0 && x < width && y < height;
        }

        private List<(int dx, int dy)> GetShuffledDirections() {
            var dirs = new List<(int dx, int dy)> {
                (1, 0), (-1, 0), (0, 1), (0, -1)
            };

            for (int i = 0; i < dirs.Count; i++) {
                int j = rand.Next(i, dirs.Count);
                var temp = dirs[i];
                dirs[i] = dirs[j];
                dirs[j] = temp;
            }

            return dirs;
        }

        private void FillBoolArray(bool[,] array, bool value) {
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    array[i, j] = value;
                }
            }
        }

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
