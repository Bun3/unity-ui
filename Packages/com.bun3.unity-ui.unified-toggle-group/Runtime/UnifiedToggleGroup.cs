using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public sealed partial class UnifiedToggleGroup : UnifiedToggle
{
    [SerializeField] private string[] _presets = { "Off", "On" };
    [SerializeField] private BaseUnifiedToggle[] _toggles = { };

    public string CurrentPreset { get; private set; } = string.Empty;

    public bool isOn
    {
        get => CurrentPreset == _presets[^1];
        set => SetValue(value ? _presets[^1] : _presets[0]);
    }

    public override void SetValue(string value)
    {
        CurrentPreset = value;
        foreach (var toggle in _toggles)
            toggle.SetValue(value);
    }

    public override void SetOptionValues(string[] values)
    {
        foreach (var toggle in _toggles)
            toggle.SetOptionValues(values);
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