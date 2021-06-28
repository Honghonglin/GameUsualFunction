using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 负责管理整个卡牌系统，理论上它是不销毁的，但是考虑到其会影响别的场景，就不放过来了
/// </summary>
public class CardManager :SingletonMono<CardManager>
{
    public CardBase cardDict;
    protected override void Awake()
    {
        base.Awake();
        cardDict=Resources.Load("卡牌/CardBase") as CardBase;//自动挂载数据库
        cardDict.InitializedBase();
    }
}
