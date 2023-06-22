using Microsoft.Maui.Controls.Compatibility.Platform.iOS;
using Microsoft.Maui.Controls.Shapes;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace SpeedTest;

public partial class BardWatchFace : ContentView
{
//	public BardWatchFace()
//	{
//		InitializeComponent();
//	}
//}



        public BardWatchFace()
        {
            InitializeComponent();
        }

        // Update the watch face whenever the time changes.
        async void OnTimeChanged(object sender, System.EventArgs e)
        {
            // Get the current time.
            DateTime dateTime = DateTime.Now;

            // Update the hour hand.
            var hourHand = (Path)Children[0];
            hourHand.Data = new PathData
            {
                Figures = new[]
                {
                    new Figure
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(100, 0),
                        SegmentCount = 4
                    }
                }
            };
            hourHand.Rotation = dateTime.Hour * 360 / 12;

            // Update the minute hand.
            var minuteHand = (Path)Children[1];
            minuteHand.Data = new PathData
            {
                Figures = new[]
                {
                    new Figure
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(100, 0),
                        SegmentCount = 4
                    }
                }
            };
            minuteHand.Rotation = dateTime.Minute * 360 / 60;

            // Update the second hand.
            var secondHand = (Path)Children[2];
            secondHand.Data = new PathData
            {
                Figures = new[]
                {
                    new Figure
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(100, 0),
                        SegmentCount = 4
                    }
                }
            };
            secondHand.Rotation = dateTime.Second * 360 / 60;
        }
    }

