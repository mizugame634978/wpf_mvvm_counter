https://prota-p.com/csharp_wpf9_mvvm1/ を行った

# アーキテクチャ
- MVVM方式で実装している
```mermaid
graph TD
    App.xaml -->|起動| CounterWindow.xaml
    CounterWindow.xaml.cs -->|DataContext| CounterViewModel.cs
    CounterWindow.xaml -->|バインディング| CounterWindow.xaml.cs
    CounterViewModel.cs -->|使用| SimpleCommand.cs
    CounterViewModel.cs -->|呼び出し| CounterModel.cs
```
