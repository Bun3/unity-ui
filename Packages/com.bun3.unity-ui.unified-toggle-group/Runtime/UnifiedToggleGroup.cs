using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using ZLinq;

[assembly: ZLinqDropIn("UnifiedToggle", DropInGenerateTypes.Everything)]
namespace UnifiedToggle
{
    
    [DisallowMultipleComponent]
    public sealed partial class UnifiedToggleGroup : MonoBehaviour, IUnifiedToggle
    {
        [SerializeField] private string[] _presets = { "Off", "On" };
        [SerializeField] private BaseUnifiedToggle[] _toggles = { };

        public string CurrentPreset { get; private set; } = string.Empty;

        public bool isOn => CurrentPreset == _presets[^1];

        public void SetOn(bool isOn)
        {
            SetOnAsync(isOn).Forget();
        }

        public async UniTask SetOnAsync(bool isOn)
        {
            await SetValueAsync(isOn ? _presets[^1] : _presets[0]);
        }

        public void SetValue(string preset)
        {
            SetValueAsync(preset).Forget();
        }

        public async UniTask SetValueAsync(string value)
        {
            if (CurrentPreset == value)
                return;
        
            CurrentPreset = value;

            using var arrayPool = _toggles
                .Where(t => t != null && t.enabled)
                .Select(t => t.SetValueAsync(value))
                .ToArrayPool();
            await UniTask.WhenAll(arrayPool.Array);
        }

        public string[] GetPresets()
        {
            return _presets;
        }
        
        public void Register(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (_toggles.Contains(toggle))
                return;
            Array.Resize(ref _toggles, _toggles.Length + 1);
            _toggles[^1] = toggle;
            UpdateValues();
        }

        public void Unregister(BaseUnifiedToggle toggle)
        {
            if (toggle == null)
                return;
            if (!_toggles.Contains(toggle))
                return;
            _toggles = _toggles.Where(t => t != toggle).ToArray();
            UpdateValues();
        }

        public void EnsureValidToggles()
        {
            _toggles = _toggles.Where(x => x != null).ToArray();
        }

        [ContextMenu(nameof(UpdateValues))]
        public void UpdateValues()
        {
            SetOptionValues(_presets);
        }

        public void SetOptionValues(string[] values)
        {
            foreach (var toggle in _toggles)
                toggle.SetOptionValues(values);
        }

    }
}