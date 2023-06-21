using InternetWorldTimeApp;
using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Input;


namespace WorldTime.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private double downloadSpeed;
        private double uploadSpeed;
        private bool isTesting;

        public double DownloadSpeed
        {
            get { return downloadSpeed; }
            set
            {
                downloadSpeed = value;
                OnPropertyChanged(nameof(DownloadSpeed));
            }
        }

        public double UploadSpeed
        {
            get { return uploadSpeed; }
            set
            {
                uploadSpeed = value;
                OnPropertyChanged(nameof(UploadSpeed));
            }
        }

        public bool IsTesting
        {
            get { return isTesting; }
            set
            {
                isTesting = value;
                OnPropertyChanged(nameof(IsTesting));
                OnPropertyChanged(nameof(IsNotTesting));
            }
        }

        public bool IsNotTesting => !IsTesting;

        public ICommand TestSpeedCommand { get; private set; }

        public MainPageViewModel()
        {
            TestSpeedCommand = new Command(async () => await TestSpeedAsync());
        }

        private async Task TestSpeedAsync()
        {
            if (!Connectivity.NetworkAccess.HasFlag(NetworkAccess.Internet))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No internet connection available.", "OK");
                return;
            }

            IsTesting = true;

            // Perform download speed test
            var downloadTask = new TaskCompletionSource<double>();
            var downloadSpeedWatcher = new InternetSpeedWatcher();
            downloadSpeedWatcher.SpeedChanged += (s, speed) => downloadTask.SetResult(speed);
            downloadSpeedWatcher.Start();

            await Task.Delay(5000); // Adjust the duration for the speed test

            downloadSpeedWatcher.Stop();
            DownloadSpeed = await downloadTask.Task;

            // Perform upload speed test
            var uploadTask = new TaskCompletionSource<double>();
            var uploadSpeedWatcher = new InternetSpeedWatcher();
            uploadSpeedWatcher.SpeedChanged += (s, speed) => uploadTask.SetResult(speed);
            uploadSpeedWatcher.Start();

            await Task.Delay(5000); // Adjust the duration for the speed test

            uploadSpeedWatcher.Stop();
            UploadSpeed = await uploadTask.Task;

            IsTesting = false;
        }

        private long PerformPingTest()
        {
            try
            {
                using (var pingSender = new Ping())
                {
                    var reply = pingSender.Send("www.google.com");
                    if (reply.Status == IPStatus.Success)
                        return reply.RoundtripTime;
                }
            }
            catch
            {
                // Handle exception or log error if necessary
            }

            return -1;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class InternetSpeedWatcher
    {
        private const int BufferSize = 8192;
        private byte[] buffer;
        private bool isRunning;
        private long totalBytes;
        private DateTime startTime;

        public event EventHandler<double> SpeedChanged;

        public void Start()
        {
            buffer = new byte[BufferSize];
            isRunning = true;
            totalBytes = 0;
            startTime = DateTime.Now;

            Task.Run(() => WatchSpeed());
        }

        public void Stop()
        {
            isRunning = false;
        }

        private void WatchSpeed()
        {
            while (isRunning)
            {
                try
                {
                    var bytesReceived = WorldTimeUtils.DownloadBytes(buffer, BufferSize);
                    totalBytes +=  bytesReceived.Result;

                    var elapsedTime = (DateTime.Now - startTime).TotalSeconds;
                    var speed = totalBytes / elapsedTime;

                    SpeedChanged?.Invoke(this, speed);
                }
                catch
                {
                    // Handle exception or log error if necessary
                }
            }
        }
    }

}
