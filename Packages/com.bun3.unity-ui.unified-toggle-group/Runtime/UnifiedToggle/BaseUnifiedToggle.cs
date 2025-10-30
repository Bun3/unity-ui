using System.Collections.Generic;
using UnityEngine;

public abstract partial class BaseUnifiedToggle : UnifiedToggle
{
    [SerializeField] protected UnifiedToggleGroup _authorGroup;
}

public abstract partial class BaseUnifiedToggle<TComponent> : BaseUnifiedToggle where TComponent : Component
{
    protected abstract TComponent component { get; }

    [SerializeReference, SubclassSelector] protected List<IUnifiedOption<TComponent>> _options = new();

    public sealed override void SetValue(string value)
    {
        for (var i = 0; i < _options.Count; i++)
        {
            var option = _options[i];
            option?.SetValue(component, value);
        }
    }

    public sealed override void SetOptionValues(string[] values)
    {
        for (var i = 0; i < _options.Count; i++)
        {
            var option = _options[i];
            option?.SetOptionValues(values);
        }
    }
    
}
