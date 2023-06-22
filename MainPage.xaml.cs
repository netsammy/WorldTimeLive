using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WorldTime.Utils;

namespace WorldTime
{





    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<TimeZoneItem> timeZones;
        public ObservableCollection<TimeZoneItem> TimeZones
        {
            get { return timeZones; }
            set { timeZones = value; OnPropertyChanged(); }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(() =>
            {
                var timeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
                var topTimeZones = timeZoneInfos.OrderByDescending(tz => tz.BaseUtcOffset.TotalHours)
                                                ;//.Take(10);

                TimeZones = new ObservableCollection<TimeZoneItem>();

                foreach (var timeZoneInfo in topTimeZones)
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
            });
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

   
}


//    public partial class MainPage : ContentPage
//    {
//        private bool isUpdatingTime;

//        public MainPage()
//        {
//            InitializeComponent();
//        }

//        protected override void OnAppearing()
//        {
//            base.OnAppearing();

//            StartUpdatingTime();
//        }

//        protected override void OnDisappearing()
//        {
//            base.OnDisappearing();

//            StopUpdatingTime();
//        }

//        private void StartUpdatingTime()
//        {
//            isUpdatingTime = true;

//            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
//            {
//                Device.BeginInvokeOnMainThread(() =>
//                {
//                    timeLabel.Text = DateTime.Now.ToString("HH:mm:ss.fff");
//                });

//                return isUpdatingTime;
//            });
//        }

//        private void StopUpdatingTime()
//        {
//            isUpdatingTime = false;
//        }
//    }
//}


//public partial class MainPage : ContentPage
//{
//    private bool isUpdatingTime;

//    public MainPage()
//    {
//        InitializeComponent();
//    }

//    protected override void OnAppearing()
//    {
//        base.OnAppearing();

//        StartUpdatingTime();
//    }

//    protected override void OnDisappearing()
//    {
//        base.OnDisappearing();

//        StopUpdatingTime();
//    }

//    private async void StartUpdatingTime()
//    {
//        isUpdatingTime = true;

//        while (isUpdatingTime)
//        {
//            Device.BeginInvokeOnMainThread(() =>
//            {
//                timeLabel.Text = DateTime.Now.ToString("HH:mm:ss");
//            });

//            await Task.Delay(1000); // Delay for 1 second
//        }
//    }

//    private void StopUpdatingTime()
//    {
//        isUpdatingTime = false;
//    }
//}


//private async void Button_Clicked(object sender, EventArgs e)
//{
//    //await PerformWorldTime();
//    await  PerformWorldTime();
//}

//private async Task PerformWorldTime()
//{
//    // Disable the button during the speed test
//    startButton.IsEnabled = false;

//    try
//    {
//        // Perform download speed test
//        var downloadSpeed = await WorldTimeService.TestDownloadSpeed();
//        downloadSpeedLabel.Text = $"Download Speed: {downloadSpeed:F2} Mbps";

//        // Perform upload speed test
//        //var uploadSpeed = await WorldTimeService.TestUploadSpeed();
//        //uploadSpeedLabel.Text = $"Upload Speed: {uploadSpeed:F2} Mbps";

//        //// Perform ping test
//        //var ping = await WorldTimeService.TestPingLatency();
//        //pingLabel.Text = $"Ping: {ping} ms";
//    }
//    catch (Exception ex)
//    {
//        // Handle exceptions if necessary
//        //Console.WriteLine($"Error: {ex.Message}");
//    }
//    finally
//    {
//        // Enable the button after the speed test
//        startButton.IsEnabled = true;
//    }
//}
//    }
//}
