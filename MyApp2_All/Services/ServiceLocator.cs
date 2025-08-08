using System;
using Avalonia;
using Avalonia.Controls;
// using Dpa.Library.ViewModels;

//using Dpa.DesignViewModels;
// using Dpa.Library.Services;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;
using MyApp2.ViewModels;
// using Dpa.Service;
// using Dpa.Services;
// using DailyPoetryA.Library.Services; // Required for GetRequiredService
// using Dpa.Services;

namespace MyApp2.Services
{
    public class ServiceLocator
    {
        private readonly IServiceProvider _serviceProvider;

        private static ServiceLocator? _current;

        public static ServiceLocator Current
        {
            get
            {
                if (_current is not null)
                {
                    return _current;
                }

                if (Application.Current.TryGetResource(nameof(ServiceLocator),
                        out var resource) &&
                    resource is ServiceLocator serviceLocator)
                {
                    return _current = serviceLocator;
                }

                throw new Exception("理论上来讲不应该发生这种情况。");
            }
        }


        public ServiceLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public T GetRequiredService<T>() where T : notnull
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
