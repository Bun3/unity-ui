using System;
using Core.Attributes.ReadOnly;

namespace UnifiedToggle
{
    [Serializable]
    public abstract class UnifiedOptionBase
    {
        [Serializable]
        public class BaseOption
        {
            [ReadOnly] public string key;
        }
    }
}
