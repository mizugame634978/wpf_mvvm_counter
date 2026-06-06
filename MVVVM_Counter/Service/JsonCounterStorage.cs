using System.Net;
using System.Text.Json;
using System.IO;
using System.Text.Json;


namespace MVVVM_Counter.Service;

/// <summary>
/// カウンターの値をJSONファイルに保存・読み込みするクラス
/// </summary>
public class JsonCounterStorage
{
    // アプリの実行ディレクトリ（bin/Debug/...）に保存される
    private const string FilePath = "counter.json";

    public int Load()
    {
        // using System.IO; を書くことで、System.IO.File を File と省略して書いている
        if(!File.Exists(FilePath)) return 0;

        var json = File.ReadAllText(FilePath);
        // JSONの文字列をint型に変換する
        return JsonSerializer.Deserialize<int>(json);
    }

    public void Save(int value)
    {
        var json = JsonSerializer.Serialize(value);
        File.WriteAllText(FilePath, json);
    }
}