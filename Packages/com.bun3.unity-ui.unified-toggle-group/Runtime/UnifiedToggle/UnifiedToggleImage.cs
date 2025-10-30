using System;
using Core.Attributes.ReadOnly;
using Core.Attributes.Required;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UnifiedOptionImageSprite : UnifiedOption<Image, Sprite>
{
    protected override void SetOption(Image component, Sprite value)
    {
        component.sprite = value;
    }
}

[Serializable]
public class UnifiedOptionImageOverrideSprite : UnifiedOption<Image, Sprite>
{
    protected override void SetOption(Image component, Sprite value)
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

    protected override void SetOption(Image component, Color value)
    {
        component.color = value;
    }
}

[RequireComponent(typeof(Image))]
public class UnifiedToggleImage : BaseUnifiedToggle<Image>
{
    [SerializeField, Required(inAutoCache: true), ReadOnly]
    protected Image _image;
    protected override Image component => _image;
}
