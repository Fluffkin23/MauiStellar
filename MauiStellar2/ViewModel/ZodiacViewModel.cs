using MauiStellar2.Model;
using MauiStellar2.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;
using System.Windows.Input;
using MauiStellar2.Views;  // Ensure this namespace is included


namespace MauiStellar2.ViewModel
{
    public class ZodiacViewModel
    {
        public ObservableCollection<ZodiacSign> ZodiacSigns { get; set; } = new ObservableCollection<ZodiacSign>();
        private readonly ZodiacService _zodiacService;
        public ICommand NavigateToHoroscopeCommand { get; set; }


        public ZodiacViewModel(ZodiacService zodiacService)
        {
            _zodiacService = zodiacService;
            Task.Run(() => loadDataAsync()); // Run loading on a background thread
            NavigateToHoroscopeCommand = new Command(async () => await navigateToHoroscopeAsync());
        }

        private async Task loadDataAsync()
        {
            try
            {
                var signs = await _zodiacService.loadZodiacSignsAsync("MauiStellar2.Resources.zodiac_Signs.csv");
                foreach (var sign in signs)
                {
                    // Use Dispatch to ensure updates to ObservableCollection happen on the UI thread
                    App.Current.Dispatcher.Dispatch(() =>
                    {
                        ZodiacSigns.Add(sign);
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading zodiac signs: {ex.Message}");
            }
        }

        private async Task navigateToHoroscopeAsync()
        {
            try
            {
                // Navigate to HoroscopePage using Shell navigation
                await Shell.Current.GoToAsync("///HoroscopePageView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }
    }
}
