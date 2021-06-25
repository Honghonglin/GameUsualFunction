using UnityEditor;
using UnityEngine;
//数据类一般不在Editor中
//如果遇到ScriptableObject数据不能正常保存的情况，
//可以尝试使用EditorUtility.SetDirty方法，
//标记该ScriptableObject为“脏”，然后就能正常保存了。
[CreateAssetMenu(fileName = "MyData", menuName = "Custom/MyDataAsset", order = 1)]
public class MyData : ScriptableObject
{
    public int id;
    public string objName;
    public float value;
    public bool isUsed;
}
