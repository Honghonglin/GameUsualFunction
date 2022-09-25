using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//Unity编辑器为开发者提供了类似PlayerPrefs的数据保存方式EditorPrefs。
//EditorPrefs是适用于编辑器模式，而PlayerPrefs适用于游戏运行时。
//EditorPrefs提供了四种数据的保存：int,float,string,bool
//通过Set方法保存下数据，下次则通过Get方法来获取数据，
//HasKey方法可以判断是否存在该数据的保存，删除数据调用DeleteKey方法即可。
//注意：需要谨慎调用EditorPrefs.DeleteAll()方法，
//因为该方法还可能会删除Unity编辑器自身存储的一些数据，给开发者带来不必要的麻烦。
public class WindowExample4 : EditorWindow
{
    private static WindowExample4 window;//窗体实例
    private string tempMsg;

    //显示窗体
    [MenuItem("MyWindow/Four Window")]
    private static void ShowWindow()
    {
        window = EditorWindow.GetWindow<WindowExample4>("Window Example");
        window.Show();
    }

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("TempMsg"))
        {
            tempMsg = EditorPrefs.GetString("TempMsg");
        }
    }

    private void OnGUI()
    {
        tempMsg = EditorGUILayout.TextField("Temp Msg", tempMsg);
        if (GUILayout.Button("Save"))
        {
            EditorPrefs.SetString("TempMsg", tempMsg);
        }
    }
}
