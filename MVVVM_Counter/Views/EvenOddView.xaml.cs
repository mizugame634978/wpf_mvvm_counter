using System.Windows.Controls;
using MVVVM_Counter.ViewModels;

namespace MVVVM_Counter.Views;

/// <summary>
/// EvenOddView.xamlの相互作用ロジック
/// </summary>
public partial class EvenOddView : UserControl
{
    public EvenOddView()
    {
        InitializeComponent();
    }
    
    public void SetViewModel(EvenOddViewModel viewModel)
    {
        // DataContextをセットするとXAMLのBindingがこのViewModelを参照する
        DataContext = viewModel;
    }
}