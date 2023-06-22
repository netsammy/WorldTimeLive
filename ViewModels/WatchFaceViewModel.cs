using System.Timers;
using Timer = System.Timers.Timer;

namespace WorldTime.ViewModels;



public class WatchFaceViewModel : INotifyPropertyChanged
{
    private DateTime currentTime;
    private Timer timer;
    public DateTime CurrentTime
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

    public WatchFaceViewModel()
    {
        // Set the initial current time
        // CurrentTime = DateTime.Now;

        timer = new Timer(1000);
        timer.Elapsed += TimerElapsed;
        timer.Start();
    }


    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        //Device.BeginInvokeOnMainThread(() =>
        //{

        CurrentTime = CurrentTime.AddSeconds(1);

        //});


    }
    //Helper method for updating the current time
    public void UpdateCurrentTime()
    {
        //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        //                    {
        //                        CurrentTime = Convert.ToDateTime(CurrentTime).AddSeconds(1);
        //                        return true;
        //                    });
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

