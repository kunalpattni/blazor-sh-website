using System.ComponentModel;

namespace BlazorShWebsite.Shared.Shared;

public class NotifiableBool : INotifyPropertyChanged
{
    public NotifiableBool(bool value)
    {
        _value = value;
    }
    
    private bool _value;
    public bool Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            PropertyChanged?.Invoke(this, new(nameof(Value)));
        }
    }
    
    public Task WaitForAnyChangeAsync()
    {
        var tcs = new TaskCompletionSource();

        PropertyChangedEventHandler handler = null!;
        handler = (s, e) =>
        {
            PropertyChanged -= handler;
            tcs.TrySetResult();
        };

        PropertyChanged += handler;
        return tcs.Task;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}