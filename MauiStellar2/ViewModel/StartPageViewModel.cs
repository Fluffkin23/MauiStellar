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
        public ICommand ExploreCommand { get; private set; }

        public StartPageViewModel()
        {
            ExploreCommand = new Command(OnExploreExecuted);
            
        }

        private async void OnExploreExecuted()
        {
            try
            {
                await Shell.Current.GoToAsync("///ZodiacSignView");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to navigate: {ex.Message}");
            }
        }
    }
}
