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

        // Commands accessible from the UI to perform various actions.
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
                OnPropertyChanged();// Notify that Horoscope has changed.
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

        // Loads horoscope data for a given zodiac sign.
        public async Task loadHoroscope(string sign)
        {
            Horoscope = await _service.getHoroscope(sign);
        }

        // Converts horoscope details to CSV format.
        public static string toCsv(Horoscope horoscope)
        {
            var csv = new StringBuilder();
            csv.AppendLine("Prediction,Number,Color,Mantra,Remedy");
            csv.AppendLine($"\"{escape(horoscope.Prediction)}\",\"{escape(horoscope.Number)}\",\"{escape(horoscope.Color)}\",\"{escape(horoscope.Mantra)}\",\"{escape(horoscope.Remedy)}\"");
            return csv.ToString();
        }

        // Helper method to escape special characters in CSV data.
        private static string escape(string input)
        {
            // Replace all double quotes in the string with two double quotes to escape them in the CSV.
            // CSV formats require double quotes in data to be escaped by doubling them.
            input = input.Replace("\"", "\"\"");

            // Replace newline characters with spaces. Newlines in a single CSV field can break the file's structure,
            // leading to records spanning multiple lines, which can confuse parsers.
            input = input.Replace("\n", " ");

            // Replace carriage return characters with spaces for the same reason as newlines: to prevent the CSV format from breaking.
            input = input.Replace("\r", " ");

            // Replace semicolons with commas to prevent them from being interpreted as delimiters.
            // Semicolons are often used as delimiters in CSV files, so their presence in data could split a single field into multiple fields.
            input = input.Replace(";", ",");

        }

        // Converts horoscope details to XAML format.
        public static string toXaml(Horoscope horoscope)
        {
            var xaml = $"<Horoscope xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Status=\"{horoscope.Status}\" Prediction=\"{horoscope.Prediction}\" Number=\"{horoscope.Number}\" Color=\"{horoscope.Color}\" Mantra=\"{horoscope.Mantra}\" Remedy=\"{horoscope.Remedy}\"/>";
            return xaml;
        }

        // Saves horoscope in either CSV or XAML format.
        public void saveHoroscope(string format, Horoscope horoscope)
        {
            // Use ThreadPool to run the save operation on a background thread, avoiding UI thread blockage.
            ThreadPool.QueueUserWorkItem(_ =>
            {
                // Determine the content format based on the input, calling the appropriate method to convert data.
                string content = format == "CSV" ? toCsv(horoscope) : toXaml(horoscope);

                // Construct a filename using the format and convert format to lowercase for consistency.
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
                    // Write content to file.
                    File.WriteAllText(filePath, content);
                    System.Diagnostics.Debug.WriteLine($"Horoscope saved as {format} at {filePath}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to save horoscope: {ex.Message}");
                }
            });
        }

        // Notifies listeners about property changes.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Method to navigate back to the previous view.
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

        // Event to notify when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
