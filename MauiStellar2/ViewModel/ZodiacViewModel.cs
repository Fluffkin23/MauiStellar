using MauiStellar2.Model;
using MauiStellar2.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiStellar2.ViewModel
{
    public class ZodiacViewModel
    {
        public ObservableCollection<ZodiacSign> ZodiacSigns { get; private set; } = new ObservableCollection<ZodiacSign>();
        private readonly ZodiacService _zodiacService;

        public ZodiacViewModel(ZodiacService zodiacService)
        {
            _zodiacService = zodiacService;
            Task.Run(() => LoadDataAsync()); // Run loading on a background thread
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var signs = await _zodiacService.LoadZodiacSignsAsync("MauiStellar2.Resources.zodiac_Signs.csv");
                // To ensure thread safety and UI thread access
                foreach (var sign in signs)
                {
                    ZodiacSigns.Add(sign);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your logging strategy
                Console.WriteLine($"Error loading zodiac signs: {ex.Message}");
            }
        }
    }
}
