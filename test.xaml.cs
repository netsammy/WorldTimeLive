namespace WorldTime;

#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
public partial class test : ContentPage
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
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