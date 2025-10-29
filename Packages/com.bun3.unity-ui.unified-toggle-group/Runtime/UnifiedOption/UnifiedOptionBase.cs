using System;
using Core.Attributes.ReadOnly;

[Serializable]
public abstract class UnifiedOptionBase
{
    [Serializable]
    public class BaseOption
    {
        [ReadOnly] public string key;
    }
}
