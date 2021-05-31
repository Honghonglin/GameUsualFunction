using System.Collections.Generic;
using UnityEngine;
/*
 *  加载panel预制件到挂载对象上,panel预制件需放在Resources文件夹下
 *  panel可以是很多东西，比如一个解密、一个成就提示框等等
 *  为了与系统UI区分，请先设置好你选择挂载这些panel的Canvas
 *  请注意，所有面板的锚点应该设置为左下角，否则无法正常显示
 */
public class StaticPanelMgr : SingletonAutoMono<StaticPanelMgr>
{
    /// <summary>
    /// 用于挂载面板的canva；
    /// 默认画布为Canvas
    /// </summary>
    private string panelCanvas = "Canvas";

    /// <summary>
    /// 已加载的面板列表
    /// </summary>
    private List<GameObject> panelList = new List<GameObject>();

    /// <summary>
    /// 当前面板
    /// </summary>
    private static GameObject currentPanel = null;
        
    /// <summary>
    /// 加载对应路径上的房间预制件，name写文件路径
    /// </summary>
    public void LoadPanel(string name)
    {
        if(currentPanel!=null)
            currentPanel.SetActive(false);
        // 检查是否已经实例化过
        foreach(GameObject panel in panelList)
        {            
            if(panel.name == name){
                panel.SetActive(true);
                // PlayerControl.GetInstance().Pause();     这里是玩家不能移动；落实到具体的移动控制脚本
                currentPanel = panel;
                return;
            }
        }
        GameObject o = (GameObject)Resources.Load(name);
        GameObject mPanelCanvas = GameObject.Find(panelCanvas); 
        o = Instantiate(o, o.transform.position, o.transform.rotation, mPanelCanvas.transform); 
        o.name = name;
        panelList.Add(o);
        currentPanel = o;
    }

    /// <summary>
    /// 离开静态面板
    /// </summary>
    public void LeavePanel()
    {
        if(currentPanel!=null) currentPanel.SetActive(false);
        // PlayerControl.GetInstance().EnableMove();     这里是玩家可以移动；落实到具体的移动控制脚本
    }

    /// <summary>
    /// 设置自定义挂载的面板
    /// </summary>
    public void SetPanel(string canvasName)
    {
        panelCanvas = canvasName;
    }
}
