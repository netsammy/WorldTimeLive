namespace WorldTime.Utils
{
    public static class TimeFormatter
    {

        //create property to hold timeformat
        public static string TimeFormat { get; set; } = "HH:mm:ss tt";
        public static string FormatTime(DateTime time) { return time.ToString(TimeFormat); }

        //public static string FormatTime(DateTime time)
        //{
        //    return time.ToString("HH:mm:ss");
        //}
    }

}
