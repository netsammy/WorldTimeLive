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
        public event EventHandler TimeChanged;

        public DateTime Time { get; set; }
    }
}