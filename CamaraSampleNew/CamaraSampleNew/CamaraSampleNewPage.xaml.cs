using System;
using System.IO;
using Xamarians.Media;
using Xamarin.Forms;

namespace CamaraSampleNew
{
    public partial class CamaraSampleNewPage : ContentPage
    {
        public CamaraSampleNewPage()
        {
            InitializeComponent();
        }
		private string GenerateFilePath()
		{
			return Path.Combine(MediaService.Instance.GetPublicDirectoryPath(), MediaService.Instance.GenerateUniqueFileName("jpg"));
		}

		private async void TakePhoto_Clicked(object sender, EventArgs e)
		{
			string filePath = GenerateFilePath();
			var result = await MediaService.Instance.TakePhotoAsync(new CameraOption()
			{
				FilePath = filePath,
				MaxWidth = 300,
				MaxHeight = 300
			});
            if (result.IsSuccess)
            {
                image.Source = result.FilePath;
                FileImageSource obj = new FileImageSource();
            }
			else
				await DisplayAlert("Error", result.Message, "OK");
		}

		private async void ChooseImage_Clicked(object sender, EventArgs e)
		{
            MediaResult result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Image);
			if (result.IsSuccess)
				image.Source = result.FilePath;
			else
				await DisplayAlert("Error", result.Message, "OK");
		}

		private async void ChooseVideo_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Video);
			if (result.IsSuccess)
				await DisplayAlert("Success", result.FilePath, "OK");
			else
				await DisplayAlert("Error", result.Message, "OK");
		}


		private async void ChooseAudio_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Audio);
			if (result.IsSuccess)
				await DisplayAlert("Success", result.FilePath, "OK");
			else
				await DisplayAlert("Error", result.Message, "OK");
		}

		private async void ResizeImage_Clicked(object sender, EventArgs e)
		{
			var result = await MediaService.Instance.OpenMediaPickerAsync(MediaType.Image);
			string resizeFilePath = GenerateFilePath();
			var success = await MediaService.Instance.ResizeImageAsync(result.FilePath, resizeFilePath, 250, 250);
			if (success)
			{
				image.Source = resizeFilePath;
			}
		}
	}
}


