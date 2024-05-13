using MauiStellar2.Model;
using MauiStellar2.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiStellar2.ViewModel
{
    public class HoroscopeViewModel: INotifyPropertyChanged
    {
        private Horoscope _horoscope;
        private HoroscopeService _service = new HoroscopeService();
        public ICommand LoadHoroscopeCommand { get; set; }  
        public ICommand SaveAsCsvCommand { get; set; }
        public ICommand SaveAsXamlCommand { get; set; }
        public ICommand GoBackCommand { get; }

        public Horoscope Horoscope
        {
            get => _horoscope;
            set
            {
                _horoscope = value;
                OnPropertyChanged();
            }
        }

        public HoroscopeViewModel()
        {
            // Initialize the command and link it to LoadHoroscope method
            LoadHoroscopeCommand = new Command<string>(async (sign) => await loadHoroscope(sign));
            SaveAsCsvCommand = new Command(() => saveHoroscope("CSV", Horoscope));
            SaveAsXamlCommand = new Command(() => saveHoroscope("XAML", Horoscope));
            GoBackCommand = new Command(async () => await goBack());
        }

        public async Task loadHoroscope(string sign)
        {
            Horoscope = await _service.getHoroscope(sign);
        }


        public static string toCsv(Horoscope horoscope)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Status,Prediction,Number,Color,Mantra,Remedy");
            csv.AppendLine($"{horoscope.Status},{horoscope.Prediction},{horoscope.Number},{horoscope.Color},{horoscope.Mantra},{horoscope.Remedy}");
            return csv.ToString();
        }

        public static string toXaml(Horoscope horoscope)
        {
            var xaml = $"<Horoscope xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Status=\"{horoscope.Status}\" Prediction=\"{horoscope.Prediction}\" Number=\"{horoscope.Number}\" Color=\"{horoscope.Color}\" Mantra=\"{horoscope.Mantra}\" Remedy=\"{horoscope.Remedy}\"/>";
            return xaml;
        }

        public void saveHoroscope(string format, Horoscope horoscope)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string content = format == "CSV" ? toCsv(horoscope) : toXaml(horoscope);
                string filename = $"horoscope.{format.ToLower()}";

                // Get the app-specific directory path that is suitable for each platform
                string folderPath = FileSystem.Current.AppDataDirectory; // App-specific data directory

                // Ensure the folder exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, filename);
                try
                {
                    File.WriteAllText(filePath, content);
                    System.Diagnostics.Debug.WriteLine($"Horoscope saved as {format} at {filePath}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to save horoscope: {ex.Message}");
                }
            });
        }


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task goBack()
        {

            try
            {
                // Navigate to HoroscopePage using Shell navigation
                await Shell.Current.GoToAsync("///ZodiaSignView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
