using MVVVM_Counter.Models;
using MVVVM_Counter.Service;

namespace MVVVM_Counter.ViewModels;

/// <summary>
/// 複数のViewModelをまとめるコンテナViewModel
/// MainView.xamlのContentControlが「CounterViewModel」「EvenOddViewModel」プロパティを
/// バインドし、App.xamlのDataTemplateで各Viewに自動変換される
/// </summary>
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