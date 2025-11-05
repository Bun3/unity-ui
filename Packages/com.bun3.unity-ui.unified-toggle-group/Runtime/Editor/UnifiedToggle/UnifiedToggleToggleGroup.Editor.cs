using UnityEditor;

namespace UnifiedToggle
{
    public partial class UnifiedToggleToggleGroup
    {
        protected override void OnValidate()
        {
            base.OnValidate();

            if (_group == _authorGroup)
            {
                EditorUtility.DisplayDialog(
                    "잘못된 그룹 설정",
                    "자기 자신을 그룹으로 설정할 수 없습니다.",
                    "확인"
                );
                _authorGroup.Unregister(this);
                _authorGroup = null;
            }
        }
    }
}
