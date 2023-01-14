using FremrolBlazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace FremrolBlazor.Models;

public class ModalController : ComponentBase, IDisposable
{
    [Inject] protected ModalService? ModalService { get; set; }
    [Inject] protected NavigationManager? NavigationManager { get; set; }

    protected readonly List<ModalInstance> Modals = new List<ModalInstance>();
        
    protected override void OnInitialized()
    {
        ModalService!.OnShow += ShowModal;
        ModalService!.OnClose += CloseModal;
        NavigationManager!.LocationChanged += CloseAllModals;
    }

    private void ShowModal(Type componentType, params KeyValuePair<string, object>[] componentParameters)
    {
        string id = Guid.NewGuid().ToString();

        RenderFragment modalFragment = BuildModal(componentType, id, componentParameters);
        var modalInstance = new ModalInstance(id, modalFragment);
        
        Modals.Add(modalInstance);
        InvokeAsync(StateHasChanged);
    }
    
    private RenderFragment BuildModal(Type componentType, string? id, params KeyValuePair<string, object>[] componentParameters) => builder =>
    {
        int i = 0;
        builder.OpenComponent(i++, componentType);
        builder.AddAttribute(i++, "Id", id);
                
        foreach (var parameter in componentParameters) 
            builder.AddAttribute(i++, parameter.Key, parameter.Value);
        
        builder.CloseComponent();
    };

    private void CloseModal(string id)
    {
        var modalToClose = Modals.Find(x => x.Id == id);
        Modals.Remove(modalToClose!);
        
        InvokeAsync(StateHasChanged);
    }
    
    private void CloseAllModals(object? sender, LocationChangedEventArgs e) => Modals.Clear();

    public void Dispose()
    {
        ModalService!.OnShow -= ShowModal;
        ModalService!.OnClose -= CloseModal;
    }
}