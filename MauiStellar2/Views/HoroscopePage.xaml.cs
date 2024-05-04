using MauiStellar2.ViewModel;

namespace MauiStellar2.Views;

public partial class HoroscopePage : ContentPage
{
    private  HoroscopeViewModel _viewModel;

    public HoroscopePage()
	{
        InitializeComponent();

        // Initialize the ViewModel
        _viewModel = new HoroscopeViewModel();

        // Set the binding context of the page to the ViewModel
        this.BindingContext = _viewModel;
    }

    

}