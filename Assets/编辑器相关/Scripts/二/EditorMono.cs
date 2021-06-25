using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
//多个特性可以用逗号隔开，例如：[SerializeField, Range(0,5)]
[RequireComponent(typeof(Transform))] //挂载该类的对象，必须要有Animator组件
[DisallowMultipleComponent] //不允许挂载多个该类或其子类
[ExecuteInEditMode] //允许脚本在编辑器未运行的情况下运行
[CanEditMultipleObjects] //允许当选择多个挂有该脚本的对象时，统一修改值
[AddComponentMenu("Custom/EditorMono")] //可以在菜单栏Component内添加组件按钮
public class EditorMono : MonoBehaviour
{
    #region 这两个特性是在UnityEngine命名空间下的，而不像其他[MenuItem]、Selection是在UnityEditor下的。
    //ContextMenu  //组件右键菜单按钮
    //给某组件添加右边小齿轮菜单选项
    [ContextMenu("FunctionName")]
    private void FunctionName()
    {
        Debug.Log("FunctionName");
    }
    //ContextMenuItem  //定义属性的右键菜单
    //给某属性添加右键菜单选项
    [ContextMenuItem("Handle", "HandleHealth")]
    public float health;
    private void HandleHealth()
    {
        Debug.Log("HandleHealth");
    }
    #endregion


    #region 常用的属性特性
    [Range(0,100)]////限制数值范围
    public int value1;

    [Multiline(3)] //字符串多行显示
    public string str1;
    //The minimum amount of lines the text area will use.
    //文本区域在开始使用滚动条之前可以显示的最大行数。
    [TextArea(2, 4)] //文本输入框
    public string str2;

    [SerializeField] //序列化字段，主要用于序列化私有字段
    private int seri;

    [NonSerialized] //反序列化一个变量，并且在Inspector上隐藏
    public int seri1;

    [HideInInspector] //public变量在Inspector面板隐藏
    public int value2;

    [FormerlySerializedAs("value3")] //当变量名发生改变时，可以保存原来value3的值  修改value3变量名，value3在编辑器的值不变
    public int value3;

    [Header("Header Name")] //加粗效果的标题
    public string head;

    [Space(10)] //表示间隔空间，数字越大，间隔越大
    public string space;

    [Tooltip("提示信息")] //显示字段的提示信息
    public string message;

    [ColorUsage(true)] //显示颜色面板
    public Color color;
    
    #endregion
}

public class EditorMonoGizmoDrawer
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]//用于Gizmos渲染，将逻辑与调试代码分离
    static void DrawGizmoForEditorMono(EditorMono scr, GizmoType gizmoType)
    {
        Vector3 position = scr.transform.position;

        if (Vector3.Distance(position, Camera.current.transform.position) > 10f)
            Gizmos.DrawCube(position,Vector3.one);
    }
}