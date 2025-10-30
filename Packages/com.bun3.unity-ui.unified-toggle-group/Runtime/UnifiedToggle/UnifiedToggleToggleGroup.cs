using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using UnityEngine;

[Serializable]
public class UnifiedOptionToggleGroup : UnifiedOption<UnifiedToggleGroup, string>
{
    protected override void SetOption(UnifiedToggleGroup component, string value)
    {
        component.SetValue(value);
    }
}

[RequireComponent(typeof(UnifiedToggleGroup))]
public partial class UnifiedToggleToggleGroup : BaseUnifiedToggle<UnifiedToggleGroup>
{
    [SerializeField, Required(inAutoCache: true), ReadOnly]
    protected UnifiedToggleGroup _group;
    protected override UnifiedToggleGroup component => _group;
    
    public UnifiedToggleGroup Group => _group;


}
