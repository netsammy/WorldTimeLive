using Plugin.MauiMTAdmob;
using System.Windows.Input;
using WorldTime.Utils;

namespace WorldTime;

//public partial class SettingsPage : ContentPage
//{
//	public SettingsPage()
//	{
//		InitializeComponent();
//	}
//}


public partial class SettingsPage : ContentPage
{
    private ObservableCollection<TimeZoneItem> _timeZones = new ObservableCollection<TimeZoneItem>();
    private ObservableCollection<TimeZoneItem> allTimeZones = new ObservableCollection<TimeZoneItem>();
    private bool _isLoading = true;

    public SettingsPage()
    {
        InitializeComponent();

        try
        {
            BindingContext = this;

            TimeZonesSelectionChangedCommand = new Command(OnTimeZonesSelectionChanged);
            SaveCommand = new Command(OnSave);
            //  SearchCommand = new Command(SearchAsync);

            IsSaveEnabled = true;
            //_ = GetTimeZonesAsync("");
            var backgroundImage = new Image { Source = "giphy234.gif", IsAnimationPlaying = true, Aspect = Aspect.Center };
            this.BackgroundImageSource = backgroundImage.Source;
        }
        catch { }

    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {          

            CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-1797269464593835/6178783276");
            CrossMauiMTAdmob.Current.ShowInterstitial();



            //if (allTimeZones.Count == 0)
               // await GetTimeZonesTestAsync("");


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await DisplayAlert("Error", ex.Message, "OK");
            IsLoading = false;
            _isLoading = false;
        }
    }


    private async Task GetTimeZonesTestAsync(string searchText)
    {
        _isLoading = true;
        IsLoading = true;
        await Task.Delay(1000); // Simulate loading for 1 second

        // await Task.Delay(1000);
        await GetTimeZonesAsync("");
        _isLoading = false;
        IsLoading = false;
    }

    public ObservableCollection<TimeZoneItem> TimeZones
    {
        get { return _timeZones; }
        set { _timeZones = value; OnPropertyChanged(nameof(TimeZones)); }
    }

    public ICommand TimeZonesSelectionChangedCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand SearchCommand { get; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged("SearchText");
            if (_searchText.Length > 2)
                _ = SearchAsync();
        }
    }
    private string _searchText;

    public bool IsSaveEnabled { get; set; }

    public bool IsLoading
    {
        get { return _isLoading; }
        set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
    }

    private async Task GetTimeZonesAsync(string searchText)
    {
        try
        {
            IsLoading = true;
            _isLoading = true;


            //await Task.Run(() =>
            //{

            if (allTimeZones.Count == 0)
            {

                var timeZones = TimeZoneInfo.GetSystemTimeZones().OrderBy(x => x.StandardName);

                //var json = JsonConvert.SerializeObject(timeZones);
                //File.WriteAllText(@"timezones.json", json);

                foreach (var timeZoneInfo in timeZones)
                {
                    var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo)
                                                    .ToString(TimeFormatter.TimeFormat);
                    var timeZoneItem = new TimeZoneItem
                    {
                        DisplayName = timeZoneInfo.DisplayName,
                        StandardName = timeZoneInfo.StandardName,
                        CurrentTime = currentTime,
                        Id = timeZoneInfo.Id
                    };

                    TimeZones.Add(timeZoneItem);
                }

                allTimeZones = TimeZones;
            }
            else
            {

                TimeZones = allTimeZones;
            }

            var selectedTimeZones = Preferences.Get("SelectedTimeZones", string.Empty)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var timeZone in TimeZones)
            {
                timeZone.IsSelected = selectedTimeZones.Contains(timeZone.Id);
            }

            if (searchText != string.Empty)
            {
                var filteredTimeZones = TimeZones.Where(c => c.DisplayName.ToLower().Contains(searchText.ToLower()) || c.StandardName.ToLower().Contains(searchText.ToLower()) || c.Id.ToLower().Contains(searchText.ToLower()));
                TimeZones = new ObservableCollection<TimeZoneItem>(filteredTimeZones);
            }

            //});

            IsLoading = false;
            _isLoading = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await DisplayAlert("Error", ex.Message, "OK");
            IsLoading = false;
            _isLoading = false;
        }
    }

    private void OnTimeZonesSelectionChanged()
    {
        IsSaveEnabled = TimeZones.Any(tz => tz.IsSelected);
        OnPropertyChanged(nameof(IsSaveEnabled));


    }

    private void OnSave()
    {
        //var previousTimeZones = Preferences.Get("SelectedTimeZones", string.Empty);
        //var selectedTimeZones = _timeZones.Where(tz => tz.IsSelected).Select(tz => tz.Id);
        //var allSelectedTimeZones = $"{previousTimeZones}|{string.Join("|", selectedTimeZones)}";
        //Preferences.Set("SelectedTimeZones", allSelectedTimeZones);

        var previousTimeZones = Preferences.Get("SelectedTimeZones", string.Empty);

        var savedselectedTimeZones =Preferences.Get("SelectedTimeZones", string.Empty)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
    .ToList();

        var selectedTimeZones = _timeZones.Where(tz => tz.IsSelected).Select(tz => tz.Id);
        foreach (var selectedTimeZone in selectedTimeZones)
        {
            if (!savedselectedTimeZones.Contains(selectedTimeZone))
            {
                savedselectedTimeZones.Add(selectedTimeZone);
            }
        }


         selectedTimeZones = _timeZones.Where(tz => tz.IsSelected==false).Select(tz => tz.Id);
        foreach (var selectedTimeZone in selectedTimeZones)
        {
            if (savedselectedTimeZones.Contains(selectedTimeZone))
            {
                savedselectedTimeZones.Remove(selectedTimeZone);
            }
        }


        var newlySelectedTimeZones = string.Join("|", savedselectedTimeZones);
        //var allSelectedTimeZones = string.IsNullOrEmpty(previousTimeZones) ? newlySelectedTimeZones : $"{previousTimeZones}|{newlySelectedTimeZones}";
        Preferences.Set("SelectedTimeZones", newlySelectedTimeZones);

        var message = "Time zones have been successfully saved.";
        DisplayAlert("Settings Saved", message, "OK");

        var target = Device.RuntimePlatform == Device.Android ? "//WorldTimePageAndroid" : "//WorldTimePage";
        Shell.Current.GoToAsync(target);
    }

    public async Task SearchAsync()
    {
        await GetTimeZonesAsync(SearchText);
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // function implementation code
    }

    private void Button_Pressed(object sender, EventArgs e)
    {
        var target = Device.RuntimePlatform == Device.Android ? "//WorldTimePageAndroid" : "//WorldTimePage";
        Shell.Current.GoToAsync(target);
    }



    private void btnShowAll_Pressed(object sender, EventArgs e)
    {
        IsLoading = true;
        GetTimeZonesAsync("");
        IsLoading = false;
    }



    private void btnSearch_Pressed(object sender, EventArgs e)
    {
        IsLoading = true;
        GetTimeZonesAsync(SearchText);
        IsLoading = false;
    }



    private void btnSave_Pressed(object sender, EventArgs e)
    {
        OnSave();

    }

    

    private void btnReset_Pressed(object sender, EventArgs e)
    {
        Preferences.Set("SelectedTimeZones", string.Empty);

        var message = "Time zones have been reset successfully.";
        DisplayAlert("Settings Saved", message, "OK");

        var target = Device.RuntimePlatform == Device.Android ? "//WorldTimePageAndroid" : "//WorldTimePage";
        Shell.Current.GoToAsync(target);
    }
}



//public partial class SettingsPage : ContentPage
//{

//    public ObservableCollection<TimeZoneItem> TimeZones { get; set; }=new ObservableCollection<TimeZoneItem>();

//    public ICommand TimeZonesSelectionChangedCommand { get; }
//    public ICommand SaveCommand { get; }
//    public bool IsSaveEnabled { get; set; }

//    public SettingsPage()
//    {
//        InitializeComponent();

//        GetTimeZonesAsync("");

//        TimeZonesSelectionChangedCommand = new Command(OnTimeZonesSelectionChanged);
//        SaveCommand = new Command(OnSave);

//        SearchCommand=new Command(async () => await SearchAsync());

//        BindingContext = this;

//        IsSaveEnabled = true;
//    }

//    //public async Task SettingsPage()
//    //{
//    //    InitializeComponent();

//    //    Use await to call GetTimeZonesAsync and wait for the result

//    //   await GetTimeZonesAsync();

//    //   TimeZonesSelectionChangedCommand = new Command(OnTimeZonesSelectionChanged);
//    //    SaveCommand = new Command(OnSave);

//    //    BindingContext = this;

//    //    IsSaveEnabled = true;
//    //}

//    public async Task GetTimeZonesAsync(string searchText)
//    {
//        try
//        {
//            // Use LINQ to get the system time zones asynchronously
//            var timeZones = await Task.Run(() => TimeZoneInfo.GetSystemTimeZones().OrderBy(x => x.StandardName));

