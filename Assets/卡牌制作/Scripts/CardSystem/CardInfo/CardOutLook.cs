using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 只负责改变卡牌的外观，对卡牌效果没有任何影响
/// 其能够在任何需要卡牌外观的地方使用
/// </summary>
public class CardOutLook : MonoBehaviour
{
    public int id = 0;
    public CardFrame card;
    [SerializeReference] protected Image icon;
    [SerializeReference] protected Text describe;
    [SerializeReference] protected Text cardName;
    [SerializeReference] protected Text cost;

    [SerializeReference] public string cardFunction;

    [HideInInspector] public int costValue;
    // Start is called before the first frame update
  

    /// <summary>
    /// 设置卡牌UI信息
    /// </summary>
    public void SetCardInfo(CardFrame card) 
    {
        id = card.cardID;
        icon.sprite = card.icon;
        describe.text = card.describe;
        cardName.text = card.cardName;
        cost.text = card.baseCost.ToString();
        costValue = card.baseCost;
    }

}
