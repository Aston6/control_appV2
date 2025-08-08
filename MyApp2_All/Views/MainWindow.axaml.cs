using Avalonia.Controls;
using MyApp2.Services;
using MyApp2.ViewModels;

namespace MyApp2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var vm = ServiceLocator.Current.GetRequiredService<MainWindowViewModel>();
        DataContext = vm;
        vm.OnInitialized();
    }
}