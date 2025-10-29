using System;
using UnityEngine;

namespace Core.Attributes.ReadOnly
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }
}
