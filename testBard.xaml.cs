namespace WorldTime;
using System.Timers;
public partial class testBard : ContentPage
{
    public testBard()
    {
        InitializeComponent();


        // Create a new view model.
        ViewModel viewModel = new ViewModel();

        //// Subscribe to the view model's TimeChanged event.
        //viewModel.TimeChanged += (o, args) =>
        //    {
        //        // Update the view with the new time.
        //        TimeLabel.Text = args.Time;
        //    };

        // Start the timer.
        Timer timer = new Timer(1000);
        //timer.Interval = TimeSpan.FromSeconds(1);
        timer.Elapsed += (o, args) =>
        {
            // Update the view model's time.
            viewModel.Time = DateTime.Now;
        };
        timer.Start();

        this.BindingContext = viewModel;
    }


    public class ViewModel
    {
#pragma warning disable CS0067 // The event 'testBard.ViewModel.TimeChanged' is never used
        public event EventHandler TimeChanged;
#pragma warning restore CS0067 // The event 'testBard.ViewModel.TimeChanged' is never used

        public DateTime Time { get; set; }
    }
}