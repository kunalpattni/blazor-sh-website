using System.Text.Json;

namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageState
{
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public int Version { get; set; }
    public int? InitialMileage { get; set; }
    public int? ContractedMiles { get; set; }
    public List<MileageRow> Rows { get; set; } = [];

    public string LastModifiedString()
    {
        return DateTime.UtcNow.Subtract(LastModified).ToHumanReadableString();
    }
    
    public async Task LoadFromServer(string username)
    {
        var acceptedUsernames = new HashSet<string> {"Kunal", "John"};
        await Task.Delay(1000);
        
        if (acceptedUsernames.Contains(username))
        {
            LastModified = DateTime.UtcNow.AddHours(-2);
            Version = 0;
            InitialMileage = null;
            ContractedMiles = null;
            Rows = [new MileageRow()];
        }
    }
    
    public bool LoadFromLocalStorage(string? mileageStateString)
    {
        var localStorageIsStale = true;
        if (mileageStateString is null)
        {
            return localStorageIsStale;
        }
        try
        {
            var newState = JsonSerializer.Deserialize<MileageState>(mileageStateString)!;
            if (LastModified < newState.LastModified)
            {
                LastModified = newState.LastModified;
                Version = newState.Version;
                InitialMileage = newState.InitialMileage;
                ContractedMiles = newState.ContractedMiles;
                Rows = newState.Rows;
                localStorageIsStale = false;
            }
        }
        catch (Exception _)
        {
            localStorageIsStale = true;
        }

        return localStorageIsStale;
    }
}