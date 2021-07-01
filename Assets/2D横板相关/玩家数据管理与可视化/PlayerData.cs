using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerData : MonoBehaviour
{   
    //玩家的各种基础数值，这里以HP为例
    public float maxHP ; 
    public float currentHP ; 
    public Image healthBar ;
    public Image healthBar_Effect ;
    public float speedOfUPdate ;   

    private void Awake()
    {
        currentHP = maxHP ; //测试时先在Awake中设置使当前=设置，之后要看情况修改
    }

    private void Update()
    {
        FreshData();
        
        //测试模块，按T减少20HP
        if(Input.GetKeyDown(KeyCode.T)){
            ChangePlayerHP(0,20);
        }
    }

    //更新数据的各个方法
    public void ChangePlayerHP(int _mode , float _change){
        //0表示减，1表示增
        if(_mode == 0) currentHP -= _change ;
        if(_mode == 1) currentHP += _change ;
    }

    
    public void FreshData(){
        //血量更新
        healthBar.fillAmount = currentHP/maxHP ;
        if(healthBar_Effect.fillAmount > healthBar.fillAmount){
            healthBar_Effect.fillAmount -= speedOfUPdate ;
        }else {
            healthBar_Effect.fillAmount = healthBar.fillAmount ;
        }
    }

}
