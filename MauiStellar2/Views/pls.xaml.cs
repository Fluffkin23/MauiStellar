using MauiStellar2.Services;
using MauiStellar2.ViewModel;

namespace MauiStellar2.Views;

public partial class pls : ContentPage
{
	public pls()
	{
		InitializeComponent();
        //var fileIOService = new FileIOService(); // Assuming you have a FileIOService implementation
       // var zodiacService = new ZodiacService(fileIOService);
        //this.BindingContext = new ZodiacViewModel(zodiacService);

            BindingContext = new ZodiacViewModel(new ZodiacService(new FileIOService()));

    }
}