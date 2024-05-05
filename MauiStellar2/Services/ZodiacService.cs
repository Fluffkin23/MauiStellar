using MauiStellar2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStellar2.Services
{
    public class ZodiacService
    {
        //Private member to hold the instance of FileIOService
        private readonly FileIOService _fileIOService;

        // Constructor to inject the FileIOService dependecy.
        //This allows the ZodiacService to access file operations provided by FileIOService, promoting loose coupling.
        public ZodiacService(FileIOService fileIOService)
        { 
            _fileIOService = fileIOService;
        }

        // Public asynchronous method to load zodiac sign from a CSV file.
        // It returns an ObservableCollection of ZodiacSign objects which can be used for data binding in MVVM.
        // The method takes the file path as a parameter.
        public async Task<ObservableCollection<ZodiacSign>> LoadZodiacSignsAsync(string filepath)
        {
            ObservableCollection<ZodiacSign> zodiacSigns = new ObservableCollection<ZodiacSign>();
            List<ZodiacSign> tempList = new List<ZodiacSign>();  // Temporary list to store signs

            string fileContent = await _fileIOService.ReadFileAsync(filepath);
            string[] lines = fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Parallel.ForEach(lines, (line, state, index) =>
            {
                if (index == 0) return;  // Skip the first line

                if (!string.IsNullOrEmpty(line))
                {
                    string[] parts = line.Split(';');
                    if (parts.Length >= 5)
                    {
                        tempList.Add(new ZodiacSign(parts[0], parts[1], parts[2], parts[3], parts[4]));
                    }
                }
            });

            // Now update the ObservableCollection on the UI thread
            await App.Current.Dispatcher.DispatchAsync(() =>
            {
                foreach (var sign in tempList)
                {
                    zodiacSigns.Add(sign);
                }
            });

            return zodiacSigns;
        }

    }
}
