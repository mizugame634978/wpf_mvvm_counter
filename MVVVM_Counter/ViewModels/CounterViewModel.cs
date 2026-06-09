using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Input;
using MVVVM_Counter.Models;
using MVVVM_Counter.Service;

namespace MVVVM_Counter.ViewModels
{
    /// <summary>
    /// - INotifyPropertyChangedでPropertyChangedが定義されている。
    /// - このファイルでWPFが自動実行するのは**PropertyChangedイベントの発火を受け取ることだけ
    /// </summary>
    public class CounterViewModel: INotifyPropertyChanged
    {
        private readonly CounterModel _model;
        private readonly JsonCounterStorage _storage;
        private readonly SimpleCommand _incrmentCommand;
        private readonly SimpleCommand _decrmentCommand;


        public CounterViewModel(CounterModel model, JsonCounterStorage storage)
        {
            _model = model;
            _storage = storage;
            int initialValue = _storage.Load();
            // 購読前にSetValueを呼ぶため通知は誰にも届かないが、
            // CountはModelを直接参照する計算プロパティなので初回描画時に正しい値が表示される
            _model.SetValue(initialValue);

            // モデルの変更を購読
            _model.ValueChanged += OnCountChanged;
            
            _incrmentCommand = new SimpleCommand(_ => ExecuteIncrement());
            _decrmentCommand = new SimpleCommand(_ => ExecuteDecrement(), _=>_model.CanDecrement());
        }

        public int Count => _model.Value;

        public ICommand IncrementCommand => _incrmentCommand;
        public ICommand DecrementCommand => _decrmentCommand;

        private void ExecuteIncrement() 
        {
            _model.Increment();
            _storage.Save(_model.Value);

        }
        
        private void ExecuteDecrement()
        {
            _model.Decrement();
            _storage.Save(_model.Value);
        }

        // Modelの変更通知を受け取る
        private void OnCountChanged()
        {
            OnPropertyChanged(nameof(Count));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        // [CallerMemberName]: 呼び出し元のメソッド/プロパティ名をコンパイル時に自動で引数に埋め込む
        // OnPropertyChanged(nameof(Count)) と書かなくても、プロパティのgetterから呼ぶだけで
        // プロパティ名が自動設定される
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
