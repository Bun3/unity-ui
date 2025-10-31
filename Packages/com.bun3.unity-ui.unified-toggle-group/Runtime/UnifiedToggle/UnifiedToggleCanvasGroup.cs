using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using UnityEngine;

[Serializable]
public class UnifiedOptionCanvasGroupAlpha : UnifiedOption<CanvasGroup, float>
{
    protected override float GetDefaultOption()
    {
        return 1f;
    }

    protected override void SetOption(CanvasGroup component, float value)
    {
        component.alpha = value;
    }
}

[Serializable]
public class UnifiedOptionCanvasGroupInteractable : UnifiedOption<CanvasGroup, bool>
{
    protected override bool GetDefaultOption()
    {
        return true;
    }

    protected override void SetOption(CanvasGroup component, bool value)
    {
        component.interactable = value;
    }
}

[Serializable]
public class UnifiedOptionCanvasGroupBlockRaycast : UnifiedOption<CanvasGroup, bool>
{
    protected override bool GetDefaultOption()
    {
        return true;
    }

    protected override void SetOption(CanvasGroup component, bool value)
    {
        component.blocksRaycasts = value;
    }
}

[RequireComponent(typeof(CanvasGroup))]
public class UnifiedToggleCanvasGroup : BaseUnifiedToggle<CanvasGroup>
{
    [SerializeField, Required(inAutoCache: true), ReadOnly]
    protected CanvasGroup _canvasGroup;

    protected override CanvasGroup component => _canvasGroup;
}