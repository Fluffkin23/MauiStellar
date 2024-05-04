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
            bool isFirstLine = true;

            // Create an ObservableCollection to hold the zodiac signs. This collection will be returned by the method.
            ObservableCollection<ZodiacSign> zodiacSigns = new ObservableCollection<ZodiacSign>();

            // Read the entire content of the CSV file asynchronously into a string. This method call will
            // utilize the FileIOService to perform the file read operation.
            string fileContent = await _fileIOService.ReadFileAsync(filepath);

            // Split the file content into lines based on the new line character. Each line represents a zodiac sign entry.
            string[] lines = fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string line in lines) 
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // Skip the header row
                }
                // Check if the line is not null or empty to avoid processing invalid entries.
                if (!string.IsNullOrEmpty(line))
                {
                    // Split the line into parts using the comma delimiter. Each part represents a property of the ZodiacSign.
                    string[]parts = line.Split(';');

                    // Check if there are at least 3 parts, corresponding to the expected number of properties for a ZodiacSign.
                    if (parts.Length >= 3) 
                    {
                        // Create a new ZodiacSign object with the parts from the line and add it to the ObservableCollection.
                        // This constructor initializes a new ZodiacSign with its properties set from the parts.
                       zodiacSigns.Add(new ZodiacSign(parts[0], parts[1], parts[2], parts[3], parts[4]));
                       Console.WriteLine($"Added: {parts[0]}, {parts[1]}, {parts[2]}"); // Debugging output

                    }
                }
            }
            return zodiacSigns;
        }
    }
}
