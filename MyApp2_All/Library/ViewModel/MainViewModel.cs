using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MyApp2.Libary.Services;

namespace MyApp2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
    private readonly IRootNavigationService _navigation;
        
    public ICommand NavigateToDashboardCommand { get; }
    public ICommand NavigateToSettingsCommand { get; }
    public ICommand NavigateToUsersCommand { get; }
    public ICommand NavigateToReportsCommand { get; }
    public ICommand NavigateToControlPanelCommand{ get; }

        public MainViewModel(IRootNavigationService navigation, IServiceProvider services)
        {
            _navigation = navigation;

            NavigateToDashboardCommand = new RelayCommand(() =>
                CurrentView = services.GetRequiredService<DashboardViewModel>());

            NavigateToSettingsCommand = new RelayCommand(() =>
                CurrentView = services.GetRequiredService<SettingsViewModel>());

            NavigateToUsersCommand = new RelayCommand(() =>
                CurrentView = services.GetRequiredService<UsersViewModel>());

            NavigateToReportsCommand = new RelayCommand(() =>
                CurrentView = services.GetRequiredService<ReportsViewModel>());
            NavigateToControlPanelCommand = new RelayCommand(() =>
                CurrentView = services.GetRequiredService<ControlPanelViewModel>());
        }

        private ViewModelBase? _currentView;
        public ViewModelBase? CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }



        // private void NavigateToDashboard() => Console.WriteLine("Navigate TO Dashboard was called");
        // private void NavigateToSettings() => Console.WriteLine("Navigate TO Settings was called");
        // private void NavigateToUsers() => Console.WriteLine("Navigate TO Users was called");
        // private void NavigateToReports() => Console.WriteLine("Navigate TO Reports was called");
    }

    // Simple ICommand implementation
    
}
