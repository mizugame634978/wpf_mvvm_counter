https://prota-p.com/csharp_wpf9_mvvm1/ を行った

# アーキテクチャ
- MVVM方式で実装している
```mermaid
graph TD
    subgraph View
        App.xaml.cs
        MainWindow.xaml
        CounterView.xaml
        EvenOddView.xaml
    end
    subgraph ViewModel
        CounterViewModel.cs
        EvenOddViewModel.cs
        SimpleCommand.cs
    end
    subgraph Model
        CounterModel.cs
    end
    subgraph Service
        JsonCounterStorage.cs
    end

    App.xaml.cs -->|DI: 生成・注入| CounterViewModel.cs
    App.xaml.cs -->|DI: 生成・注入| EvenOddViewModel.cs
    App.xaml.cs -->|生成| MainWindow.xaml
    MainWindow.xaml -->|含む| CounterView.xaml
    MainWindow.xaml -->|含む| EvenOddView.xaml
    CounterView.xaml -->|DataContext| CounterViewModel.cs
    EvenOddView.xaml -->|DataContext| EvenOddViewModel.cs
    CounterViewModel.cs -->|使用| SimpleCommand.cs
    CounterViewModel.cs -->|呼び出し| CounterModel.cs
    EvenOddViewModel.cs -->|ValueChanged購読| CounterModel.cs
    CounterViewModel.cs -->|保存・読み込み| JsonCounterStorage.cs
    JsonCounterStorage.cs -->|読み書き| counter.json
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