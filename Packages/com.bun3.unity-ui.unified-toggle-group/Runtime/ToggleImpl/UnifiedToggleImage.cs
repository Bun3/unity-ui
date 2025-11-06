using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UnifiedToggle
{
    [Serializable]
    public class UnifiedOptionImageSprite : UnifiedOption<Image, Sprite>
    {
        protected override async UniTask SetOption(Image component, Sprite value)
        {
            component.sprite = value;
        }
    }

    [Serializable]
    public class UnifiedOptionImageOverrideSprite : UnifiedOption<Image, Sprite>
    {
        protected override async UniTask SetOption(Image component, Sprite value)
        {
            component.overrideSprite = value;
        }
    }


    [Serializable]
    public class UnifiedOptionImageColor : UnifiedOption<Image, Color>
    {
        protected override Color GetDefaultOption()
        {
            return Color.white;
        }

        protected override async UniTask SetOption(Image component, Color value)
        {
            component.color = value;
        }
    }

    [RequireComponent(typeof(Image))]
    public class UnifiedToggleImage : BaseUnifiedToggle<Image>
    {
        [SerializeField, Required(inAutoCache: true), ReadOnly]
        protected Image _image;

        public override Image component => _image;
    }
}