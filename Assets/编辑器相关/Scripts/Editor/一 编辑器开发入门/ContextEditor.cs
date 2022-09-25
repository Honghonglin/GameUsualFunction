using UnityEditor;
using UnityEngine;

//建议用一个类来存放MenuItem属性标记的(在组件右击出现的方法）
public class ContextEditor : ScriptableObject
{
    //给某组件添加右键菜单选项
    //注意CONTEXT大写
    //只要加上这个特性起作用，不管在哪个类
    [MenuItem("CONTEXT/Rigidbody/Init")]
    private static void RigidbodyInit()
    {
        Debug.Log("Init");
    }
    
    [MenuItem("CONTEXT/PlayerHealth/Init1")]
    private static void ScriptInit()
    {
        Debug.Log("Init1");
    }
}
