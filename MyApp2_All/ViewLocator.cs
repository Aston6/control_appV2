using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;

namespace MyApp2
{
    public class ViewLocator : IDataTemplate
    {
        // This implements IDataTemplate.Build with correct signature
        public Control? Build(object? param)
        {
            if (param == null)
                return null;

            // Replace ViewModel with View in the full type name
            var name = param.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type == null)
                return new TextBlock { Text = $"View not found: {name}" };

            var view = Activator.CreateInstance(type) as Control;
            if (view != null)
                view.DataContext = param;

            return view;
        }

        public bool Match(object? data)
        {
            return data?.GetType().Name.EndsWith("ViewModel") ?? false;
        }
    }
}
