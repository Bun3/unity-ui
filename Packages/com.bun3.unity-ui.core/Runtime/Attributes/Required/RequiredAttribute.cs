using System;
using UnityEngine;

namespace Core.Attributes.Required
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredAttribute : PropertyAttribute
    {
        public bool autoCache { get; private set; }
        public string message { get; private set; }
        public RequiredAttribute(bool inAutoCache = false, string inMessage = null)
        {
            autoCache = inAutoCache;
            message = inMessage;
        }
    }
}
