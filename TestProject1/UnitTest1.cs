namespace MauiStellarTests
{
    [TestFixture]
    public class ZodiacTests
    {
        [Test]
        public async Task TestZodiacViewModel()
        {
            var fileIOService = new FileIOService(); // Ensure this service is correctly implemented to read files.
            var zodiacService = new ZodiacService(fileIOService);
            var viewModel = new ZodiacViewModel(zodiacService);

            // Wait for data to load. Ensure LoadDataAsync is accessible for testing.
            await viewModel.LoadDataAsync();

            // Output the results to the test console.
            foreach (var sign in viewModel.getZodiacSigns())
            {
                Console.WriteLine($"Name: {sign.getName}, Description: {sign.getDescription}, Personality: {sign.getPersonality}");
                // Optionally add assertions to verify correct behavior
                Assert.IsNotNull(sign.getName());
                Assert.IsNotNull(sign.getDescription());
                Assert.IsNotNull(sign.getPersonality());
            }
        }
    }
}
