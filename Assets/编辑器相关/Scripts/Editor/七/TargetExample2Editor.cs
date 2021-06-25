using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
[CustomEditor(typeof(TargetExample2))]
public class TargetExample2Editor : Editor
{
    public enum PrefabType
    {
        Player,
        Enemy,
    }

    public struct Creation
    {
        public PrefabType prefabType;
        public string path;
    }
    private ReorderableList _playerItemArray;

    private void OnEnable()
    {
        _playerItemArray = new ReorderableList(serializedObject, serializedObject.FindProperty("playerItemArray")
            , true, true, true, true);

        //自定义列表名称
        _playerItemArray.drawHeaderCallback = (Rect rect) =>
        {
            GUI.Label(rect, "Player Array");
        };

        //定义元素的高度
        _playerItemArray.elementHeight = 68;

        //自定义绘制列表元素
        _playerItemArray.drawElementCallback = (Rect rect, int index, bool selected, bool focused) =>
        {
            //根据index获取对应元素 
            SerializedProperty item = _playerItemArray.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, item, new GUIContent("Index " + index));
        };

        //当删除元素时候的回调函数，实现删除元素时，有提示框跳出
        _playerItemArray.onRemoveCallback = (ReorderableList list) =>
        {
            if (EditorUtility.DisplayDialog("Warnning", "Do you want to remove this element?", "Remove", "Cancel"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(list);
            }
        };

        _playerItemArray.onAddDropdownCallback = (Rect rect, ReorderableList list) =>
        {
            GenericMenu menu = new GenericMenu();
            var guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/编辑器相关/Prefabs/Player" });
            foreach (var guid in guids) 
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                //Debug.Log(System.IO.Path.GetFileNameWithoutExtension(path));
                menu.AddItem(new GUIContent("Player/" + System.IO.Path.GetFileNameWithoutExtension(path))
                    , false, ClickHandler, new Creation() { prefabType = PrefabType.Player, path = path });
            }
            //添加分割线
            menu.AddSeparator("");
            guids = AssetDatabase.FindAssets("t:Prefab", new string[] { "Assets/编辑器相关/Prefabs/Enemy" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Enemy/" + System.IO.Path.GetFileNameWithoutExtension(path))
                    , false, ClickHandler, new Creation() { prefabType = PrefabType.Enemy, path = path });
            }
            //显示鼠标下方的菜单
            menu.ShowAsContext();
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //自动布局绘制列表
        _playerItemArray.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
    private void ClickHandler(object target)
    {
        Creation creation = (Creation)target;
        int index = _playerItemArray.serializedProperty.arraySize;
        _playerItemArray.serializedProperty.arraySize++;
        _playerItemArray.index = index;
        SerializedProperty element = _playerItemArray.serializedProperty.GetArrayElementAtIndex(index);

        switch (creation.prefabType)
        {
            case PrefabType.Player:
                SpawnCharacter(creation, element, 90);
                break;
            case PrefabType.Enemy:
                SpawnCharacter(creation, element, 80);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void SpawnCharacter(Creation creation, SerializedProperty element, int atk)
    {
        GameObject character = AssetDatabase.LoadAssetAtPath<GameObject>(creation.path);

        GameObject obj = Instantiate(character);
        obj.name = character.name;

        SerializedProperty prefabPreperty = element.FindPropertyRelative("prefab");
        SerializedProperty iconPreperty = element.FindPropertyRelative("icon");
        SerializedProperty namePreperty = element.FindPropertyRelative("name");
        SerializedProperty attackPreperty = element.FindPropertyRelative("attack");

        prefabPreperty.objectReferenceValue = character;
        iconPreperty.objectReferenceValue = GetPreviewTex(character);
        namePreperty.stringValue = character.name;
        attackPreperty.intValue = atk;
    }

    //获取预制体的预览图
    private Texture GetPreviewTex(GameObject obj)
    {
        return AssetPreview.GetAssetPreview(obj);
    }
}
