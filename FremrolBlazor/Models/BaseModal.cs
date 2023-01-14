using FremrolBlazor.Services;
using Microsoft.AspNetCore.Components;

namespace FremrolBlazor.Models;

public class BaseModal : ComponentBase
{
    [Inject] protected ModalService ModalService { get; set; } = default!;
    [Parameter] public string? Id { get; set; }

    protected void CloseModal()
    {
        ModalService.Close(Id!);
    }
}