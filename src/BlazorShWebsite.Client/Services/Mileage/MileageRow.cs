using System.Globalization;

namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageRow
{
    public DateOnly? FillDate { get; set; }
    public int? CurrentMileage { get; set; }
    public decimal? PricePerLitre { get; set; }
    public decimal? TotalPrice { get; set; }

    private decimal? LitresFilled => TotalPrice / PricePerLitre;

    private decimal? CostPerMile(int? previousMileage)
    {
        if (CurrentMileage is null || TotalPrice is null || previousMileage is null)
        {
            return null;
        }
        return (CurrentMileage - previousMileage) / TotalPrice;
    }

    private int? DaysSinceLastFill(DateOnly? previousFillDate)
    {
        if (previousFillDate is null || FillDate is null)
        {
            return null;
        }
        return (Convert.ToDateTime(FillDate) - Convert.ToDateTime(previousFillDate)).Days;
    }

    private decimal? CostPerDay(DateOnly? previousFillDate)
    {
        if (previousFillDate is null || TotalPrice is null)
        {
            return null;
        }

        return TotalPrice / DaysSinceLastFill(previousFillDate);
    }

    public string CostPerMileString(int? previousMileage)
    {
        var costPerMile = CostPerMile(previousMileage);
        if (costPerMile is null)
        {
            return "-";
        } 
        return costPerMile.Value.ToString("C", CultureInfo.CreateSpecificCulture("en-GB"));
    }

    public string DaysSinceLastFillString(DateOnly? previousFillDate)
    {
        var daysSinceLastFill = DaysSinceLastFill(previousFillDate);
        if (daysSinceLastFill is null)
        {
            return "-";
        } 
        return daysSinceLastFill.Value.ToString("N", CultureInfo.CreateSpecificCulture("en-GB"));
    }
    
    public string CostPerDayString(DateOnly? previousFillDate)
    {
        var costPerDay = CostPerDay(previousFillDate);
        if (costPerDay is null)
        {
            return "-";
        } 
        return costPerDay.Value.ToString("C", CultureInfo.CreateSpecificCulture("en-GB"));
    }
    
    public string LitresFilledString()
    {
        var litresFilled = LitresFilled;
        if (litresFilled is null)
        {
            return "-";
        } 
        return litresFilled.Value.ToString("F", CultureInfo.CreateSpecificCulture("en-GB"));
    }
}