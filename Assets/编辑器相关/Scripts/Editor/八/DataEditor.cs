using System.IO;
using UnityEditor;
using UnityEngine;
//该脚本需要放在Editor文件夹中。
//可以通过方法调用来创建资源，最终得到的资源是一样的
class DataEditor
{
    public static void CreateAsset()
    {
        //生成一个实例对象
        MyData asset = ScriptableObject.CreateInstance<MyData>();
        //创建资源
        AssetDatabase.CreateAsset(asset, Path.Combine(Application.dataPath, "MyData.asset"));
        //保存资源
        AssetDatabase.SaveAssets();
    }
}

