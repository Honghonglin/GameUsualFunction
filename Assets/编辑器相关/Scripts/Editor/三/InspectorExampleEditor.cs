using UnityEditor;
using UnityEngine;

//第一种方式
[CustomEditor(typeof(InspectorExample))]
public class InspectorExampleEditor : Editor
{
    private InspectorExample _target { get { return target as InspectorExample; } }

    //GUI重新绘制
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("IntValue", _target.intValue.ToString(), EditorStyles.boldLabel);
        _target.intValue = EditorGUILayout.IntSlider(new GUIContent("Slider"), _target.intValue, 0, 10);
        _target.floatValue = EditorGUILayout.Slider(new GUIContent("FloatValue"), _target.floatValue, 0, 10);
        _target.intValue = EditorGUILayout.IntField("IntValue", _target.intValue);
        _target.floatValue = EditorGUILayout.FloatField("FloatValue", _target.floatValue);
        _target.stringValue = EditorGUILayout.TextField("StringValue", _target.stringValue);
        _target.boolValue = EditorGUILayout.Toggle("BoolValue", _target.boolValue);
        _target.vector3Value = EditorGUILayout.Vector3Field("Vector3Value", _target.vector3Value);
        _target.enumValue = (Course)EditorGUILayout.EnumPopup("EnumValue", _target.enumValue);
        _target.colorValue = EditorGUILayout.ColorField(new GUIContent("ColorValue"), _target.colorValue);
        _target.textureValue = (Texture)EditorGUILayout.ObjectField("TextureValue", _target.textureValue, typeof(Texture), true);


        //水平和垂直布局，注意这是一个方法对，Begin和End不能少
        EditorGUILayout.BeginVertical("box");
        GUILayout.Label("编辑器入门");
        GUILayout.Label("编辑器入门");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal("box");
        GUILayout.Label("编辑器入门");
        GUILayout.Label("编辑器入门");
        EditorGUILayout.EndHorizontal();
    }
}
