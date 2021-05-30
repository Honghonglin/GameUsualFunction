using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesItem : MonoBehaviour
{
    //物体的数据仓库
    public Item item;
    //背包的数据仓库
    //public MainItem mainItem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (!BagController.Controller.mainItem.itemList.Contains(item))
            {
                item.AddEventListener(BagController.insertItemToUI);
            }
            item.ItemAdd();
            Destroy(this.gameObject);
        }
    }
}
