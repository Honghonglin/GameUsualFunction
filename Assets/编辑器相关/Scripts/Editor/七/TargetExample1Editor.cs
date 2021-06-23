using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(TargetExample1))]
public class TargetExample1Editor : Editor
{
    //ReorderableList可以实现通过鼠标拖动，修改列表元素的排列顺序，注意其命名空间为UnityEditorInternal
    private ReorderableList _stringArray;

    private void OnEnable()
    {
        _stringArray = new ReorderableList(serializedObject, serializedObject.FindProperty("stringArray")
            , true, true, true, true);

        //自定义列表名称
        _stringArray.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "StringArray");
        };

        //自定义绘制列表元素
        _stringArray.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
        {
            //根据index获取对应元素
            SerializedProperty item = _stringArray.serializedProperty.GetArrayElementAtIndex(index);
            rect.height = EditorGUIUtility.singleLineHeight;
            rect.y += 2;
            EditorGUI.PropertyField(rect, item, new GUIContent("Element " + index));
        };

        //当添加新元素时的回调函数，自定义新元素的值
        _stringArray.onAddCallback = (ReorderableList list) =>
        {
            //Debug.Log(list.serializedProperty.arraySize);
            if (list.serializedProperty != null)
            {
                //自定义新元素  list.serializedProperty就需要自己控制
                list.serializedProperty.arraySize++;
                list.index = list.serializedProperty.arraySize - 1;
                SerializedProperty item = list.serializedProperty.GetArrayElementAtIndex(list.index);
                item.stringValue = "Default Value";
            }
            else
            {
                ReorderableList.defaultBehaviours.DoAddButton(list);
            }
        };

        //当删除元素时候的回调函数，实现删除元素时，有提示框跳出
        _stringArray.onRemoveCallback = (ReorderableList list) =>
        {
            if (EditorUtility.DisplayDialog("Warnning", "Do you want to remove this element?", "Remove", "Cancel"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //自动布局绘制列表
        _stringArray.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
