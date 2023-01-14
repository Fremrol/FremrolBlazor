using FremrolBlazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using System.Timers;
using Timer = System.Timers.Timer;

namespace FremrolBlazor.Services;

public class ToastService : IDisposable
{
    public event Action<RenderFragment, RenderFragment, ToastType>? OnShow;
    public event Action? OnHide;

    private Timer? _countdown;

    public void ShowToast(RenderFragment head, RenderFragment body, ToastType type)
    {
        OnShow?.Invoke(head, body, type);
        StartCountdown();
    }

    private void StartCountdown()
    {
        SetCountdown();

        if (_countdown!.Enabled)
        {
            _countdown.Stop();
            _countdown.Start();
            return;
        }
        
        _countdown.Start();
    }

    private void SetCountdown()
    {
        if (_countdown != null)
            return;

        _countdown = new Timer(5000);
        _countdown.Elapsed += HideToast!;
        _countdown.AutoReset = false;
    }

    private void HideToast(object source, ElapsedEventArgs args) => OnHide?.Invoke();
    public void Dispose() => _countdown?.Dispose();
}