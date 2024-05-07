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
        public ICommand LoadHoroscopeCommand { get; private set; }  // ICommand property
        public ICommand SaveAsCsvCommand { get; private set; }
        public ICommand SaveAsXamlCommand { get; private set; }
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
            LoadHoroscopeCommand = new Command<string>(async (sign) => await LoadHoroscope(sign));
            SaveAsCsvCommand = new Command(() => SaveHoroscope("CSV", Horoscope));
            SaveAsXamlCommand = new Command(() => SaveHoroscope("XAML", Horoscope));
            GoBackCommand = new Command(async () => await GoBack());



        }

        public async Task LoadHoroscope(string sign)
        {
            Horoscope = await _service.GetHoroscope(sign);
            

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static string ToCsv(Horoscope horoscope)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Status,Prediction,Number,Color,Mantra,Remedy");
            csv.AppendLine($"{horoscope.Status},{horoscope.Prediction},{horoscope.Number},{horoscope.Color},{horoscope.Mantra},{horoscope.Remedy}");
            return csv.ToString();
        }

        public static string ToXaml(Horoscope horoscope)
        {
            var xaml = $"<Horoscope xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Status=\"{horoscope.Status}\" Prediction=\"{horoscope.Prediction}\" Number=\"{horoscope.Number}\" Color=\"{horoscope.Color}\" Mantra=\"{horoscope.Mantra}\" Remedy=\"{horoscope.Remedy}\"/>";
            return xaml;
        }

        public void SaveHoroscope(string format, Horoscope horoscope)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                string content = format == "CSV" ? ToCsv(horoscope) : ToXaml(horoscope);
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

        private async Task GoBack()
        {

            try
            {
                // Navigate to HoroscopePage using Shell navigation
                await Shell.Current.GoToAsync("///pls");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }
    }
}
