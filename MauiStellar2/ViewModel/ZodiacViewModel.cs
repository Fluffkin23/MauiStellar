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
        public ObservableCollection<ZodiacSign> ZodiacSigns { get; private set; } = new ObservableCollection<ZodiacSign>();
        private readonly ZodiacService _zodiacService;
        public ICommand NavigateToHoroscopeCommand { get; }





        public ZodiacViewModel(ZodiacService zodiacService)
        {
            _zodiacService = zodiacService;



            Task.Run(() => LoadDataAsync()); // Run loading on a background thread
            NavigateToHoroscopeCommand = new Command(async () => await NavigateToHoroscopeAsync());

        }

        private async Task LoadDataAsync()
        {
            try
            {
                var signs = await _zodiacService.LoadZodiacSignsAsync("MauiStellar2.Resources.zodiac_Signs.csv");
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

        private async Task NavigateToHoroscopeAsync()
        {
           

            try
            {
                // Navigate to HoroscopePage using Shell navigation
                await Shell.Current.GoToAsync("///HoroscopePage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }


    }
}
