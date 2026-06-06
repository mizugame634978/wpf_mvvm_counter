using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Input;

namespace MVVVM_Counter
{
    /// <summary>
    /// - INotifyPropertyChangedでPropertyChangedが定義されている。
    /// - このファイルでWPFが自動実行するのは**PropertyChangedイベントの発火を受け取ることだけ
    /// </summary>
    internal class CounterViewModel: INotifyPropertyChanged
    {
        private readonly CounterModel _model;
        private readonly SimpleCommand _incrmentCommand;
        private readonly SimpleCommand _decrmentCommand;


        public CounterViewModel()
        {
            _model = new CounterModel();
            _incrmentCommand = new SimpleCommand(_ => ExecuteIncrement());
            _decrmentCommand = new SimpleCommand(_ => ExecuteDecrement(), _=>_model.CanDecrement());
        }

        public int Count => _model.Value;

        public ICommand IncrementCommand => _incrmentCommand;
        public ICommand DecrementCommand => _decrmentCommand;

        private void ExecuteIncrement() 
        {
            _model.Increment();
            OnPropertyChanged(nameof(Count));

        }
        
        private void ExecuteDecrement()
        {
            _model.Decrement();
            OnPropertyChanged(nameof(Count));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if(propertyName == nameof(Count))
            {
                // Countが変わったとき、DecrementCommandのCanExecuteも再評価させる
                // これによりWPFが「-」ボタンの有効/無効を更新する
                _decrmentCommand.RaiseCanExecuteChanged();
            }
        }
    }
}
