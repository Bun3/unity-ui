using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UnifiedToggle
{
    [Serializable]
    public class UnifiedOptionLayoutElementIgnoreLayout : UnifiedOption<LayoutElement, bool>
    {
        protected override async UniTask SetOption(LayoutElement component, bool value)
        {
            component.ignoreLayout = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementMinWidth : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.minWidth = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementMinHeight : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.minHeight = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementPreferredWidth : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.preferredWidth = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementPreferredHeight : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.preferredHeight = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementFlexibleWidth : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.flexibleWidth = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementFlexibleHeight : UnifiedOption<LayoutElement, float>
    {
        protected override float GetDefaultOption()
        {
            return -1f;
        }

        protected override async UniTask SetOption(LayoutElement component, float value)
        {
            component.flexibleHeight = value;
        }
    }

    [Serializable]
    public class UnifiedOptionLayoutElementLayoutPriority : UnifiedOption<LayoutElement, int>
    {
        protected override int GetDefaultOption()
        {
            return 1;
        }

        protected override async UniTask SetOption(LayoutElement component, int value)
        {
            component.layoutPriority = value;
        }
    }

    [RequireComponent(typeof(LayoutElement))]
    public class UnifiedToggleLayoutElement : BaseUnifiedToggle<LayoutElement>
    {
        [SerializeField, Required(inAutoCache: true), ReadOnly]
        protected LayoutElement _layoutElement;

        public override LayoutElement component => _layoutElement;
    }
}