using System.Text.Json;

namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageStateManager(ILogger<MileageStateManager> logger)
{
    private MileageState _clientMileageState;
    private MileageState _serverMileageState;
    
    public MileageStateManager GetOrCreate(string? mileageStateString)
    {
        if (mileageStateString != null)
        {
            try
            {
                _clientMileageState = JsonSerializer.Deserialize<MileageState>(mileageStateString)!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not deserialize mileage state");
            }

        }
        _clientMileageState = new MileageState
        {
            LastModified = DateTime.UtcNow,
            Version = 0,
            InitialMileage = null,
            ContractedMiles = null,
            Rows = [new MileageRow()]
        };
        
        return this;
    }
}