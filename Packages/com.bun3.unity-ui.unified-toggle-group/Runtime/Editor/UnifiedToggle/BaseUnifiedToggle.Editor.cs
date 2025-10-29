using System;
using System.Collections.Generic;
using UnityEditor;

public partial class BaseUnifiedToggle
{
}

public partial class BaseUnifiedToggle<TComponent>
{
    protected virtual void OnValidate()
    {
        var set = new HashSet<Type>();
        for (var i = 0; i < _options.Count; i++)
        {
            var option = _options[i];
            if (option == null)
                continue;
            
            var type = option.GetType();
            if (!set.Add(type))
            {
                EditorUtility.DisplayDialog(
                    "중복된 옵션 형식 감지",
                    "옵션 형식 " + type.Name + " 이(가) 중복되었습니다. 동일한 옵션 형식은 하나만 허용됩니다.",
                    "확인"
                );
                
                _options[i] = null;
            }
        }
    }
}