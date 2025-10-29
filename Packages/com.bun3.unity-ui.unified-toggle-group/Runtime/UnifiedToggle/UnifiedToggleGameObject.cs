using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UnifiedOptionGameObjectActive : UnifiedOption<UnifiedToggleGameObject, GameObject[]>
{
    protected override GameObject[] GetDefaultOption()
    {
        return new GameObject[0];
    }
    
    protected override void SetOption(UnifiedToggleGameObject component, GameObject[] value)
    {
        var all = GetAllGameObjects().Distinct();
        var toDeactivate = all.Except(value);

        foreach (var go in toDeactivate)
        {
            if (go != null)
                go.SetActive(false);
        }

        foreach (var go in value)
        {
            if (go != null)
                go.SetActive(true);
        }
    }

    public IEnumerable<GameObject> GetAllGameObjects()
    {
        return _options.SelectMany(x => x.option).Where(x => x != null);
    }
}

[Serializable]
public class UnifiedToggleGameObject : BaseUnifiedToggle<UnifiedToggleGameObject>
{
    protected override UnifiedToggleGameObject component => this;
}