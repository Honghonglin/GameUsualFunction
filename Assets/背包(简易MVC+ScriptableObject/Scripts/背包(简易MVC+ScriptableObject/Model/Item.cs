using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="New Item",menuName = "Bag/New Item")]
public class Item : ScriptableObject
{
    //物体名、需要在UI中显示的图片、持有物体的数量、物体信息的描述
    public string itemName;
    public Sprite itemImage;
    public int itemNum;
    [TextArea] //改变输入框格式，提示输入框容量
    public string itemInfo;
    private event UnityAction<Item> updateEvent;
    public void ItemAdd()
    {
        itemNum++;
        UpdateInfo();
    }

    public void AddEventListener(UnityAction<Item> function)
    {
        updateEvent += function;
    }

    public void RemoveEventListener(UnityAction<Item> function)
    {
        updateEvent -= function;
    }


    //通知外部更新数据的方法
    private void UpdateInfo()
    {
        //找到对应的 使用数据的脚本 去更新数据
        if (updateEvent != null)
        {
            updateEvent(this);
        }
    }

}