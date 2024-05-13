using MauiStellar2.ViewModel;

namespace MauiStellar2.Views;

public partial class HoroscopePageView : ContentPage
{
    private  HoroscopeViewModel _viewModel;

    public HoroscopePageView()
	{
        InitializeComponent();

        // Initialize the ViewModel
        _viewModel = new HoroscopeViewModel();

        // Set the binding context of the page to the ViewModel
        this.BindingContext = _viewModel;
    }

    

}