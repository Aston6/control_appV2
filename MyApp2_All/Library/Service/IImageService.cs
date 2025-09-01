using Avalonia.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp2.Services
{
    public class ImagePickerService : IImagePickerService
    {
        public async Task<string?> PickImageAsync()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Select an image",
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter() { Name = "Image files", Extensions = { "png", "jpg", "jpeg", "bmp" } }
                },
                AllowMultiple = false
            };

            var window = Avalonia.Application.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop
                ? desktop.MainWindow
                : null;

            var result = await dialog.ShowAsync(window);
            return result?.Length > 0 ? result[0] : null;
        }
    }
}

namespace MyApp2.Services
{
    public interface IImagePickerService
    {
        Task<string?> PickImageAsync();
    }
}
