using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuItemEditor : ScriptableObject
{
    //第三个参数priority是优先级，用来表示菜单按钮的先后顺序，默认值为1000。一般菜单中的分栏，数值相差大于10。
    [MenuItem("MenuItem/test1", false,110)]
    private static void Test1()
    {
        Debug.Log("Test1");
    }
    //第二个参数为验证函数，判断是否可用  返回为true则按钮enable   为false则按钮disable
    [MenuItem("MenuItem/Project中的按钮", true, 100)]
    private static bool isValidate_AssetsProject()
    {
        return true;
    }

    [MenuItem("MenuItem/Project中的按钮", false, 100)]
    private static bool AssetsProject()
    {
        Debug.Log("AssetsProjectFalse");
        return false;
    }

    [MenuItem("MenuItem/DeleteAllObject", true)]
    private static bool DeleteValidate()
    {
        if (Selection.objects.Length > 0)
            return true;
        else
            return false;
    }

    [MenuItem("MenuItem/DeleteAllObject %_q", false)]
    private static void DeleteAllObj()
    {
        // Selection.objects 返回场景或者Project中选择的多个对象
        foreach (var item in Selection.objects)
        {
            //记录删除操作，允许撤销
            Undo.DestroyObjectImmediate(item);
        }
    }
}
