using System.Collections.Generic;

public interface IUnifiedOption
{
    void SetOptionValues(string[] values);
}

public interface IUnifiedOption<in TComponent> : IUnifiedOption
{
    public void SetValue(TComponent component, string value);
}