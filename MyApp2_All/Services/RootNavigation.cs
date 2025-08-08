using System;
// using Dpa.App;
// using Dpa.Library.Services;
// using Dpa.Library.ViewModels;
using MyApp2.Libary.Services;


using MyApp2.ViewModels;

namespace MyApp2.Services;

public class RootNavigationService : IRootNavigationService
{

    public void NavigateTo(string view)
    {
    Console.WriteLine($"NavigateTo was called with: {view}");

    var mainVM = ServiceLocator.Current.GetRequiredService<MainWindowViewModel>();

    mainVM.Content = view switch
    {
        RootNavigationConstants.LoginView => ServiceLocator.Current.GetRequiredService<LoginViewModel>(),
        RootNavigationConstants.ControlPanelView => ServiceLocator.Current.GetRequiredService<MainViewModel>(),
        RootNavigationConstants.SettingsView => ServiceLocator.Current.GetRequiredService<SettingsViewModel>(),
        RootNavigationConstants.UsersView => ServiceLocator.Current.GetRequiredService<UsersViewModel>(),
        RootNavigationConstants.ReportsView => ServiceLocator.Current.GetRequiredService<ReportsViewModel>(),
        RootNavigationConstants.MainView =>ServiceLocator.Current.GetRequiredService<MainViewModel>(),
        RootNavigationConstants.DashboardView=>ServiceLocator.Current.GetRequiredService<DashboardViewModel>(),
        
        _ => throw new Exception($"Unknown view: {view}")
    };

    }


    // private IMenuNavigationService _menuNavigationService;

    // public RootNavigationService(IMenuNavigationService menuNavigationService)
    // {
    //     _menuNavigationService = menuNavigationService;
    // }
    // public void NavigateTo(string view)
    // {
    //     if (view == RootNavigationConstants.MainView)
    //     {
    //         ServiceLocator.Current.MainWindowViewModel.Content =
    //             ServiceLocator.Current.MainViewModel;
    //         _menuNavigationService.NavigateTo(MenuNavigationConstant.TodayView);

    //     }
    // }
}