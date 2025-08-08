

namespace MyApp2.Libary.Services;

public interface IRootNavigationService
{
    void NavigateTo(string view);

}

public static class RootNavigationConstants
{
    public const string LoginView = "Login";
    public const string ControlPanelView = "ControlPanel";
    public const string SettingsView = "Settings";
    public const string UsersView = "Users";
    public const string ReportsView = "Reports";
    public const string MainView = "Main";
    public const string DashboardView = "Dashboard";
}


