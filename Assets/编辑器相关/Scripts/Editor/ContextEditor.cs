using UnityEditor;
using UnityEngine;


public class ContextEditor : ScriptableObject
{
    //给某组件添加右键菜单选项
    //注意CONTEXT大写
    [MenuItem("CONTEXT/Rigidbody/Init")]
    private static void RigidbodyInit()
    {
        Debug.Log("Init");
    }
}
