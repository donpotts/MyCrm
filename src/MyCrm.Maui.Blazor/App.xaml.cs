namespace MyCrm.Maui.Blazor;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

		window.Title = "The Games";

		return window;
    }
}

