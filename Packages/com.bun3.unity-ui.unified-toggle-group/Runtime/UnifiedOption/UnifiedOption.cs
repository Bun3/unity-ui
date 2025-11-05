using System;
using System.Buffers;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZLinq;

namespace UnifiedToggle
{
    [Serializable]
    public abstract class UnifiedOption<TComponent, TOption> : UnifiedOptionBase, IUnifiedOption<TComponent>
    {
        [Serializable]
        public class Option : BaseOption
        {
            public TOption option;
        }

        protected virtual TOption GetDefaultOption()
        {
            return default;
        }

        //ToggleGroup의 preset 카운트만큼 존재
        [SerializeField] protected List<Option> _options = new();
        
        public IReadOnlyCollection<Option> Options => _options;

        public void SetOptionValues(string[] values)
        {
            // 더 이상 존재하지 않는 프리셋에 해당하는 옵션을 제거합니다.
            _options.RemoveAll(opt => !values.Contains(opt.key));

            var currentKeys = _options.Select(opt => opt.key).ToHashSet();

            // 새롭게 추가된 프리셋에 대한 옵션을 추가합니다.
            foreach (var value in values)
            {
                if (!currentKeys.Contains(value))
                {
                    _options.Add(new Option
                    {
                        key = value,
                        option = GetDefaultOption()
                    });
                }
            }
        }

        public async UniTask SetValueAsync(TComponent component, string value)
        {
            var count = _options.Count;
            if (count == 0)
                return;

            using var arrayPool = _options
                .Where(o => o != null && o.key == value)
                .Select(o => SetOption(component, o.option))
                .ToArrayPool();
            await UniTask.WhenAll(arrayPool.Array);
        }

        protected abstract UniTask SetOption(TComponent component, TOption value);
    }
}