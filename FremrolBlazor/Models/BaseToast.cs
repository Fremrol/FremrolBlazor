using FremrolBlazor.Models.Enums;
using FremrolBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace FremrolBlazor.Models;

public class BaseToast : ComponentBase, IDisposable
{
    [Inject] private ToastService? ToastService { get; set; }
    
    protected string? Class { get; set; }
    protected string? Style { get; set; }
    protected RenderFragment? Head { get; set; }
    protected RenderFragment? Body { get; set; }
    protected bool IsVisible;

    protected override void OnInitialized()
    {
        ToastService!.OnShow += ShowToast;
        ToastService.OnHide += HideToast;
    }
    
    private void ShowToast(RenderFragment? heading, RenderFragment? body, ToastType type)
    {
        BuildToast(heading, body, type);    
        IsVisible = true;
        StateHasChanged();
    }
    
    private void HideToast()
    {
        IsVisible = false;
        InvokeAsync(StateHasChanged);
    }
    
    private void BuildToast(RenderFragment? heading, RenderFragment? body, ToastType type)
    {
        switch (type)
        {
            case ToastType.Green:
                Style = "background: linear-gradient(90deg, rgba(48, 232, 49, 0) 2.38%, rgba(48, 232, 49, 0.6) 100%); border-radius: 10px; box-shadow: 0px 0px 10px 1px rgba(0, 0, 0, 0.95);";
                Head = (builder) => { builder.AddContent(0, heading); };
                Body = (builder) => { builder.AddContent(0, body); };
                break;
        }
    }

    public void Dispose()
    {
        ToastService!.OnShow -= ShowToast;
    }
}