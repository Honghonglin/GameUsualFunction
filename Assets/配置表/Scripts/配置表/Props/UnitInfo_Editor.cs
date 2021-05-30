using UnityEngine;
using UnityEditor;
using System;
using XlsWork;
using XlsWork.PropsXls;

[CustomEditor(typeof(PropsInfo))]//将本模块指定为UnitInfo组件的编辑器自定义模块
public class UnitInfo_Editor : Editor
{
    public override void OnInspectorGUI()//对UnitInfo在Inspector中的绘制方式进行接管
    {
        DrawDefaultInspector();//绘制常规内容

        if (GUILayout.Button("从配表ID刷新"))//添加按钮和功能——当组件上的按钮被按下时
        {
            PropsInfo propsInfo = (PropsInfo)target;
            Init(propsInfo);
        }
    }

    public void Init(PropsInfo instance)
    {
        Action init;

        var dictionary = Props.LoadExcelAsDictionary();

        if (!dictionary.ContainsKey(instance.InitFromID))
        {
            Debug.LogErrorFormat("未能在配表中找到指定的ID:{0}", instance.InitFromID);
            return;
        }
        IndividualData item = dictionary[instance.InitFromID];

        init = (() =>
        {
            instance.Settings.ID = Convert.ToInt32(item.Values[0]);
            instance.Settings.Name = Convert.ToString(item.Values[1]);
            instance.Settings.Damage = Convert.ToInt32(item.Values[2]);
            instance.Settings.Hp = Convert.ToInt32(item.Values[3]);
            instance.Settings.AttackSpeed = Convert.ToInt32(item.Values[4]);
            instance.Settings.Description = Convert.ToString(item.Values[5]);
        });

        init();
    }
}