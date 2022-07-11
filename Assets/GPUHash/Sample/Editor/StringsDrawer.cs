using System.Diagnostics;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringsAttribute))]
public class StringsDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        StringsAttribute strings = (StringsAttribute)attribute;

        strings.Index = EditorGUI.Popup(
            position,
            property.name,
            strings.Index,
            strings.Strings
        );

        property.stringValue = strings.Strings[strings.Index];
    }
}
