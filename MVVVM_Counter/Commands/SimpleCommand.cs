using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MVVVM_Counter
{
    /// <summary>
    /// - ICommandインターフェースを使用しているので、そこで定義している関数を使用する必要がある
    /// - コードジャンプが少し重い気がするので、ここに書いておく
    ///   - CanExecuteChanged, CanExecute, Execute
    /// - その関数は、wpfが特定の場面で自動的に実行する
    ///     - CanExecute → 初回バインド時 + CanExecuteChanged発火時
    ///     - Execute → ボタンがクリックされた時
    ///     - CanExecuteChanged → WPFがこのイベントを購読し、発火を待ち受ける
    /// </summary>
    internal class SimpleCommand: ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public SimpleCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        
        // ButtonのCommmndプロパティにバインドすると、wpfがIsEnableを制御する
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute(parameter); 

        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
