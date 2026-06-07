using System.Configuration;
using System.Data;
using System.Windows;
using MVVVM_Counter.ViewModels;
using MVVVM_Counter.Models;
using MVVVM_Counter.Service;
using MVVVM_Counter.Views;

namespace MVVVM_Counter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            //ここがコンポジションルート
            base.OnStartup(e);
            
            // サービスを作成
            var storage = new JsonCounterStorage();
            
            // Modelを作成
            var model = new CounterModel();
            
            // ViewModelを作成(依存性注入)
            var counterViewModel = new CounterViewModel(model, storage);
            var evenOddViewModel = new EvenOddViewModel(model);
            
            // MainWindowを作成し、ViewModelを設定(依存性注入)
            var mainWindow = new MainWindow();
            mainWindow.CounterView.SetViewModel(counterViewModel);
            mainWindow.EvenOddView.SetViewModel(evenOddViewModel);
            
            //ウィンドウ表示
            mainWindow.Show();
        }
    }

}
