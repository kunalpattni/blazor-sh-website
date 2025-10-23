using BlazorShWebsite.Client.Js;
using Legacy = BlazorShWebsite.Client.Js.Legacy;

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
    
    [Inject] Legacy.LocalStorage LegacyLocalStorage { get; set; }
    private string AfterRenderServer = "loading";
    private string AfterRenderBrowser = "loading";
    private string Initialized = "loading";
    private string ParametersSet = "loading";

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            AfterRenderServer = await LegacyLocalStorage.GetItem("test2");
            if (OperatingSystem.IsBrowser())
            {
                AfterRenderBrowser = LocalStorage.GetItem("test");
            }

            // Task.Yield();
            StateHasChanged();
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
        StateHasChanged();
    }
    private void RecalculateRow(ChangeEventArgs args, int row, MileageRowInputId inputId)
    {
        var mileageRow = _rowInputs[row];
        switch (inputId)
        {
            case MileageRowInputId.FillDate:
                if (DateOnly.TryParseExact(Convert.ToString(args.Value), "yyyy-MM-dd", out var fillDate))
                {
                    mileageRow.FillDate = fillDate;
                }
                break;
            case MileageRowInputId.CurrentMileage:
                if (int.TryParse(Convert.ToString(args.Value), out var currentMileage)) 
                {
                    mileageRow.CurrentMileage = currentMileage;
                }
                break;
            case MileageRowInputId.PricePerLitre:
                if (decimal.TryParse(Convert.ToString(args.Value), out var pricePerLitre)) 
                {
                    mileageRow.PricePerLitre = pricePerLitre;
                }
                break;
            case MileageRowInputId.TotalPrice:
                if (decimal.TryParse(Convert.ToString(args.Value), out var totalPrice)) 
                {
                    mileageRow.TotalPrice = totalPrice;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(inputId), inputId, null);
        }
        StateHasChanged();
    }
}