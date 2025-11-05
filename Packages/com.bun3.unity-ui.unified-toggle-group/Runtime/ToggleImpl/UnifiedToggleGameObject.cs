using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZLinq;

namespace UnifiedToggle
{
    [Serializable]
    public class UnifiedOptionGameObjectActive : UnifiedOption<UnifiedToggleGameObject, GameObject[]>
    {
        protected override GameObject[] GetDefaultOption()
        {
            return new GameObject[0];
        }

        protected override async UniTask SetOption(UnifiedToggleGameObject component, GameObject[] toActivate)
        {
            var all = _options
                .SelectMany(x => x.option)
                .Where(x => x != null)
                .Distinct();
            
            var toDeactivate = all.Except(toActivate);
            foreach (var go in toDeactivate)
            {
                if (go != null)
                    go.SetActive(false);
            }

            foreach (var go in toActivate)
            {
                if (go != null)
                    go.SetActive(true);
            }
        }
        
    }

    [Serializable]
    public partial class UnifiedToggleGameObject : BaseUnifiedToggle<UnifiedToggleGameObject>
    {
        protected override UnifiedToggleGameObject component => this;
    }
}