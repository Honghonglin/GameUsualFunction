
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 战斗使用的卡牌，负责战斗使用的挂载与对象选择
/// </summary>
public class CardInBattle 
{
    private CardFunction function;
    /// <summary>
    /// 卡牌目标
    /// </summary>
    private List<CardCharacter> target;

    /// <summary>
    /// 外部对象设置
    /// </summary>
    /// <param name="function"></param>
    public void SetFunction(CardFunction function)
    {
        this.function=function;
    }

    /// <summary>
    /// 用于获取卡牌作用对象的方式
    /// </summary>
    public void SetTarget()
    {

    }

    /// <summary>
    /// 使用牌
    /// </summary>
    public void UseCard()
    {
        if(function==null) Debug.LogError("no function");
        else function.DoAction(target);
    }

    /// <summary>
    /// 结束牌使用
    /// </summary>
    private void OnDisable()
    {
        function=null;
        target=null;
    }

}



