namespace WorldTime.Views;

using Microsoft.Maui.Controls;

using Microsoft.Maui.Graphics;
using System;


public partial class AnalogClock : ContentView
    {
    //private BoxView hourHand;
    //private BoxView minuteHand;
    //private BoxView secondHand;

 


    public AnalogClock()
    {
        hourHand = new BoxView { Color = Colors.Black };
        minuteHand = new BoxView { Color = Colors.Black };
        secondHand = new BoxView { Color = Colors.Red };

        Content = new Grid
        {
            Children =
                {
                    hourHand,
                    minuteHand,
                    secondHand
                }
        };

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            UpdateClock();
            return true;
        });
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        double centerX = width / 2;
        double centerY = height / 2;

        double handLength = Math.Min(width, height) / 2 * 0.8;

        LayoutChild(hourHand, centerX, centerY, handLength * 0.5, 6);
        LayoutChild(minuteHand, centerX, centerY, handLength * 0.7, 4);
        LayoutChild(secondHand, centerX, centerY, handLength * 0.8, 2);
    }

    private void LayoutChild(View child, double centerX, double centerY, double length, double thickness)
    {
        double childWidth = child.Measure(double.PositiveInfinity, double.PositiveInfinity).Request.Width;
        double childHeight = child.Measure(double.PositiveInfinity, double.PositiveInfinity).Request.Height;

        double childX = centerX - childWidth / 2;
        double childY = centerY - childHeight / 2;

        child.WidthRequest = childWidth;
        child.HeightRequest = childHeight;

        child.AnchorX = 0.5;
        child.AnchorY = 1;

        child.Rotation = 0;
        child.RotationY = 0;

        child.TranslationX = centerX - childX;
        child.TranslationY = centerY - childY;

        child.Scale = 1;

        Rect rect = new Rect(childX, childY, childWidth, childHeight);

        child.Layout(rect);
        //child.Layout( new Rectangle(Convert.ToInt32(childX), Convert.ToInt32(childY), Convert.ToInt32(childWidth), Convert.ToInt32(childHeight)));
    }

    private void UpdateClock()
    {
        DateTime currentTime = DateTime.Now;

        double hourAngle = (currentTime.Hour % 12 + currentTime.Minute / 60.0) * 30;
        double minuteAngle = (currentTime.Minute + currentTime.Second / 60.0) * 6;
        double secondAngle = currentTime.Second * 6;

        hourHand.Rotation = hourAngle;
        minuteHand.Rotation = minuteAngle;
        secondHand.Rotation = secondAngle;
    }
}

