using Microsoft.AspNetCore.Components;

namespace FremrolBlazor.Services;

public class ModalService
{
    public event Action<Type, KeyValuePair<string, object>[]>? OnShow;
    public event Action<string>? OnClose;

    public void Show<T>(params KeyValuePair<string, object>[] parameters) where T : IComponent
    {
        OnShow?.Invoke(typeof(T), parameters);
    }

    public void Close(string id)
    {
        OnClose?.Invoke(id);
    }
}