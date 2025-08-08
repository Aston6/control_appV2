// LoginView.xaml.cs
using Avalonia.Controls;
using Avalonia.Input;
using MyApp2.Services;
using MyApp2.ViewModels;

namespace MyApp2.Views
{
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void OnPasswordKeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.DataContext is ViewModels.LoginViewModel vm && vm.LoginCommand.CanExecute(null))
                {
                    vm.LoginCommand.Execute(null);
                }
            }
        }

    }
}
