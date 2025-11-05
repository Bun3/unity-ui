using Cysharp.Threading.Tasks;

namespace UnifiedToggle
{
    public interface IUnifiedOption
    {
        void SetOptionValues(string[] values);
    }

    public interface IUnifiedOption<in TComponent> : IUnifiedOption
    {
        public UniTask SetValueAsync(TComponent component, string value);
    }
}
