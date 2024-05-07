
namespace MauiStellar2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);

            const int newWidth = 500;
            const int newHeight = 800;

            

            window.Width = newWidth;
            window.Height = newHeight;

            window.MinimumHeight = newHeight;
            window.MinimumWidth = newWidth;

            window.MaximumHeight = newHeight;
            window.MaximumWidth = newWidth;


            return window;


        }
    }
}
