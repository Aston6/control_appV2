using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using MyApp2.Services;
using MyApp2.ViewModels;
using MyApp2.Libary.Services;
using MyApp2.Data;
using Microsoft.EntityFrameworkCore;
using System;
using MyApp2.Models;
using System.Linq;
using static MyApp2.Services.UserService;

namespace MyApp2;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));

        services.AddSingleton<IRootNavigationService, RootNavigationService>();
        services.AddSingleton<IUserService, UserService>();

        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<UsersViewModel>();
        services.AddTransient<ReportsViewModel>();
        services.AddTransient<DashboardViewModel>();
        services.AddTransient<ControlPanelViewModel>();

        var serviceProvider = services.BuildServiceProvider();

        // ✅ Seed first admin user
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate(); // or EnsureCreated()

            if (!db.Users.Any()|| db.Users.Any(u => string.IsNullOrEmpty(u.Username)))
            {
                var hash = BCrypt.Net.BCrypt.HashPassword("admin123");
                db.Users.Add(new User { Username = "admin", PasswordHash = hash });
                db.SaveChanges();
                Console.WriteLine("✔ Default admin user created (admin / admin123)");
            }
        }

        var locator = new ServiceLocator(serviceProvider);
        this.Resources.Add(nameof(ServiceLocator), locator);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
