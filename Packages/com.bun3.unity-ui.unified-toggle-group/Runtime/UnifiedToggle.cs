using UnityEngine;

public abstract class UnifiedToggle : MonoBehaviour, IUnifiedToggle
{
    public abstract void SetValue(string value);
    public abstract void SetOptionValues(string[] values);
}