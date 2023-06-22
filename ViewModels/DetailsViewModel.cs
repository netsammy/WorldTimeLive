namespace WorldTime.ViewModel;

[QueryProperty(nameof(TimeZoneItem), "TimeZoneItem")]
public partial class DetailsViewModel  : ObservableObject
{
#pragma warning disable CS0169 // The field 'DetailsViewModel.map' is never used
    IMap map;
#pragma warning restore CS0169 // The field 'DetailsViewModel.map' is never used
    public DetailsViewModel()
    {
        
    }

    [ObservableProperty]
    TimeZoneItem timeZoneItem;

    [RelayCommand]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    async Task OpenMap()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
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
