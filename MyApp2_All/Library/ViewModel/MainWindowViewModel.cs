using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MyApp2.Libary.Services;
using MyApp2.Services;

namespace MyApp2.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase? _content;

        // Services
        private readonly IRootNavigationService _rootNavigationService;

        // Add ServiceLocator or IServiceProvider to get ViewModels
        private readonly IServiceProvider _serviceProvider;

        public MainWindowViewModel(
            IServiceProvider serviceProvider,
            IRootNavigationService rootNavigationService)
        {
            _serviceProvider = serviceProvider;
            _rootNavigationService = rootNavigationService;

            OnInitializedCommand = new RelayCommand(OnInitialized);
        }

        public ViewModelBase? Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public ICommand OnInitializedCommand { get; }

        public void OnInitialized()
        {
            // Start by showing LoginViewModel
            var loginVM = _serviceProvider.GetRequiredService<LoginViewModel>();
            loginVM.LoginSucceeded += ShowControlPanel;
            Content = loginVM;
        }

        private void ShowControlPanel()
        {
            var controlPanelVM = _serviceProvider.GetRequiredService<MainViewModel>();
            Content = controlPanelVM;
        }
    }


}