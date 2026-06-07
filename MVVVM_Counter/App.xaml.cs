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
            // 同一のmodelインスタンスを渡すことで、カウンターの変化が両Viewに伝わる
            var counterViewModel = new CounterViewModel(model, storage);
            var evenOddViewModel = new EvenOddViewModel(model);
            
            // 階層構造を構築
            var mainViewModel = new MainViewModel(counterViewModel, evenOddViewModel);
            
            // Main
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            
            //ウィンドウ表示
            mainWindow.Show();
        }
    }

}
