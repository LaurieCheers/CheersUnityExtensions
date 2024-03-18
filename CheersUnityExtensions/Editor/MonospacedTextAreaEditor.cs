using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MonospacedTextAreaAttribute))]
public class MonospacedTextAreaEditor : PropertyDrawer
{
    float height = 50;
    float lineHeight;
    GUIStyle style;
    static Font monoFont;
    public MonospacedTextAreaEditor()
    {
        if (monoFont == null)
            monoFont = AssetDatabase.LoadAssetAtPath<Font>("Packages/com.cheers.unityextensions/CheersUnityExtensions/Fonts/consola.ttf");
        style = new GUIStyle(GUI.skin.textArea) { font = monoFont };
        lineHeight = style.CalcSize(new GUIContent("|\n|")).y - style.CalcSize(new GUIContent("|")).y;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return height;// base.GetPropertyHeight(property, label);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int lines = 1+property.stringValue.Count(stopCountingAt:10, c => c == '\n');
        height = Mathf.Max(lines * lineHeight, 40);
        property.stringValue = EditorGUI.TextArea(position, property.stringValue, style);
    }
}
