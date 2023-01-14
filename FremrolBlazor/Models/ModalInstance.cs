using Microsoft.AspNetCore.Components;

namespace FremrolBlazor.Models;

public class ModalInstance
{
    public string? Id { get; }
    public RenderFragment? RenderFragment { get; }
    
    public ModalInstance(string? id, RenderFragment? renderFragment)
    {
        Id = id;
        RenderFragment = renderFragment;
    }
}