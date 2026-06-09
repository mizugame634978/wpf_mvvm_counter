https://prota-p.com/csharp_wpf9_mvvm1/ を行った

# アーキテクチャ
- MVVM方式で実装している
```mermaid
graph TD
    subgraph View
        AppXaml["App.xaml (DataTemplate定義)"]
        AppXamlCs["App.xaml.cs (コンポジションルート)"]
        MainWindowXaml["MainWindow.xaml"]
        MainViewXaml["MainView.xaml"]
        CounterViewXaml["CounterView.xaml"]
        EvenOddViewXaml["EvenOddView.xaml"]
    end
    subgraph ViewModel
        MainViewModelCs["MainViewModel.cs"]
        CounterViewModelCs["CounterViewModel.cs"]
        EvenOddViewModelCs["EvenOddViewModel.cs"]
        SimpleCommandCs["SimpleCommand.cs"]
    end
    subgraph Model
        CounterModelCs["CounterModel.cs"]
    end
    subgraph Service
        JsonStorageCs["JsonCounterStorage.cs"]
    end

    AppXamlCs -->|DI: 生成・注入| MainViewModelCs
    AppXamlCs -->|DataContext設定| MainWindowXaml
    AppXaml -.->|DataTemplate提供| MainWindowXaml
    AppXaml -.->|DataTemplate提供| MainViewXaml
    MainWindowXaml -->|ContentControl| MainViewXaml
    MainViewXaml -->|ContentControl| CounterViewXaml
    MainViewXaml -->|ContentControl| EvenOddViewXaml
    MainViewModelCs -->|プロパティ保持| CounterViewModelCs
    MainViewModelCs -->|プロパティ保持| EvenOddViewModelCs
    CounterViewModelCs -->|使用| SimpleCommandCs
    CounterViewModelCs -->|呼び出し| CounterModelCs
    EvenOddViewModelCs -->|ValueChanged購読| CounterModelCs
    CounterViewModelCs -->|保存・読み込み| JsonStorageCs
    JsonStorageCs -->|読み書き| counter.json
```

# DI(依存性注入)について
- 依存性（クラスからnewした動くために必要なオブジェクト）を、外部から渡した
- 背景
  - 今までは、CounterViewModelでnewしていたクラスを、同じデータをEvenOddViewModelでも使いたくなった
  - そのまま２つのVMでnewするだけでは、値を共有できない
- 解決策
  - App.xaml.csでnewしたクラスを、２つのVMに渡す
  - こうすることで、それぞれのVMが同じクラスを見ているので、データを共有できる
- 言葉の説明
  - ２つのVMは、それぞれCounterModelがないと動かない（依存している）
  - その依存しているクラスを、外部(App.xaml.cs)から渡している（注入している）