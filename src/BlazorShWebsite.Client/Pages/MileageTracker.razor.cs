using System.Diagnostics;
using System.Text.Json;
using BlazorShWebsite.Client.Js;
using BlazorShWebsite.Client.Services;
using BlazorShWebsite.Client.Services.Mileage;
using Microsoft.AspNetCore.Components;

namespace BlazorShWebsite.Client.Pages;

public partial class MileageTracker
{
    [Inject] LocalStorage LocalStorage { get; set; }
    [Inject] private ILogger<MileageTracker> Logger { get; set; }

    private MileageState _state = new();
    
    private Dictionary<MileageInputId, MileageInput> _pageInputs = new()
    {
        {MileageInputId.InitialMileage, new()},
        {MileageInputId.ContractedMiles, new()}
    };

    protected override async Task OnInitializedAsync()
    {
        await _state.LoadFromServer("Kunal");
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await LocalStorage.Initialise();
            
            var localStorageIsState = _state.LoadFromLocalStorage(await LocalStorage.GetItem("mileage-data"));

            if (localStorageIsState)
            {
                await LocalStorage.SetItem("mileage-data", JsonSerializer.Serialize(_state));
            }
            else
            {
                await Task.Yield();
                StateHasChanged();
            }
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
        _state.Rows.Add(new());
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
        var mileageRow = _state.Rows[row];
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