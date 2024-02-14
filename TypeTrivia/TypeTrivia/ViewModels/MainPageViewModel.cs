using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TypeTrivia.Models;
using TypeTrivia.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TypeTrivia.ViewModels
{
    public class MainPageViewModel
    {
        public IAsyncCommand AttackCommand { get; }
        public IAsyncCommand DefenseCommand { get; }
        public IAsyncCommand DualTypeAttackingCommand { get; }

        public IAsyncCommand WebsiteCommand { get; }
        public IAsyncCommand CameraCommand { get; }

        private string PhotoPath = "";
        public MainPageViewModel()
        {
            AttackCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(AttackingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 0);
            });
            DefenseCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(DefendingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 1);
            });
            DualTypeAttackingCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(DualTypeAttackingPage)}");
                MessagingCenter.Send<MainPageViewModel, int>(this, "QuestionType", 2);
            });
            WebsiteCommand = new AsyncCommand(async () =>
            {
                await Shell.Current.GoToAsync($"{nameof(WebsiteView)}");
            });
            CameraCommand = new AsyncCommand(async () =>
            {
                await TakePhotoAsync();
            });
        }

        async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        async Task LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
    }
}
