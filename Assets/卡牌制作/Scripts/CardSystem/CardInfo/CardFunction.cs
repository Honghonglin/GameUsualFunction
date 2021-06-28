using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象类，用于写有具体功能的卡牌，支持魔改
/// </summary>
public abstract class CardFunction
{
    protected List<CardCharacter> target;
    /// <summary>
    /// 这个是为了通用牌获得基础数值
    /// </summary>
    protected CardFrame frame;

    public void SetFrame(CardFrame cardFrame)
    {
        frame=cardFrame; 
    }

    /// <summary>
    /// 卡牌具体的功能，其子类可以自定义
    /// </summary>
    protected abstract void CardAction();

    /// <summary>
    /// 对target使用卡牌
    /// </summary>
    /// <param name="target"></param>
    public void DoAction(List<CardCharacter> target)
    {
        this.target=target;
        CardAction();
    }

}
