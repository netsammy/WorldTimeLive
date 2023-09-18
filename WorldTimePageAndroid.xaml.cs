using WorldTime.ViewModels;
namespace WorldTime;

public partial class WorldTimePageAndroid : ContentPage
{
    public WorldTimePageAndroid()
    {
        InitializeComponent();
        BindingContext = new WorldTimePageViewModel();


        //set navigation bar color to transparent        


       


        //set background image source
        //do not strech image
        //var backgroundImage = new Image { Source = "giphy234.gif",IsAnimationPlaying=true };
        //this.BackgroundImageSource= backgroundImage.Source;

        var backgroundImage = new Image { Source = "giphy234.gif",IsAnimationPlaying=true,Aspect= Aspect.Center };
        //backgroundImage.Aspect = Aspect.AspectFill;
        
        this.BackgroundImageSource= backgroundImage.Source;

        //Shell.SetSearchHandler(this, new SearchHandler
        //{
        //    Placeholder = "Enter ",
        //    ShowsResults = true,
        //   // SelectedItemNavigationTarget = typeof(UserDetailPage),
        //   // Users = UserData.Users,
        //    ItemTemplate = new DataTemplate(() =>
        //    {
        //        Grid grid = new Grid { Padding = 10 };
        //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) });
        //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.85, GridUnitType.Star) });

        //        Image image = new Image { HeightRequest = 40, WidthRequest = 40 };
        //        image.SetBinding(Image.SourceProperty, "Images[1].ImageUri");
        //        Label nameLabel = new Label { FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.Center };
        //        nameLabel.SetBinding(Label.TextProperty, "Name");

        //        grid.Children.Add(image);
        //        grid.Children.Add(nameLabel);

        //        return grid;
        //    })
        //});
        //webView.Source = new UrlWebViewSource
        //{
        //    //Url = "https://www.msn.com/en-us/weather/maps/temperature/in-Karachi,Sindh?loc=eyJsIjoiS2FyYWNoaSIsInIiOiJTaW5kaCIsImMiOiJQYWtpc3RhbiIsImkiOiJQSyIsImciOiJlbi11cyIsIngiOiI2Ny4wODIyIiwieSI6IjI0LjkwNTYifQ%3D%3D&weadegreetype=C&cvid=976733ce1cf542b594232ad2f61e3d91&zoom=4&3d=1&ocid=winp2fptaskbar"
        //    //Url = "https://earth.google.com/web/@9.23782166,71.27953178,-5971.37160656a,18139435.85241795d,35y,-0h,0t,0r"

        //    //Url = "https://earth.google.com"
        //    //Url= "https://www.msn.com/en-xl/weather/maps/temperature/in-Karachi,Sindh?loc=eyJsIjoiS2FyYWNoaSIsInIiOiJTaW5kaCIsImMiOiJQYWtpc3RhbiIsImkiOiJQSyIsInQiOjEsImciOiJlbi14bCIsIngiOiI2Ny4wODIyIiwieSI6IjI0LjkwNTYifQ%3D%3D&weadegreetype=C&cvid=bfcb42158f794b7f928def6b2b87a882&zoom=3&3d=1&ocid=msedgntp"

        //};

        //Preferences.Set("SelectedTimeZones", "");

        //        // Mark Pakistan on the map
        //        string scriptPakistan = @"
        //var markerPakistan = ge.createPlacemark('');
        //var pointPakistan = ge.createPoint('');
        //pointPakistan.setLatitude(30.3753);
        //pointPakistan.setLongitude(69.3451);
        //markerPakistan.setGeometry(pointPakistan);
        //ge.getFeatures().appendChild(markerPakistan);
        //";

        //        // Mark USA on the map
        //        string scriptUSA = @"
        //var markerUSA = ge.createPlacemark('');
        //var pointUSA = ge.createPoint('');
        //pointUSA.setLatitude(37.0902);
        //pointUSA.setLongitude(-95.7129);
        //markerUSA.setGeometry(pointUSA);
        //ge.getFeatures().appendChild(markerUSA);
        //";

        //        // Execute the scripts
        //        webView.EvaluateJavaScriptAsync(scriptPakistan);
        //        webView.EvaluateJavaScriptAsync(scriptUSA);


        //webView.Navigated += WebViewNavigated;

        //// Start a timer to rotate Google Earth in real time.
        //var timer = new System.Timers.Timer(1000);
        //timer.Elapsed += (sender, e) =>
        //{
        //    // Rotate Google Earth by 1 degree.
        //    webView.EvaluateJavaScriptAsync("document.getElementById('earth').style.rotation += 1deg;");
        //};
        //timer.Start();
    }


    private void WebViewNavigated(object sender, WebNavigatedEventArgs e)
    {

        //string jsCode = @"
        //        function initialize() {
        //            var mapOptions = {
        //                center: {lat: 40.712776, lng: -74.005974}, // Specify the latitude and longitude of the marker
        //                zoom: 10 // Adjust the zoom level as needed
        //            };

        //            var map = new google.earth.Map(document.getElementById('map'), mapOptions);

        //            var placemark = map.createPlacemark('');

        //            var marker = map.createMarker('');
        //            marker.setPoint(map.createPoint(''));
        //            marker.getPoint().setLatitude(40.712776); // Set the latitude of the marker
        //            marker.getPoint().setLongitude(-74.005974); // Set the longitude of the marker

        //            placemark.setGeometry(marker);
        //            map.getFeatures().appendChild(placemark);
        //        }

        //        google.earth.addEventListener(google.earth, 'init', initialize, false);
        //    ";
        //webView.EvaluateJavaScriptAsync(jsCode);


        // jsCode = @"
        //        var header = document.getElementById('header');
        //        if (header) {
        //            header.style.display = 'none';
        //        }
        //    ";

        //jsCode += @"
        //        var footer = document.getElementById('footer');
        //        if (footer) {
        //            footer.style.display = 'none';
        //        }
        //    ";
        //webView.EvaluateJavaScriptAsync(jsCode);


        //string jsCode2 = "setInterval(function() { var earth = document.getElementById('globe'); if (earth) earth.style.transform = 'rotate(10deg)'; }, 10);";
        //webView.EvaluateJavaScriptAsync(jsCode2);
    }

    //int tileCount = 0;
    //List<Frame> tiles = new List<Frame>();
    //void OnHandlerChanged(object sender, EventArgs e)
    //{
    //    Frame f = (Frame)sender;
    //    tiles.Add(f);
    //    tileCount++;
    //}


    protected override void OnAppearing()
    {
        base.OnAppearing();
        //triger the refresh command
        if (BindingContext is WorldTimePageViewModel viewModel)
        {
            viewModel.GetRefreshCommand.Execute(null);
        }

        // Load or refresh the collection here
        // For example, you can rebind the collection to update the data
        // assuming your view model is set as the BindingContext of the page
        //if (BindingContext is WorldTimePageViewModel viewModel)
        //{
        //    viewModel.IsRefreshing=true;
        //}
    }
}