// 略語と対応する日本語を管理するクラス
public class Abbreviations {
    private readonly Dictionary<string, string> _dict = new();

    // コンストラクター
    public Abbreviations() {
        var lines = File.ReadAllLines("./Abbreviations.txt");
        _dict = lines
            .Select(line => line.Split('='))
            .ToDictionary(x => x[0], x => x[1]);
    }

    // 要素を追加
    public void Add(string abbr, string japanese) =>
        _dict[abbr] = japanese;

    // 省略語から対応する日本語を取り出す
    public string? Get(string abbr) =>
        _dict.ContainsKey(abbr) ? _dict[abbr] : null;

    //要素をすべて取得する
    public IEnumerable<KeyValuePair<string, string>> GetAll() => _dict;

    // 日本語から対応する省略語を取り出す
    public string? ToAbbreviation(string japanese) =>
        _dict.FirstOrDefault(x => x.Value == japanese).Key;

    // 日本語の部分文字列を引数に与え、それが含まれる要素(Key,Value)をすべて取り出す
    public IEnumerable<(string, string)> FindAll(string substring) {
        foreach (var (key, value) in _dict) {
            if (value.Contains(substring)) {
                yield return (key, value);
            }
        }
    }

    //要素の個数を返す
    public int Count => _dict.Count;

    //要素を削除する
    public bool Remove(string abbr) => _dict.Remove(abbr);

    //要素を検索して取得する
    public IDictionary<string, string> Search(Func<KeyValuePair<string, string>, bool> condition) {
        IDictionary<string, string> rtn = new Dictionary<string, string>();
        foreach (var keypair in _dict) {
            if (condition(keypair)) {
                rtn.Add(keypair.Key, keypair.Value);
            }
        }
        return rtn;
    }
}