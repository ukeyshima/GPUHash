using UnityEngine;

public class StringsAttribute : PropertyAttribute
{
    public string[] Strings;
    public int Index;

    public StringsAttribute(params string[] strings)
    {
        Strings = strings;
    }
}