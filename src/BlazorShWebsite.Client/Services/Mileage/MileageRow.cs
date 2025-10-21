namespace BlazorShWebsite.Client.Services.Mileage;

public class MileageRow
{
    public DateOnly? FillDate { get; set; }
    public int? CurrentMileage { get; set; }
    public decimal? PricePerLitre { get; set; }
    public decimal? TotalPrice { get; set; }

    public decimal? LitresFilled => TotalPrice / PricePerLitre;

    public decimal CostPerMile(int initialMileage) => (CurrentMileage - initialMileage) / TotalPrice ?? 1;
    public int? DaysSinceLastFill(DateOnly? previousFillDate) => previousFillDate is null ? 1 : (Convert.ToDateTime(FillDate ?? DateOnly.FromDateTime(DateTime.Today)) - Convert.ToDateTime(previousFillDate)).Days;
    public decimal? CostPerDay(DateOnly? previousFillDate) => TotalPrice / DaysSinceLastFill(previousFillDate) == TotalPrice ? 1 : DaysSinceLastFill(previousFillDate);

}