//            // Use Parallel.ForEach to convert the time zones and add them to the list asynchronously
//            await Task.Run(() =>
//            {
//                Parallel.ForEach(timeZones, timeZoneInfo =>
//                {
//                    var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo)
//                    .ToString(TimeFormatter.TimeFormat);
//                    var timeZoneItem = new TimeZoneItem
//                    {
//                        DisplayName = timeZoneInfo.DisplayName,
//                        CurrentTime = currentTime,
//                        Id = timeZoneInfo.Id
//                    };
//                    TimeZones.Add(timeZoneItem);
//                });
//            });

//            // Retrieve the selected time zones from preferences
//            var selectedTimeZones = Preferences.Get("SelectedTimeZones", string.Empty)
//            .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
//            .ToList();

//            // Set the IsSelected property based on the retrieved time zones
//            foreach (var timeZone in TimeZones)
//            {
//                timeZone.IsSelected = selectedTimeZones.Contains(timeZone.Id);
//            }


//            if (searchText != string.Empty)
//            {

//                TimeZones = (ObservableCollection<TimeZoneItem>)TimeZones.Where(c => c.DisplayName.ToLower().Contains(searchText.ToLower()));
//            }

//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//            await DisplayAlert("Error", ex.Message, "OK");
//        }
//    }

//    private void OnTimeZonesSelectionChanged()
//    {
//        IsSaveEnabled = TimeZones.Any(tz => tz.IsSelected);
//        OnPropertyChanged(nameof(IsSaveEnabled));
//    }

//    //private void OnSave()
//    //{
//    //    var selectedTimeZones = TimeZones
//    //        .Where(tz => tz.IsSelected)
//    //        //.Select(tz => tz.TimeZoneInfo.Id)
//    //        .ToList();


//    //    string joinedTimeZones = string.Empty;
//    //    foreach (var item in selectedTimeZones)
//    //    {
//    //        joinedTimeZones += item.Id + "|";
//    //    }


//    //    Preferences.Set("SelectedTimeZones", joinedTimeZones);


//    //    //var json = JsonConvert.SerializeObject(selectedTimeZones);
//    //    //Preferences.Set("SelectedTimeZones", json);

//    //    // Show a confirmation or perform any other necessary actions

//    //    // Example: Display an alert
//    //    DisplayAlert("Settings Saved", "Selected time zones have been saved.", "OK");

//    //    if (Device.RuntimePlatform == Device.Android)
//    //    {
//    //         Shell.Current.GoToAsync("//WorldTimePageAndroid");
//    //    }
//    //    else
//    //    {
//    //        Shell.Current.GoToAsync("//WorldTimePage");
//    //    }
//    //}


//    private void OnSave()
//    {
//        var selectedTimeZones = TimeZones
//            .Where(tz => tz.IsSelected)
//            .ToList();

//        string joinedTimeZones = string.Join("|", selectedTimeZones.Select(tz => tz.Id));

//        Preferences.Set("SelectedTimeZones", joinedTimeZones);

//        DisplayAlert("Settings Saved", "Selected time zones have been saved.", "OK");

//        var target = Device.RuntimePlatform == Device.Android ? "//WorldTimePageAndroid" : "//WorldTimePage";
//        Shell.Current.GoToAsync(target);
//    }

//    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
//    {
//        IsSaveEnabled = TimeZones.Any(tz => tz.IsSelected);
//        OnPropertyChanged(nameof(IsSaveEnabled));
//    }

//    private void btnClear_Clicked(object sender, EventArgs e)
//    {
//        //copilot uncheck all checkboxes in the collectionView 






//#pragma warning disable CS0219 // Variable is assigned but its value is never used
//        int i = 0;
//#pragma warning restore CS0219 // Variable is assigned but its value is never used
//        //foreach (TimeZoneItem item in colTimeZones.ItemsSource)
//        //{
//        //    //colTimeZones.FindByName("",)
//        //    if (item is TimeZoneItem timeZoneItem)
//        //    {
//        //        item.IsSelected = false;
//        //        // Access the checkbox in the row and set IsChecked to false
//        //        //var checkbox = colTimeZones.ItemsSource.get [i++].FindByName<CheckBox>("chkChecked");
//        //        //checkbox.IsChecked = false;
//        //    }
//        //}
//        // colTimeZones.ItemTemplate.Values.Clear();
//        var chkChecked = colTimeZones.FindByName("chkChecked");

//        //item.


//    }


//    private string _searchText;
//    public string SearchText
//    {
//        get => _searchText;
//        set
//        {
//            _searchText = value;
//            OnPropertyChanged("SearchText");
//        }
//    }

//    public Command SearchCommand { private set; get; }


//    public async Task SearchAsync()
//    {
//        // add search logic here
//        TimeZones = (ObservableCollection<TimeZoneItem>)TimeZones.Where(c => c.DisplayName.ToLower().Contains(SearchText.ToLower()));
//    }
//}
