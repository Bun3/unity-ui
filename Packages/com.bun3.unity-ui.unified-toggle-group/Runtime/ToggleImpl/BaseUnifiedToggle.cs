using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZLinq;

namespace UnifiedToggle
{
    [ExecuteAlways]
    public abstract partial class BaseUnifiedToggle : MonoBehaviour, IUnifiedToggle
    {
        [SerializeField] protected UnifiedToggleGroup _authorGroup;
    
        public abstract UniTask SetValueAsync(string value);

        partial void OnDestroyEditor();
        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            OnDestroyEditor();
#endif
            _authorGroup.EnsureValidToggles();
        }

        protected virtual void OnEnable()
        {
            _authorGroup.Register(this);
        }

        protected virtual void OnDisable()
        {
            _authorGroup.Unregister(this);
        }
    }

    [DisallowMultipleComponent]
    public abstract partial class BaseUnifiedToggle<TComponent> : BaseUnifiedToggle where TComponent : Component
    {
        protected abstract TComponent component { get; }

        [SerializeReference, SubclassSelector] protected List<IUnifiedOption<TComponent>> _options = new();

        public sealed override async UniTask SetValueAsync(string value)
        {
            var count = _options.Count;
            if (count == 0)
                return;

            using var arrayPool = _options
                .Where(o => o != null)
                .Select(o => o.SetValueAsync(component, value))
                .ToArrayPool();
            await UniTask.WhenAll(arrayPool.Array);
        }
    
    }
}