namespace WorldTime;

public partial class test : ContentPage
{
	public test()
	{
		InitializeComponent();
        webView.Source = new UrlWebViewSource
        {
            Url = "https://earth.google.com"
        };
    }
}