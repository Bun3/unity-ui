using System;
using System.Linq;
using UnityEngine;

public partial class UnifiedToggleGroup
{
    [ContextMenu(nameof(FindAll))]
    private void FindAll()
    {
        var childGroups = GetComponentsInChildren<UnifiedToggleGroup>()
            .Where(c => c.gameObject != gameObject);

        var ignores = childGroups.SelectMany(c => c._toggles);

        _toggles = GetComponentsInChildren<BaseUnifiedToggle>()
            .Except(ignores)
            .ToArray();
    }
    
    [ContextMenu(nameof(UpdateValues))]
    public void UpdateValues()
    {
        SetOptionValues(_presets);
    }
    
    public void Register(BaseUnifiedToggle toggle)
    {
        if (_toggles.Contains(toggle)) 
            return;
        Array.Resize(ref _toggles, _toggles.Length + 1);
        _toggles[^1] = toggle;
        UpdateValues();
    }
    
    public void Unregister(BaseUnifiedToggle toggle)
    {
        if (!_toggles.Contains(toggle)) 
            return;
        _toggles = _toggles.Where(t => t != toggle).ToArray();
        UpdateValues();
    }
}