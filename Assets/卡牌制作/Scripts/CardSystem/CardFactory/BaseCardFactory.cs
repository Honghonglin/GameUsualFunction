using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础的卡牌工厂，用于更换卡牌外观
/// </summary>
public class BaseCardFactory : SingletonMono<BaseCardFactory>
{
    [SerializeReference]protected CardBase cardDic;

    public virtual void MakeCard(GameObject card,int id)
    {
        card.GetComponent<CardOutLook>().SetCardInfo(cardDic.GetCard(id));
    }

}
