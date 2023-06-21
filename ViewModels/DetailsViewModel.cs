namespace WorldTime.ViewModel;

[QueryProperty(nameof(TimeZoneItem), "TimeZoneItem")]
public partial class DetailsViewModel  : ObservableObject
{
    IMap map;
    public DetailsViewModel()
    {
        
    }

    [ObservableProperty]
    TimeZoneItem timeZoneItem;

    [RelayCommand]
    async Task OpenMap()
    {
        //try
        //{
        //    await map.OpenAsync(TimeZoneItem, Monkey.Longitude, new MapLaunchOptions
        //    {
        //        Name = Monkey.Name,
        //        NavigationMode = NavigationMode.None
        //    });
        //}
        //catch (Exception ex)
        //{
        //    Debug.WriteLine($"Unable to launch maps: {ex.Message}");
        //    await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        //}
    }
}
