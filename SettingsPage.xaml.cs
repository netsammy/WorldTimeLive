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
    public ObservableCollection<TimeZoneItem> TimeZones { get; } = new();

    public ICommand TimeZonesSelectionChangedCommand { get; }
    public ICommand SaveCommand { get; }
    public bool IsSaveEnabled { get; set; }

    public SettingsPage()
    {
        InitializeComponent();

        try
        {



            var timeZones = TimeZoneInfo.GetSystemTimeZones().OrderBy(x => x.StandardName);
            // .Select(tz => new TimeZoneSelectionViewModel(tz)));






            foreach (var timeZoneInfo in timeZones)
            {
                var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo)
                                                .ToString(TimeFormatter.TimeFormat);
                var timeZoneItem = new TimeZoneItem
                {
                    DisplayName = timeZoneInfo.DisplayName,
                    CurrentTime = currentTime,
                    Id = timeZoneInfo.Id
                };
                TimeZones.Add(timeZoneItem);
            }


            // Retrieve the selected time zones from preferences
            var selectedTimeZones = Preferences.Get("SelectedTimeZones", string.Empty)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            // Set the IsSelected property based on the retrieved time zones
            foreach (var timeZone in TimeZones)
            {
                timeZone.IsSelected = selectedTimeZones.Contains(timeZone.Id);
            }



            //// Retrieve the selected time zones from preferences
            //var selectedTimeZonesJson = Preferences.Get("SelectedTimeZones", string.Empty);
            //var selectedTimeZones = JsonConvert.DeserializeObject<List<string>>(selectedTimeZonesJson);

            //// Set the IsSelected property based on the retrieved time zones
            //foreach (var timeZone in TimeZones)
            //{
            //    timeZone.IsSelected = selectedTimeZones?.Contains(timeZone.Id) ?? false;
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            DisplayAlert("Error", ex.Message, "OK");
        }

        TimeZonesSelectionChangedCommand = new Command(OnTimeZonesSelectionChanged);
        SaveCommand = new Command(OnSave);

        BindingContext = this;

        IsSaveEnabled = true;
    }

    private void OnTimeZonesSelectionChanged()
    {
        IsSaveEnabled = TimeZones.Any(tz => tz.IsSelected);
        OnPropertyChanged(nameof(IsSaveEnabled));
    }

    private void OnSave()
    {
        var selectedTimeZones = TimeZones
            .Where(tz => tz.IsSelected)
            //.Select(tz => tz.TimeZoneInfo.Id)
            .ToList();


        string joinedTimeZones = string.Empty;
        foreach (var item in selectedTimeZones)
        {
            joinedTimeZones += item.Id + "|";
        }


        Preferences.Set("SelectedTimeZones", joinedTimeZones);


        //var json = JsonConvert.SerializeObject(selectedTimeZones);
        //Preferences.Set("SelectedTimeZones", json);

        // Show a confirmation or perform any other necessary actions

        // Example: Display an alert
        DisplayAlert("Settings Saved", "Selected time zones have been saved.", "OK");

        Shell.Current.GoToAsync("///WorldTimePage");
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        IsSaveEnabled = TimeZones.Any(tz => tz.IsSelected);
        OnPropertyChanged(nameof(IsSaveEnabled));
    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {
        //copilot uncheck all checkboxes in the collectionView 






        int i = 0;
        //foreach (TimeZoneItem item in colTimeZones.ItemsSource)
        //{
        //    //colTimeZones.FindByName("",)
        //    if (item is TimeZoneItem timeZoneItem)
        //    {
        //        item.IsSelected = false;
        //        // Access the checkbox in the row and set IsChecked to false
        //        //var checkbox = colTimeZones.ItemsSource.get [i++].FindByName<CheckBox>("chkChecked");
        //        //checkbox.IsChecked = false;
        //    }
        //}
        // colTimeZones.ItemTemplate.Values.Clear();
        var chkChecked = colTimeZones.FindByName("chkChecked");

        //item.


    }
}
//public partial class SettingsPage : ContentPage, INotifyPropertyChanged
//{
//    private ObservableCollection<TimeZoneItem> timeZones;
//    public ObservableCollection<TimeZoneItem> TimeZones
//    {
//        get { return timeZones; }
//        set { timeZones = value; OnPropertyChanged(); }
//    }

//    private List<TimeZoneItem> selectedTimeZones;
//    public List<TimeZoneItem> SelectedTimeZones
//    {
//        get { return selectedTimeZones; }
//        set { selectedTimeZones = value; OnPropertyChanged(); }
//    }

//    public SettingsPage()
//    {
//        InitializeComponent();
//        BindingContext = this;

//        LoadTimeZones();
//    }

//    private void LoadTimeZones()
//    {
//        var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
//        TimeZones = new ObservableCollection<TimeZoneItem>();

//        foreach (var timeZoneInfo in timeZoneInfos)
//        {
//            var timeZoneItem = new TimeZoneItem
//            {
//                DisplayName = timeZoneInfo.DisplayName,
//                TimeZoneId = timeZoneInfo.Id
//            };
//            TimeZones.Add(timeZoneItem);
//        }

//        // Set the selected time zones initially
//        SelectedTimeZones = TimeZones.Where(tz => tz.IsSelected).ToList();
//    }


//    private void SaveButton_Clicked(object sender, EventArgs e)
//    {
//        // Save the selected time zones to Preferences
//        Preferences.Set("SelectedTimeZones", SelectedTimeZones.Select(tz => tz.TimeZoneId).ToList());

//        // Display a confirmation message
//        DisplayAlert("Saved", "Selected time zones saved successfully.", "OK");
//    }


//    //private void LoadTimeZones()
//    //{
//    //    var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
//    //    TimeZones = new ObservableCollection<TimeZoneItem>();

//    //    foreach (var timeZoneInfo in timeZoneInfos)
//    //    {
//    //        var timeZoneItem = new TimeZoneItem
//    //        {
//    //            DisplayName = timeZoneInfo.DisplayName,
//    //            TimeZoneId = timeZoneInfo.Id
//    //        };
//    //        TimeZones.Add(timeZoneItem);
//    //    }

//    //    // Get the selected time zone IDs from Preferences
//    //    var selectedTimeZoneIds = Preferences.Get("SelectedTimeZones", new List<string>());

//    //    // Set the IsSelected property based on the stored selected time zones
//    //    foreach (var timeZoneItem in TimeZones)
//    //    {
//    //        timeZoneItem.IsSelected = selectedTimeZoneIds.Contains(timeZoneItem.TimeZoneId);
//    //    }

//    //    // Set the selected time zones initially
//    //    SelectedTimeZones = TimeZones.Where(tz => tz.IsSelected).ToList();
//    //}


//}