using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// 该方法需要将该类放在Editor文件夹内，使用特性的方法可以将业务逻辑和调试脚本分开。
//由于其针对的是编辑器组件的方法，需要设置为Static方法。
public class GizmosTest
{
    //表示物体显示并且被选择的时候，绘制Gizmos
    [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
    //第一个参数需要指定目标类，目标类需要挂载在场景对象中
    private static void MyCustomOnDrawGizmos(TargetExample target, GizmoType gizmoType)
    {
        var color = Gizmos.color;
        Gizmos.color = Color.white;
        //target为挂载该组件的对象
        Gizmos.DrawCube(target.transform.position, Vector3.one);
        Gizmos.color = color;
    }
}
