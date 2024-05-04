using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

// THIS SERVICE WILL HANDEL ALL FILE-RELATED OPERATIONS, PROVIDING A METHOD TO READ THE CSV FILE ASYNCHRONOUSLY
namespace MauiStellar2.Services
{
    public class FileIOService
    {
        public async Task<String> ReadFileAsync(string filePath)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(filePath))
            {
                if (stream == null)
                {
                    throw new FileNotFoundException("Embedded resource not found: " + filePath);
                }
                using (StreamReader reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
