using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Test4))]
public class Test4Editor : Editor
{
    private GUIStyle _titleStyle;

    private void OnEnable()
    {
        GUISkin skin = Resources.Load("编辑器相关/New GUISkin") as GUISkin;
        _titleStyle = skin.label;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("GUIStyle");
        EditorGUILayout.LabelField("GUIStyle", _titleStyle);
    }
}
