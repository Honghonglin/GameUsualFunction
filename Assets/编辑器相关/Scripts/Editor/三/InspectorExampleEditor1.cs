using UnityEditor;
using UnityEngine;

//第二种绘制方式相较于第一种，显示的效果是差不多的。
//虽然脚本内容多了一点，但是方式比较简单。
//不用根据每个变量的数据类型选择相对应的属性API绘制。
public class InspectorExampleEditor1 : Editor
{
    //定义序列化属性
    private SerializedProperty intValue;
    private SerializedProperty floatValue;
    private SerializedProperty stringValue;
    private SerializedProperty boolValue;
    private SerializedProperty vector3Value;
    private SerializedProperty enumValue;
    private SerializedProperty colorValue;
    private SerializedProperty textureValue;

    private void OnEnable()
    {
        //通过名字查找被序列化属性。
        intValue = serializedObject.FindProperty("intValue");
        floatValue = serializedObject.FindProperty("floatValue");
        stringValue = serializedObject.FindProperty("stringValue");
        boolValue = serializedObject.FindProperty("boolValue");
        vector3Value = serializedObject.FindProperty("vector3Value");
        enumValue = serializedObject.FindProperty("enumValue");
        colorValue = serializedObject.FindProperty("colorValue");
        textureValue = serializedObject.FindProperty("textureValue");

    }

    public override void OnInspectorGUI()
    {
        //表示更新序列化物体
        serializedObject.Update();

        EditorGUILayout.PropertyField(intValue);
        EditorGUILayout.PropertyField(floatValue);
        EditorGUILayout.PropertyField(stringValue);
        EditorGUILayout.PropertyField(boolValue);
        EditorGUILayout.PropertyField(vector3Value);
        EditorGUILayout.PropertyField(enumValue);
        EditorGUILayout.PropertyField(colorValue);
        EditorGUILayout.PropertyField(textureValue);

        //应用修改的属性值，不加的话，Inspector面板的值修改不了
        serializedObject.ApplyModifiedProperties();

        
    }
}
