using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace TestProg {
    public class Othello6x6Analyzer {
        private const int EMPTY = 0;
        private const int BLACK = 1;
        private const int WHITE = 2;
        private const int SIZE = 6;
        private const int BOARD_SIZE = SIZE * SIZE;

        // ビットボードを使用した高速化
        private struct BitBoard {
            public ulong Black;
            public ulong White;

            public BitBoard(ulong black, ulong white) {
                Black = black;
                White = white;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool IsEmpty(int pos) => (Black | White & (1UL << pos)) == 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int GetPiece(int pos) {
                ulong mask = 1UL << pos;
                if ((Black & mask) != 0) return BLACK;
                if ((White & mask) != 0) return WHITE;
                return EMPTY;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void SetPiece(int pos, int piece) {
                ulong mask = 1UL << pos;
                Black &= ~mask;
                White &= ~mask;
                if (piece == BLACK) Black |= mask;
                else if (piece == WHITE) White |= mask;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public ulong GetOccupied() => Black | White;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int CountBits(ulong bits) {
                // Brian Kernighanのアルゴリズム
                int count = 0;
                while (bits != 0) {
                    bits &= bits - 1;
                    count++;
                }
                return count;
            }

            public (int black, int white, int empty) CountStones() {
                int black = CountBits(Black);
                int white = CountBits(White);
                int empty = BOARD_SIZE - black - white;
                return (black, white, empty);
            }
        }

        private readonly BitBoard _initialBoard;
        private readonly ConcurrentBag<GameResult> _allResults;
        private readonly object _progressLock = new object();

        private long _totalNodes;
        private long _processedNodes;
        private int _gameCount;
        private DateTime _startTime;

        // 事前計算された方向マスク
        private static readonly ulong[] _directionMasks = new ulong[8];
        private static readonly int[] _directions = { -7, -6, -5, -1, 1, 5, 6, 7 }; // 6x6用の方向オフセット

        // トランスポジションテーブル
        private readonly ConcurrentDictionary<ulong, GameResult> _transpositionTable;

        static Othello6x6Analyzer() {
            InitializeDirectionMasks();
        }

        public Othello6x6Analyzer() {
            _allResults = new ConcurrentBag<GameResult>();
            _transpositionTable = new ConcurrentDictionary<ulong, GameResult>();
            _initialBoard = CreateInitialBoard();
        }

        private static void InitializeDirectionMasks() {
            // 6x6盤面用の方向マスクを事前計算
            for (int i = 0; i < 8; i++) {
                _directionMasks[i] = CalculateDirectionMask(i);
            }
        }

        private static ulong CalculateDirectionMask(int direction) {
            // 実装は省略 - 各方向の有効なマスクを計算
            return 0xFFFFFFFFFFFFFFFFUL; // 簡略化
        }

        private BitBoard CreateInitialBoard() {
            var board = new BitBoard(0, 0);
            board.SetPiece(2 * SIZE + 2, WHITE); // (2,2)
            board.SetPiece(2 * SIZE + 3, BLACK); // (2,3)
            board.SetPiece(3 * SIZE + 2, BLACK); // (3,2)
            board.SetPiece(3 * SIZE + 3, WHITE); // (3,3)
            return board;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsValidPosition(int pos) {
            return pos >= 0 && pos < BOARD_SIZE;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int PosToRow(int pos) => pos / SIZE;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int PosToCol(int pos) => pos % SIZE;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int RowColToPos(int row, int col) => row * SIZE + col;

        // ビットボードを使った高速な手の生成
        private unsafe List<int> GetValidMoves(BitBoard board, int player) {
            var moves = new List<int>();
            ulong playerBits = player == BLACK ? board.Black : board.White;
            ulong opponentBits = player == BLACK ? board.White : board.Black;
            ulong empty = ~(playerBits | opponentBits) & ((1UL << BOARD_SIZE) - 1);

            while (empty != 0) {
                int pos = TrailingZeroCount(empty);
                empty &= empty - 1; // 最下位ビットをクリア

                if (CanFlipAny(board, pos, player)) {
                    moves.Add(pos);
                }
            }

            return moves;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int TrailingZeroCount(ulong value) {
            if (value == 0) return 64;
            int count = 0;
            while ((value & 1) == 0) {
                value >>= 1;
                count++;
            }
            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanFlipAny(BitBoard board, int pos, int player) {
            for (int d = 0; d < 8; d++) {
                if (CanFlipDirection(board, pos, d, player) > 0)
                    return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int CanFlipDirection(BitBoard board, int pos, int dirIndex, int player) {
            int dir = _directions[dirIndex];
            int current = pos + dir;
            int count = 0;

            ulong playerBits = player == BLACK ? board.Black : board.White;
            ulong opponentBits = player == BLACK ? board.White : board.Black;

            // 境界チェックを最適化
            while (IsValidInDirection(pos, current, dirIndex) &&
                   (opponentBits & (1UL << current)) != 0) {
                count++;
                current += dir;
            }

            if (count > 0 && IsValidInDirection(pos, current, dirIndex) &&
                (playerBits & (1UL << current)) != 0) {
                return count;
            }

            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsValidInDirection(int startPos, int currentPos, int dirIndex) {
            if (currentPos < 0 || currentPos >= BOARD_SIZE) return false;

            int startRow = startPos / SIZE;
            int startCol = startPos % SIZE;
            int currentRow = currentPos / SIZE;
            int currentCol = currentPos % SIZE;

            // 行や列の境界を越えていないかチェック
            switch (dirIndex) {
                case 0:
                case 3:
                case 5: // 左方向を含む
                    return currentCol <= startCol;
                case 2:
                case 4:
                case 7: // 右方向を含む
                    return currentCol >= startCol;
                default:
                    return true;
            }
        }

        private BitBoard MakeMove(BitBoard board, int pos, int player) {
            var newBoard = board;
            newBoard.SetPiece(pos, player);

            for (int d = 0; d < 8; d++) {
                int flipCount = CanFlipDirection(board, pos, d, player);
                if (flipCount > 0) {
                    int current = pos + _directions[d];
                    for (int i = 0; i < flipCount; i++) {
                        newBoard.SetPiece(current, player);
                        current += _directions[d];
                    }
                }
            }

            return newBoard;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsGameOver(BitBoard board, int consecutivePasses) {
            if (consecutivePasses >= 2) return true;
            return board.GetOccupied() == ((1UL << BOARD_SIZE) - 1);
        }

        // ハッシュ値計算（Zobrist hashing）
        private static readonly ulong[] _zobristTable = GenerateZobristTable();

        private static ulong[] GenerateZobristTable() {
            var table = new ulong[BOARD_SIZE * 3]; // EMPTY, BLACK, WHITE
            var random = new Random(12345); // 固定シード

            for (int i = 0; i < table.Length; i++) {
                table[i] = NextULong(random);
            }

            return table;
        }

        private static ulong NextULong(Random random) {
            byte[] bytes = new byte[8];
            random.NextBytes(bytes);
            return BitConverter.ToUInt64(bytes, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ulong ComputeHash(BitBoard board) {
            ulong hash = 0;
            for (int i = 0; i < BOARD_SIZE; i++) {
                int piece = board.GetPiece(i);
                if (piece != EMPTY) {
                    hash ^= _zobristTable[i * 3 + piece];
                }
            }
            return hash;
        }

        private string MoveToString(int pos) {
            int row = pos / SIZE;
            int col = pos % SIZE;
            return $"{(char)('a' + col)}{row + 1}";
        }

        // 並列処理を最適化した解析関数
        private async Task AnalyzeGameAsync(BitBoard board, int player, List<string> moves,
            int consecutivePasses = 0, int depth = 0) {
            // 進捗更新
            long processed = Interlocked.Increment(ref _processedNodes);
            if (processed % 50000 == 0) UpdateProgress();

            // トランスポジションテーブルチェック
            ulong hash = ComputeHash(board);
            if (_transpositionTable.TryGetValue(hash, out var cachedResult)) {
                // キャッシュされた結果を使用
                var newResult = new GameResult {
                    Moves = new List<string>(moves),
                    BlackStones = cachedResult.BlackStones,
                    WhiteStones = cachedResult.WhiteStones,
                    Winner = cachedResult.Winner
                };
                _allResults.Add(newResult);
                Interlocked.Increment(ref _gameCount);
                return;
            }

            // ゲーム終了チェック
            if (IsGameOver(board, consecutivePasses)) {
                var counts = board.CountStones();
                var result = new GameResult {
                    Moves = new List<string>(moves),
                    BlackStones = counts.black,
                    WhiteStones = counts.white,
                    Winner = counts.black > counts.white ? "BLACK" :
                             counts.white > counts.black ? "WHITE" : "DRAW"
                };

                _allResults.Add(result);
                _transpositionTable.TryAdd(hash, result);
                Interlocked.Increment(ref _gameCount);
                return;
            }

            var validMoves = GetValidMoves(board, player);

            // パスの場合
            if (validMoves.Count == 0) {
                var nextPlayer = player == BLACK ? WHITE : BLACK;
                var playerStr = player == BLACK ? "B" : "W";
                var newMoves = new List<string>(moves) { $"{playerStr}pass" };

                await AnalyzeGameAsync(board, nextPlayer, newMoves, consecutivePasses + 1, depth + 1);
                return;
            }

            // 並列処理の最適化
            if (depth < 4 && validMoves.Count > 2) {
                var tasks = validMoves.Select(pos => Task.Run(async () => {
                    var newBoard = MakeMove(board, pos, player);
                    var moveStr = MoveToString(pos);
                    var playerStr = player == BLACK ? "B" : "W";
                    var nextPlayer = player == BLACK ? WHITE : BLACK;
                    var newMoves = new List<string>(moves) { $"{playerStr}{moveStr}" };

                    await AnalyzeGameAsync(newBoard, nextPlayer, newMoves, 0, depth + 1);
                })).ToArray();

                await Task.WhenAll(tasks);
            } else {
                // 順次処理
                foreach (var pos in validMoves) {
                    var newBoard = MakeMove(board, pos, player);
                    var moveStr = MoveToString(pos);
                    var playerStr = player == BLACK ? "B" : "W";
                    var nextPlayer = player == BLACK ? WHITE : BLACK;
                    var newMoves = new List<string>(moves) { $"{playerStr}{moveStr}" };

                    await AnalyzeGameAsync(newBoard, nextPlayer, newMoves, 0, depth + 1);
                }
            }
        }

        private long EstimateNodes(BitBoard board, int player, int depth = 0, int maxDepth = 15,
            int consecutivePasses = 0) {
            if (depth > maxDepth || IsGameOver(board, consecutivePasses))
                return 1;

            var validMoves = GetValidMoves(board, player);
            if (validMoves.Count == 0) {
                var nextPlayer = player == BLACK ? WHITE : BLACK;
                return EstimateNodes(board, nextPlayer, depth + 1, maxDepth, consecutivePasses + 1);
            }

            long total = 0;
            int sampleSize = Math.Min(validMoves.Count, 2); // さらにサンプル数を削減

            for (int i = 0; i < sampleSize; i++) {
                var newBoard = MakeMove(board, validMoves[i], player);
                var nextPlayer = player == BLACK ? WHITE : BLACK;
                total += EstimateNodes(newBoard, nextPlayer, depth + 1, maxDepth, 0);
            }

            return total * validMoves.Count / sampleSize;
        }

        private void UpdateProgress() {
            lock (_progressLock) {
                var elapsed = DateTime.Now - _startTime;
                var percentage = Math.Min((_processedNodes * 100.0) / _totalNodes, 100.0);
                var rate = _processedNodes / elapsed.TotalSeconds;
                var remaining = Math.Max(0, _totalNodes - _processedNodes);
                var eta = remaining / rate;

                Console.Write($"\r進捗: {_processedNodes:N0}/{_totalNodes:N0} ({percentage:F1}%) | " +
                                $"完了: {_gameCount:N0} | " +
                                $"ヒット: {_transpositionTable.Count:N0} | " +
                                $"{rate:F0}ノード/秒 | " +
                                $"{elapsed:hh\\:mm\\:ss} | " +
                                $"/{TimeSpan.FromSeconds(eta):hh\\:mm\\:ss}");
            }
        }

        public async Task<AnalysisResult> PerformCompleteAnalysisAsync() {
            Console.WriteLine("6×6オセロ完全解析を開始します（高速化版）...");
            PrintBoard(_initialBoard);
            Console.WriteLine();

            // メモリ設定最適化
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("ノード数を計算中...");
            _totalNodes = EstimateNodes(_initialBoard, BLACK);
            Console.WriteLine($"予想ノード数: {_totalNodes:N0}");
            Console.WriteLine();

            _processedNodes = 0;
            _gameCount = 0;
            _startTime = DateTime.Now;
            _allResults.Clear();
            _transpositionTable.Clear();

            var stopwatch = Stopwatch.StartNew();

            try {
                await AnalyzeGameAsync(_initialBoard, BLACK, new List<string>());
            } catch (OutOfMemoryException) {
                Console.WriteLine("メモリ不足が発生しました。結果を保存して処理を継続します...");
                await SaveIntermediateResultsAsync();
                GC.Collect();
            }

            stopwatch.Stop();

            var results = _allResults.ToList();
            var analysisTime = stopwatch.Elapsed;

            Console.WriteLine();
            Console.WriteLine("=== 解析完了 ===");
            Console.WriteLine($"総ゲーム数: {results.Count:N0}");
            Console.WriteLine($"処理ノード数: {_processedNodes:N0}");
            Console.WriteLine($"キャッシュヒット数: {_transpositionTable.Count:N0}");
            Console.WriteLine($"解析時間: {analysisTime:hh\\:mm\\:ss}");
            Console.WriteLine($"処理速度: {_processedNodes / analysisTime.TotalSeconds:F0}ノード/秒");

            return new AnalysisResult {
                Games = results,
                TotalGames = results.Count,
                ProcessedNodes = _processedNodes,
                AnalysisTime = analysisTime
            };
        }

        private void PrintBoard(BitBoard board) {
            Console.WriteLine("  abcdef");
            for (int row = 0; row < SIZE; row++) {
                Console.Write($"{row + 1} ");
                for (int col = 0; col < SIZE; col++) {
                    int pos = row * SIZE + col;
                    switch (board.GetPiece(pos)) {
                        case EMPTY:
                            Console.Write('.');
                            break;
                        case BLACK:
                            Console.Write('B');
                            break;
                        case WHITE:
                            Console.Write('W');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        private async Task SaveIntermediateResultsAsync() {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var filename = $"othello_6x6_intermediate_{timestamp}.txt";

            var results = _allResults.ToList();
            await SaveResultsToFileAsync(results, filename);

            Console.WriteLine($"中間結果を {filename} に保存しました。");
        }

        public async Task SaveResultsToFileAsync(List<GameResult> results, string filename = null) {
            if (filename == null) {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                filename = $"othello_6x6_analysis_optimized_{timestamp}.txt";
            }

            using var writer = new StreamWriter(filename, false, Encoding.UTF8);

            await writer.WriteLineAsync("6×6オセロ完全解析結果（高速化版）");
            await writer.WriteLineAsync("=====================================");
            await writer.WriteLineAsync();
            await writer.WriteLineAsync($"解析日時: {DateTime.Now}");
            await writer.WriteLineAsync($"総ゲーム数: {results.Count:N0}");
            await writer.WriteLineAsync($"キャッシュヒット数: {_transpositionTable.Count:N0}");
            await writer.WriteLineAsync();

            // 統計情報
            var blackWins = results.Count(r => r.Winner == "BLACK");
            var whiteWins = results.Count(r => r.Winner == "WHITE");
            var draws = results.Count(r => r.Winner == "DRAW");

            await writer.WriteLineAsync("=== 勝敗統計 ===");
            await writer.WriteLineAsync($"黒勝利: {blackWins:N0} ({blackWins * 100.0 / results.Count:F2}%)");
            await writer.WriteLineAsync($"白勝利: {whiteWins:N0} ({whiteWins * 100.0 / results.Count:F2}%)");
            await writer.WriteLineAsync($"引分: {draws:N0} ({draws * 100.0 / results.Count:F2}%)");
            await writer.WriteLineAsync();

            // スコア分布
            var scoreDistribution = results
                .GroupBy(r => $"{r.BlackStones}-{r.WhiteStones}")
                .OrderByDescending(g => g.Count())
                .ToList();

            await writer.WriteLineAsync("=== スコア分布 ===");
            foreach (var group in scoreDistribution.Take(20)) {
                var percentage = group.Count() * 100.0 / results.Count;
                await writer.WriteLineAsync($"{group.Key}: {group.Count():N0}回 ({percentage:F2}%)");
            }
            await writer.WriteLineAsync();

            Console.WriteLine($"結果を {filename} に保存しました。");
        }
    }

    public class GameResult {
        public List<string> Moves { get; set; }
        public int BlackStones { get; set; }
        public int WhiteStones { get; set; }
        public string Winner { get; set; }
    }

    public class AnalysisResult {
        public List<GameResult> Games { get; set; }
        public int TotalGames { get; set; }
        public long ProcessedNodes { get; set; }
        public TimeSpan AnalysisTime { get; set; }
    }

    class Program {
        static async Task Main(string[] args) {
            try {
                Console.WriteLine("高速化された6×6オセロ解析プログラム");
                Console.WriteLine("====================================");

                var analyzer = new Othello6x6Analyzer();
                var result = await analyzer.PerformCompleteAnalysisAsync();

                // 結果をファイルに保存
                await analyzer.SaveResultsToFileAsync(result.Games);

                Console.WriteLine();
                Console.WriteLine("解析完了！結果ファイルをご確認ください。");
                Console.WriteLine("何かキーを押して終了...");
                Console.ReadKey();
            } catch (Exception ex) {
                Console.WriteLine($"エラーが発生しました: {ex.Message}");
                Console.WriteLine($"スタックトレース: {ex.StackTrace}");
                Console.WriteLine("何かキーを押して終了...");
                Console.ReadKey();
            }
        }
    }
}
