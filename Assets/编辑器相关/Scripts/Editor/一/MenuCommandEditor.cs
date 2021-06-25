using UnityEditor;
using UnityEngine;

public class MenuCommandEditor : ScriptableObject
{
    [MenuItem("CONTEXT/PlayerHealth/Init")]
    //用于获取当前操作的组件
    private static void Init(MenuCommand cmd)
    {
        PlayerHealth health = cmd.context as PlayerHealth;
        Debug.Log("PlayerHealth");
    }
}
