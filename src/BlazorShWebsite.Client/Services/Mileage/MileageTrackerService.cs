namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageTrackerService(SortedDictionary<DateOnly, MileageRow> rows)
{
    public void AddRow(DateOnly key, MileageRow value)
    {
        rows.Add(key, value);
    }

    public void RemoveRow(DateOnly key)
    {
        rows.Remove(key);
    }

    public void UpdateRow(DateOnly key, MileageRow value)
    {
        rows[key] = value;
    }
}