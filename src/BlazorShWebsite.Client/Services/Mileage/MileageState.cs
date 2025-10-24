namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageState
{
    public DateTime LastModified { get; set; }
    public int Version { get; set; }
    public int? InitialMileage { get; set; }
    public int? ContractedMiles { get; set; }
    public List<MileageRow> Rows { get; set; } = [];
}