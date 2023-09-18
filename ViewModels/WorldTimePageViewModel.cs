
using System.Windows.Input;
using WorldTime.Utils;


namespace WorldTime.ViewModels
{

    public partial class WorldTimePageViewModel : INotifyPropertyChanged

    {

        public ICommand GetWorldTimeCommand { get; private set; }
        public ICommand GetRefreshCommand { get; private set; }
        public ICommand NavigateToSettingsCommand { get; private set; }
        public WorldTimePageViewModel()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            GetWorldTimeAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed


            GetWorldTimeCommand = new Command(async () => await GetWorldTimeAsync());
            GetRefreshCommand = new Command(async () => await GetRefreshAsync());
            NavigateToSettingsCommand = new Command(async () => await NavigateToSettingsAsync());


            SearchCommand = new Command(async () => await SearchAsync());
        }




        private string currentTime = "";
        public string CurrentTime
        {
            get => currentTime;
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string displayName = "";
        public string DisplayName
        {
            get => displayName;
            set
            {
                if (displayName != value)
                {
                    displayName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string searchTerm = "";
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                if (searchTerm != value)
                {
                    searchTerm = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<TimeZoneItem> timeZones = new ObservableCollection<TimeZoneItem>();

        public ObservableCollection<TimeZoneItem> TimeZones
        {
            get => timeZones;
            set
            {
                if (timeZones != value)
                {
                    timeZones = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //[RelayCommand]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task GetWorldTimeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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

            //var timeZonesDefault = new List<string>
            //{
            //    "UTC",
            //    "Eastern Standard Time",
            //    "Atlantic Standard Time",
            //    "Central European Time",
            //    "Eastern European Time",
            //    "Indian Standard Time",
            //    "China Standard Time",
            //    "Japan Standard Time",
            //    "New Zealand Standard Time",
            //    "Pacific Standard Time",
            //    "Moscow Standard Time",
            //    "Australian Eastern Standard Time",
            //    "Gulf Standard Time",
            //    "Bangladesh Standard Time",
            //    "Indochina Time",
            //    "Iran Standard Time",
            //    "Greenwich Mean Time",
            //    "Pakistan Standard Time",
            //    "Brasília Standard Time",
            //    "Mountain Standard Time"
            //};


            // Retrieve the selected time zones from preferences

            string defaultTimeZones = string.Empty;
#if ANDROID

            defaultTimeZones = "Asia/Karachi|Asia/Kolkata|America/Eirunepe";
#elif WINDOWS
            defaultTimeZones = string.Empty;
#endif


            var selectedTimeZones = Preferences.Get("SelectedTimeZones", defaultTimeZones)
        .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
        .ToList();

            var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones()
            //var topTimeZones = timeZoneInfos;
            .Where(tz => selectedTimeZones.Contains(tz.Id))
            .ToList();


            //if (Preferences.Get("SelectedTimeZones", string.Empty) == string.Empty)
            //{
            //    timeZoneInfos = TimeZoneInfo.GetSystemTimeZones()
            //.Where(tz => timeZonesDefault.Contains(tz.Id))
            //.ToList();
            //}


            foreach (var timeZoneInfo in timeZoneInfos)
            {
                //if (selectedTimeZones.Contains(timeZoneInfo.Id))
                //{

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
                //}
            }

        }





        //[RelayCommand]
        public async Task GetRefreshAsync()
        {
            TimeZones.Clear();
            await GetWorldTimeAsync();
        }


        //[RelayCommand]
        public async Task NavigateToSettingsAsync()
        {
            //await Shell.Current.GoToAsync(nameof(SettingsPage));
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                await Shell.Current.GoToAsync("///SettingsPage");
            }
            catch (Exception ex)
            {
                // Handle or log the exception
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }





        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public Command SearchCommand { private set; get; }


        public async Task SearchAsync()
        {
            // add search logic here
            TimeZones = (ObservableCollection<TimeZoneItem>)TimeZones.Where(c => c.DisplayName.ToLower().Contains(SearchText.ToLower()));
        }
    }
}
