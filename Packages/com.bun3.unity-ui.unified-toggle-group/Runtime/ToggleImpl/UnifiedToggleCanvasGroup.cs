using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnifiedToggle
{
    [Serializable]
    public class UnifiedOptionCanvasGroupAlpha : UnifiedOption<CanvasGroup, float>
    {
        protected override float GetDefaultOption()
        {
            return 1f;
        }

        protected override async UniTask SetOption(CanvasGroup component, float value)
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

        protected override async UniTask SetOption(CanvasGroup component, bool value)
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

        protected override async UniTask SetOption(CanvasGroup component, bool value)
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
}