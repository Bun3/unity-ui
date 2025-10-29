using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed partial class UnifiedToggleGroup : UnifiedToggle
{
    [SerializeField] private string[] _presets = { "On", "Off" };
    [SerializeField] private BaseUnifiedToggle[] _toggles = { };

    private IEnumerable<IUnifiedToggle> Toggles => _toggles;

    public string CurrentPreset { get; private set; } = string.Empty;

    public override void SetValue(string value)
    {
        CurrentPreset = value;
        foreach (var toggle in Toggles)
        {
            if (toggle as UnityEngine.Object != this)
                toggle.SetValue(value);
        }
    }

    public override void SetOptionValues(string[] values)
    {
        foreach (var toggle in Toggles)
        {
            if (toggle as UnityEngine.Object != this)
                toggle.SetOptionValues(values);
        }
    }

    public string[] GetPresets()
    {
        return _presets;
    }

    public UnifiedToggle[] GetToggles()
    {
        return _toggles;
    }
}