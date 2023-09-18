

namespace WorldTime;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        //MainPage = new AppShell {            
        //    FlyoutIcon="earth.gif",
            
        //};
        // WorldTimePage = new AppShell();

        //add earth.gif image to the app
        // MainPage.BackgroundImageSource = "earth.gif";


    }
}
