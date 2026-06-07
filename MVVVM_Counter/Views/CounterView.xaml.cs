using System.Windows.Controls;
using MVVVM_Counter.ViewModels;

namespace MVVVM_Counter.Views;

public partial class CounterView : UserControl
{
    public CounterView()
    {
        InitializeComponent();
    }

    public void SetViewModel(CounterViewModel viewModel)
    {
        // DataContextをセットするとXAMLのBindingがこのViewModelを参照する
        DataContext = viewModel;
    }
}