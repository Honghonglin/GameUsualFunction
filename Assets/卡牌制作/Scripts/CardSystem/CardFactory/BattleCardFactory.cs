using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCardFactory : BaseCardFactory
{
    /// <summary>
    /// 制作卡牌外观与编写卡牌功能
    /// </summary>
    /// <param name="cardID"></param>
   public override void MakeCard(GameObject card,int cardID)
   {
       base.MakeCard(card,cardID);
       ChooseCard( card, cardID);
   }

    /// <summary>
    /// 通过不同的逻辑选择卡牌的具体功能
    /// 这里是直接通过反射建立特殊卡牌
    /// 但是这里只是一个演示
    /// 基础牌一类的东西没必要写每一张都写一个新的类
    /// </summary>
    /// <param name="cardID"></param>
    /// <returns></returns>
   private void ChooseCard(GameObject card,int cardID)
   {
       CardFunction cardFunction=ChooseFunction();
       cardFunction.SetFrame(cardDic.GetCard(cardID));//设置卡牌属性
       card.GetComponent<CardInBattle>().SetFunction(cardFunction);
   }

    /// <summary>
    /// 这个方法是完全自定义的，用于指定卡牌功能，这里只是个栗子
    /// </summary>
    /// <returns></returns>
   private CardFunction ChooseFunction()
   {
       return (CardFunction)(new Attack());
   }
}
