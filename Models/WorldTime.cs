namespace WorldTime
{
    public class TimeZoneItem
    {
        public string DisplayName { get; set; }
        public string CurrentTime { get; set; }

        public string StandardName { get; set; }

        public string Id { get; internal set; }
        public bool IsSelected { get; set; }
    }
}
