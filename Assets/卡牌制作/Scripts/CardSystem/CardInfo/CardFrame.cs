using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 卡的基础类型，枚举类型便于直接在Hierarchy修改
/// </summary>
public enum CardType
{
    BUFF,ATTACK,HEALTH
}

/// <summary>
/// 不同的CardInstance的ID必须有不同，包含关于卡牌的一切信息，不同卡牌制作器会根据此自动生成卡牌
/// </summary>
[CreateAssetMenu(fileName = "Card", menuName = "CardSys/card")]
public class CardFrame : ScriptableObject
{
    [Header("基础属性")]
    public string cardName;
    /// <summary>
    /// 主牌库ID
    /// </summary>
    [HideInInspector]public int cardID;
    [TextArea]
    public string describe;
    public int baseCost;
    public int baseDamage;
    public CardType type;
    public Sprite icon;//卡牌图标
}

