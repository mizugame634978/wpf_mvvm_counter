using MVVVM_Counter.Models;
using MVVVM_Counter.Service;

namespace MVVVM_Counter.ViewModels;

public class MainViewModel
{
    public CounterViewModel CounterViewModel { get; }
    public EvenOddViewModel EvenOddViewModel { get; }

    public MainViewModel(CounterViewModel counterViewModel, EvenOddViewModel evenOddViewModel)
    {
        CounterViewModel = counterViewModel;
        EvenOddViewModel = evenOddViewModel;
    }
}