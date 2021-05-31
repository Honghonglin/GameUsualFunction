using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDemoScript : MonoBehaviour
{
    void Start()
    {
        StaticPanelMgr.GetInstance().SetPanel("PanelCanvas");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
            StaticPanelMgr.GetInstance().LoadPanel("Panel1");
            
        if(Input.GetKeyDown(KeyCode.Keypad2))
            StaticPanelMgr.GetInstance().LoadPanel("Panel2");

        if(Input.GetKeyDown(KeyCode.Keypad3))
            StaticPanelMgr.GetInstance().LoadPanel("Panel3");

        if(Input.GetKeyDown(KeyCode.Keypad4))
            StaticPanelMgr.GetInstance().LoadPanel("Panel4");

        if(Input.GetKeyDown(KeyCode.Escape))
            StaticPanelMgr.GetInstance().LeavePanel();
    }
}
