using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主卡库，会记录所有的CardFrame类型,CardBase中卡的序列会与卡ID一致
/// </summary>
[CreateAssetMenu(fileName ="New CardBase",menuName ="CardSys/CardBase")]
public class CardBase :ScriptableObject
{
    public CardFrame[] allCards;
    public Dictionary<string,int> cardDict;

    /// <summary>
    /// 初始化字典，使其能通过卡名字获取实例，比较适合获得指定卡
    /// </summary>
    public void InitializedBase()
    {
        cardDict=new Dictionary<string, int>();
        int counter=0;
        foreach(CardFrame cardFrame in allCards)
        {
            if(cardDict.ContainsKey(cardFrame.cardName)) throw new RepeatedCardException();
            cardDict.Add(cardFrame.cardName,cardFrame.cardID);
            cardFrame.cardID=counter++;//给卡牌编号
        }
    }

    /// <summary>
    /// 根据ID获取卡实例，比较适合抽卡
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    public CardFrame GetCard(int idx) 
    {
        return allCards[idx];
    }
    
    /// <summary>
    ///根据卡名字获取卡实例 
    /// </summary>
    /// <param name="cardName"></param>
    /// <returns></returns>
    public CardFrame GetCard(string cardName)
    {
        return allCards[cardDict[cardName]];
    }

    /// <summary>
    /// 防止加入名字重复的卡
    /// </summary>
    class RepeatedCardException:Exception
    {
        public RepeatedCardException()
        {
            Debug.LogError("有命名重复的卡牌"+base.Message);
        }
    }
}
