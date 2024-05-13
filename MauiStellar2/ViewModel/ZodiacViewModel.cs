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
        // Collection to hold the ZodiacSign objects in a way that notifies views of any changes.
        public ObservableCollection<ZodiacSign> ZodiacSigns { get; set; } = new ObservableCollection<ZodiacSign>();

        // Service to fetch zodiac signs data.
        private readonly ZodiacService _zodiacService;

        // Command for navigating to the horoscope page.
        public ICommand NavigateToHoroscopeCommand { get; set; }

        public ZodiacViewModel(ZodiacService zodiacService)
        {
            _zodiacService = zodiacService;

            // Run loading on a background thread
            Task.Run(() => loadDataAsync());
            // Initialize navigation command.

            NavigateToHoroscopeCommand = new Command(async () => await navigateToHoroscopeAsync());
        }

        // Asynchronously load zodiac signs from a CSV file.
        private async Task loadDataAsync()
        {
            try
            {
                // Fetch zodiac signs using the service.
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
                // Log any errors during the loading process.
                Console.WriteLine($"Error loading zodiac signs: {ex.Message}");
            }
        }

        // Method to navigate to the HoroscopePage using Shell routing.
        private async Task navigateToHoroscopeAsync()
        {
            try
            {
                // Navigate to HoroscopePage using Shell navigation
                await Shell.Current.GoToAsync("///HoroscopePageView");
            }
            catch (Exception ex)
            {
                // Debug output if navigation fails.
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }
    }
}
