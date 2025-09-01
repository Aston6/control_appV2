using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using MyApp2.Services;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System;

namespace MyApp2.ViewModels
{
    public partial class BuyListViewModel : ViewModelBase
    {
        private readonly IImagePickerService _imagePicker;
        public ICommand UploadImageCommand { get; }

        // Save inside project folder
        private readonly string _imageFolder = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Images");

        [ObservableProperty]
        private ObservableCollection<Bitmap> images = new ObservableCollection<Bitmap>();

        public BuyListViewModel(IImagePickerService imagePicker)
        {
            _imagePicker = imagePicker;

            Directory.CreateDirectory(_imageFolder);

            // Load previously saved images
            LoadSavedImages();

            UploadImageCommand = new RelayCommand(async () => await UploadImageAsync());
        }

        public async Task UploadImageAsync()
        {
            var path = await _imagePicker.PickImageAsync();
            if (path != null && File.Exists(path))
            {
                // Create unique filename for each image
                var fileName = $"image_{DateTime.Now:yyyyMMddHHmmss}.png";
                var destPath = Path.Combine(_imageFolder, fileName);

                File.Copy(path, destPath, true);

                // Add to the list
                Images.Add(new Bitmap(destPath));
            }
        }

        private void LoadSavedImages()
        {
            var files = Directory.GetFiles(_imageFolder, "*.png");
            foreach (var file in files)
            {
                Images.Add(new Bitmap(file));
            }
        }
    }
}
