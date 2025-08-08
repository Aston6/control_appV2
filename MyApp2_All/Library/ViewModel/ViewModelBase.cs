using CommunityToolkit.Mvvm.ComponentModel;
// using Dpa.Library.Services;

namespace MyApp2.ViewModels;

public class ViewModelBase : ObservableObject
{
    public virtual void SetParameter(object parameter)
    {
    }
}