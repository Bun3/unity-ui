#if UNITY_EDITOR
namespace UnifiedToggle
{
    public partial interface IUnifiedToggle
    {
        void SetOptionValues(string[] values);
    }
}
#endif