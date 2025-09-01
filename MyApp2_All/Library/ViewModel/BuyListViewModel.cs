using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Media.Imaging;
using MyApp2.Services;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Windows.Input;

namespace MyApp2.ViewModels
{
    public partial class BuyListViewModel : ViewModelBase
    {
        private readonly IImagePickerService _imagePicker;
        public ICommand UploadImageCommand { get; }

        // Save inside project folder
        private readonly string _imageFolder = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Images");

        private readonly string _imageFileName = "saved_image.png";

        [ObservableProperty]
        private Bitmap? selectedImage;

        public BuyListViewModel(IImagePickerService imagePicker)
        {
            _imagePicker = imagePicker;

            // Ensure the folder exists in the project
            Directory.CreateDirectory(_imageFolder);

            // Load previously saved image
            LoadSavedImage();

            // Command
            UploadImageCommand = new RelayCommand(async () => await UploadImageAsync());
        }

        public async Task UploadImageAsync()
        {
            var path = await _imagePicker.PickImageAsync();
            if (path != null && File.Exists(path))
            {
                var destPath = Path.Combine(_imageFolder, _imageFileName);

                // Copy image to project folder
                File.Copy(path, destPath, true);

                // Load the image
                SelectedImage = new Bitmap(destPath);
            }
            Console.WriteLine("Image folder: " + _imageFolder);

        }

        private void LoadSavedImage()
        {
            var savedPath = Path.Combine(_imageFolder, _imageFileName);
            if (File.Exists(savedPath))
            {
                SelectedImage = new Bitmap(savedPath);
            }
        }
    }
}
