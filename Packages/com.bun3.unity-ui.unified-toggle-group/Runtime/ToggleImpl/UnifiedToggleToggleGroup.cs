using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnifiedToggle
{
    [Serializable]
    public class UnifiedOptionToggleGroup : UnifiedOption<UnifiedToggleGroup, string>
    {
        protected override async UniTask SetOption(UnifiedToggleGroup component, string value)
        {
            await component.SetValueAsync(value);
        }
    }

    [RequireComponent(typeof(UnifiedToggleGroup))]
    public partial class UnifiedToggleToggleGroup : BaseUnifiedToggle<UnifiedToggleGroup>
    {
        [SerializeField, Required(inAutoCache: true), ReadOnly]
        protected UnifiedToggleGroup _group;
        public override UnifiedToggleGroup component => _group;
    
        public UnifiedToggleGroup Group => _group;


    }
}
