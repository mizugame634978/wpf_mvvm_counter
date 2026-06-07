using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVVM_Counter.Models;

namespace MVVVM_Counter.ViewModels;

public class EvenOddViewModel : INotifyPropertyChanged
{
    private readonly CounterModel _model;

    public EvenOddViewModel(CounterModel model)
    {
        _model = model;

        //モデルの変更を購読
        _model.ValueChanged += OnCountChanged;
    }

    public string EvenOddText => _model.Value % 2 == 0 ? "Even" : "odd";

    private void OnCountChanged()
    {
        OnPropertyChanged(nameof(EvenOddText));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}