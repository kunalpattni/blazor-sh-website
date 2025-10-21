using BlazorShWebsite.Client.Services.Mileage;
using Microsoft.AspNetCore.Components;

namespace BlazorShWebsite.Client.Pages;

public partial class MileageTracker
{
    private Dictionary<MileageInputId, MileageInput> _pageInputs = new()
    {
        {MileageInputId.InitialMileage, new()},
        {MileageInputId.ContractedMiles, new()}
    };

    private List<MileageRow> _rowInputs = new()
    {
        new MileageRow()
    };

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            
        }
    }

    private async Task OnPageInputEventHandler(ChangeEventArgs args, MileageInputId inputId)
    {
        switch (inputId)
        {
            case MileageInputId.InitialMileage:
                RecalculateAll(args);
                break;
            case MileageInputId.ContractedMiles:
                RecalculateChart(args);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(inputId), inputId, null);
        }

    }
    
    private async Task OnRowInputEventHandler(ChangeEventArgs args, int row, MileageRowInputId inputId)
    {
        RecalculateRow(args, row, inputId);
    }
    
    private void AddRow()
    {
        _rowInputs.Add(new());
    }

    private void RecalculateChart(ChangeEventArgs args)
    {
        throw new NotImplementedException();
    }

    private void RecalculateAll(ChangeEventArgs args)
    {
        throw new NotImplementedException();
    }
    private void RecalculateRow(ChangeEventArgs args, int row, MileageRowInputId inputId)
    {
        Console.WriteLine($"args.Value: {args.Value}");
        var mileageRow = _rowInputs[row];
        switch (inputId)
        {
            case MileageRowInputId.FillDate:
                mileageRow.FillDate = DateOnly.ParseExact(Convert.ToString(args.Value), "yyyy-MM-dd");
                break;
            case MileageRowInputId.CurrentMileage:
                mileageRow.CurrentMileage = Convert.ToInt32(args.Value);
                break;
            case MileageRowInputId.PricePerLitre:
                mileageRow.PricePerLitre = Convert.ToDecimal(args.Value);
                break;
            case MileageRowInputId.TotalPrice:
                mileageRow.TotalPrice = Convert.ToDecimal(args.Value);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(inputId), inputId, null);
        }
    }
}