using BlazorShWebsite.Client.Services.Mileage;
using Microsoft.AspNetCore.Components;

namespace BlazorShWebsite.Client.Pages;

public partial class MileageTracker
{
    private Dictionary<MileageInputId, MileageInput> _inputs = new()
    {
        {MileageInputId.InitialMileage, new() {}}
    };
    //
    // private List<MileageRowComponent> _mileageRows;

    private MileageTrackerService _service;

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _service = new MileageTrackerService([]);
        }
    }

    private async Task OnInputEventHandler(ChangeEventArgs args, MileageInputId mileageInputId)
    {
        
    }
}