



using WorldTime.ViewModels;
using System.Timers;
using Timer = System.Timers.Timer;
using WorldTime.Utils;

namespace WorldTime;


public partial class WatchFace : ContentView
{
#pragma warning disable CS0169 // The field 'WatchFace.timer' is never used
    private Timer timer;
#pragma warning restore CS0169 // The field 'WatchFace.timer' is never used
    public static readonly BindableProperty TimeNowProperty =
            BindableProperty.Create(nameof(TimeNow), typeof(string), typeof(WatchFace), default(string), BindingMode.TwoWay, propertyChanged: (bindable, oldvalue, newvalue) =>
                {
                    var watchFace = (WatchFace)bindable;
                    watchFace.TimeLabel.Text = Convert.ToDateTime(newvalue).ToString(TimeFormatter.TimeFormat);
                    watchFace.TimeNow = Convert.ToDateTime(newvalue).ToString(TimeFormatter.TimeFormat);
                    // this.TimeNow= Convert.ToDateTime(newvalue).ToString("HH:mm:ss");
                    //update WatchFaceViewModel CurrentTime

                   
                    //watchFace.BindingContext = new WatchFaceViewModel {
                    //    CurrentTime = Convert.ToDateTime(newvalue)
                    //};


                });


    public string TimeNow
    {
        get => (string)GetValue(TimeNowProperty);
        set
        {
            SetValue(TimeNowProperty, value);
            
        }
    }

    public WatchFace()
    {
        InitializeComponent();


#pragma warning disable CS0612 // Type or member is obsolete
#pragma warning disable CS0618 // Type or member is obsolete
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                            {
                                TimeLabel.Text = Convert.ToDateTime(TimeLabel.Text).AddSeconds(1).ToString(TimeFormatter.TimeFormat);
                                return true;
                            });
#pragma warning restore CS0618 // Type or member is obsolete
#pragma warning restore CS0612 // Type or member is obsolete


        //this.BindingContext = new WatchFaceViewModel
        //{
        //    CurrentTime = Convert.ToDateTime(TimeNow)
        //};
        //timer = new Timer(1000);
        //timer.Elapsed += TimerElapsed;
        //timer.Start();
    }

    //private static void OnTimeNowChanged(BindableObject bindable, object oldValue, object newValue)
    //{
    //    if (bindable is WatchFace watchFace)
    //    {
    //        watchFace.TimeLabel.Text = Convert.ToDateTime(newValue).ToString("HH:mm:ss");
    //    }
    //}

                            //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
                            //{
                            //    base.OnPropertyChanged(propertyName);

                            //    if (propertyName == nameof(TimeNow))
                            //    {
                            //        //Device.BeginInvokeOnMainThread(() =>
                            //        //{
                            //            TimeLabel.Text = Convert.ToDateTime(TimeNow).ToString("HH:mm:ss");
                            //        //});
                            //    }
                            //}

                            //private  void TimerElapsed(object sender, ElapsedEventArgs e)
                            //{
                            //    DateTime currentTime = DateTime.Parse(TimeNow);
                            //    currentTime = currentTime.AddSeconds(1);
                            //    TimeNow = currentTime.ToString("HH:mm:ss");


                            //    this.TimeLabel.Text = TimeNow;


                            //}
}

//public partial class WatchFace : ContentView
//{

//    private Timer timer;




//    public static readonly BindableProperty TimeNowProperty =
//            BindableProperty.Create(nameof(TimeNow), typeof(string), typeof(WatchFace), default(string), BindingMode.Default, propertyChanged:( bindable,oldvalue,newvalue)=>
//                {
//                var watchFace = (WatchFace)bindable;
//                    watchFace.TimeLabel.Text = Convert.ToDateTime(newvalue).ToString("HH:mm:ss");

//                    //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//                    //{
//                    //    watchFace.TimeLabel.Text = Convert.ToDateTime(watchFace.TimeLabel.Text).AddSeconds(1).ToString("HH:mm:ss");
//                    //    return true;
//                    //});
//                });



//    public string TimeNow
//    {
//        get => (string)GetValue(TimeNowProperty);
//        set => SetValue(TimeNowProperty, value);


//    }

//    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        base.OnPropertyChanged(propertyName);

//        if (propertyName == nameof(TimeNow))
//        {
//            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//            {
//                TimeNow = Convert.ToDateTime(TimeNow).AddSeconds(1).ToString("HH:mm:ss");
//                TimeLabel.Text = TimeNow;
//                return true;
//            });
//        }
//    }

//    //private void TimerElapsed(object sender, ElapsedEventArgs e)
//    //{
//    //    DateTime currentTime = DateTime.Parse(TimeNow);
//    //    currentTime = currentTime.AddSeconds(1);
//    //    TimeNow = currentTime.ToString("HH:mm:ss");
//    //}

//public WatchFace()
//    {
//        InitializeComponent();


//        ////increment TimeNow by 1 second
//        //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//        //{
//        //    TimeNow = Convert.ToDateTime(TimeNow).AddSeconds(1).ToString("HH:mm:ss");
//        //    TimeLabel.Text = Convert.ToDateTime(TimeLabel.Text).AddSeconds(1).ToString("HH:mm:ss");
//        //    return true;
//        //});


//        //timer = new Timer(1000);
//        //timer.Elapsed += TimerElapsed;
//        //timer.Start();
//        //TimeSpan timeSpan = DateTime.Now.TimeOfDay;
//        //var diff = timeSpan - DateTime.Now.Subtract(Convert.ToDateTime(TimeNow));
//        // TimeNow = DateTime.Now.Add(diff).ToString("HH:mm:ss");
//        // TimeLabel.Text = Convert.ToDateTime(TimeNow).ToString("HH:mm:ss");
//        //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//        //{
//        //    TimeLabel.Text = Convert.ToDateTime(TimeLabel.Text).AddSeconds(1).ToString("HH:mm:ss");
//        //    return true;
//        //});
//    }

//    //private void TimerElapsed(object sender, ElapsedEventArgs e)
//    //{
//    //    //Device.BeginInvokeOnMainThread(() =>
//    //    //{
//    //    //    DateTime currentTime = DateTime.Parse(TimeNow);
//    //    //    currentTime = currentTime.AddSeconds(1);
//    //    //    TimeNow = currentTime.ToString("HH:mm:ss");
//    //    //});

//    //    //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
//    //    //{
//    //    //    TimeLabel.Text = Convert.ToDateTime(TimeLabel.Text).AddSeconds(1).ToString("HH:mm:ss");
//    //    //    return true;
//    //    //});
//    //}

//}