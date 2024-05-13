using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiStellar2.ViewModel
{
    public class StartPageViewModel
    {
        // ICommand property for binding UI actions like button clicks to commands.
        public ICommand ExploreCommand { get; private set; }

        public StartPageViewModel()
        {
            // Initialize the ExploreCommand with the OnExploreExecuted method as its action.
            ExploreCommand = new Command(onExploreExecuted);
            
        }

        // Method executed when the Explore command is triggered.
        private async void onExploreExecuted()
        {
            try
            {
                // Use Shell navigation to asynchronously navigate to the ZodiacSignView.
                // The triple-slash (///) prefix in the route ensures navigation within the current route hierarchy.
                await Shell.Current.GoToAsync("///ZodiacSignView");
            }
            catch (Exception ex)
            {
                // Log the exception to the debug output if navigation fails.
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }
    }
}
