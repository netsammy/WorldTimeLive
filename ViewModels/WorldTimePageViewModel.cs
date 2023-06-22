using WorldTime.Utils;


namespace WorldTime.ViewModels
{
    public partial class WorldTimePageViewModel : ObservableObject
    {
        public WorldTimePageViewModel()
        {
            GetWorldTimeAsync();
        }

        public ObservableCollection<TimeZoneItem> TimeZones { get; } = new();
        //public ObservableCollection<TimeZoneItem> TimeZones;

        [ObservableProperty]
        string currentTime = "";

        [ObservableProperty]
        string displayName = "";

        [ObservableProperty]
        bool isRefreshing;


        [ObservableProperty]
        string searchTerm = "";

        [RelayCommand]
        public async Task GetWorldTimeAsync()
        {


            //Task.Run(() =>
            //{

            //filter the TimeZoneInfo.GetSystemTimeZones() with the following time zones
            //UTC (Coordinated Universal Time)
            //        GMT - 5(Eastern Standard Time, EST)
            //GMT - 4(Atlantic Standard Time, AST)
            //GMT + 1(Central European Time, CET)
            //GMT + 2(Eastern European Time, EET)
            //GMT + 5:30(Indian Standard Time, IST)
            //GMT + 8(China Standard Time, CST)
            //GMT + 9(Japan Standard Time, JST)
            //GMT + 12(New Zealand Standard Time, NZST)
            //GMT - 8(Pacific Standard Time, PST)
            //GMT + 3(Moscow Standard Time, MSK)
            //GMT + 10(Australian Eastern Standard Time, AEST)
            //GMT + 4(Gulf Standard Time, GST)
            //GMT + 6(Bangladesh Standard Time, BST)
            //GMT + 7(Indochina Time, ICT)
            //GMT + 3:30(Iran Standard Time, IRST)
            //GMT + 0(Greenwich Mean Time, GMT)
            //GMT + 5(Pakistan Standard Time, PKT)
            //GMT - 3(Brasília Standard Time, BRT)
            //GMT - 7(Mountain Standard Time, MST)

            var timeZonesDefault = new List<string>
            {
                "UTC",
                "Eastern Standard Time",
                "Atlantic Standard Time",
                "Central European Time",
                "Eastern European Time",
                "Indian Standard Time",
                "China Standard Time",
                "Japan Standard Time",
                "New Zealand Standard Time",
                "Pacific Standard Time",
                "Moscow Standard Time",
                "Australian Eastern Standard Time",
                "Gulf Standard Time",
                "Bangladesh Standard Time",
                "Indochina Time",
                "Iran Standard Time",
                "Greenwich Mean Time",
                "Pakistan Standard Time",
                "Brasília Standard Time",
                "Mountain Standard Time"
            };


            // Retrieve the selected time zones from preferences
            var selectedTimeZones = Preferences.Get("SelectedTimeZones", string.Empty)
                .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();




            var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones()
            //var topTimeZones = timeZoneInfos;
            .Where(tz => selectedTimeZones.Contains(tz.Id))
            .ToList();


            if (Preferences.Get("SelectedTimeZones", string.Empty) == string.Empty)
            {
                timeZoneInfos = TimeZoneInfo.GetSystemTimeZones()
            .Where(tz => timeZonesDefault.Contains(tz.Id))
            .ToList();
            }


            foreach (var timeZoneInfo in timeZoneInfos)
            {
                //if (selectedTimeZones.Contains(timeZoneInfo.Id))
                //{

                var currentTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZoneInfo)
                                                    .ToString(TimeFormatter.TimeFormat);
                var timeZoneItem = new TimeZoneItem
                {
                    DisplayName = timeZoneInfo.DisplayName,
                    CurrentTime = currentTime,
                    Id = timeZoneInfo.Id
                };
                TimeZones.Add(timeZoneItem);
                //}
            }

        }





        [RelayCommand]
        public async Task GetRefreshAsync()
        {
            TimeZones.Clear();
            await GetWorldTimeAsync();
        }


        [RelayCommand]
        public async Task NavigateToSettingsAsync()
        {
            //await Shell.Current.GoToAsync(nameof(SettingsPage));
            try
            {
                await Shell.Current.GoToAsync("///SettingsPage");
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
        }


        [RelayCommand]
        async Task GoToDetails(TimeZoneItem timeZoneItem)
        {
            if (timeZoneItem == null)
                return;

            //var timeZoneItemParams = new Dictionary<string, object>
            //{
            //    { "TimeZoneItem", timeZoneItem }              
            //    // Add more parameters as needed
            //};

            //await Shell.Current.GoToAsync($"details?QueryParameters={Uri.EscapeDataString(JsonConvert.SerializeObject(timeZoneItemParams))}");

            // await Shell.Current.GoToAsync(nameof(DetailsPage));

            //    await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            //{
            //    {"TimeZoneItem", timeZoneItem }
            //});
        }



    }
}
