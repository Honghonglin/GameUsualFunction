using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于演示卡牌效果的
/// </summary>
public class TestForCard : MonoBehaviour
{
    int id=0;
    public int max;
    public GameObject card;
    public BaseCardFactory factory;

    void Start()
    {
        factory=BaseCardFactory.GetInstance();
        factory.MakeCard(card,id);
    }

    public void LastCard()
    {
        if(id==0) id=max;
        else id--;
        factory.MakeCard(card,id);
    }

    public void NextCard()
    {
        if(id==max) id=0;
        else id++;
        factory.MakeCard(card,id);
    }
}
