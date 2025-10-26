using System.Text.Json;

namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageStateManager(ILogger<MileageStateManager> logger)
{
    public MileageState State { get; set; }
    
    public MileageStateManager GetOrCreate(string? mileageStateString)
    {
        if (mileageStateString != null)
        {
            try
            {
                State = JsonSerializer.Deserialize<MileageState>(mileageStateString)!;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not deserialize mileage state");
            }

        }
        State = new MileageState
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