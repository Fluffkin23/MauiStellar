namespace MauiStellar2.Views;

// Define the startPage class as a page withing the application
public partial class startPage : ContentPage
{
    // Constructor for the StartPage class.
    public startPage()
	{
        // it initializes the page's components.
        InitializeComponent();
    }

    // OnAppearing is called when the page is about to appear on screen.
    protected override async void OnAppearing()
    {
        // Call the base class's OnAppearing method
        base.OnAppearing();

        //Prevent the animations from running if it is already running
        if(this.AnimationIsRunning("TransitionAnimation"))
            return;

        //Create a parent animation that will contain all sub-animations.
        var parentAnimation = new Animation();

        // Define opacity animations for each planet image, gradually making them visible.
        parentAnimation.Add(0, 0.2, new Animation(v => imgCapricon.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.1, 0.3, new Animation(v => imgLeo.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.2, 0.4, new Animation(v => imgPisces.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.3, 0.5, new Animation(v => imgCancer.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.4, 0.6, new Animation(v => imgAquarius.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.5, 0.7, new Animation(v => imgAries.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.6, 0.8, new Animation(v => imgScorpio.Opacity = v, 0, 1, Easing.CubicIn));
        parentAnimation.Add(0.7, 0.9, new Animation(v => imgLibra.Opacity = v, 0, 1, Easing.CubicIn));

        //Define an opacity animation for the intro box, making it visible
        parentAnimation.Add(0.7, 1, new Animation(v => frmIntro.Opacity = v, 0, 1, Easing.CubicIn));

        //Commit the animation to the page, specifying details like duration and repeat behavior
        parentAnimation.Commit(this, "TransitionAnimation", 16, 3000, null, null);

       

    }
}