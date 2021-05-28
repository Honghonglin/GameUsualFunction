using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    //单例模式
    static BagController bagController;
    private void Awake()
    {
        if (bagController != null)
        {
            Destroy(this);

        }
        bagController = this;
        gameObject.SetActive(false);
    }
    //每次游戏启动前，动态的更新背包UI元素
    private void OnEnable()
    {
        updateItemToUI();
    }
    //背包数据仓库、格子中物体预制体、和UI中显示物体元素的父元素
    public MainItem mainItem;
    public Grid gridPrefab;

    //这里可以存一个背包Dictionary,比较优化时间效率

    public GameObject myBag;

    /// <summary>
    /// 在UI中将一个物体的数据仓库显示出来
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item item)
    {
        Grid grid = Instantiate(bagController.gridPrefab, bagController.myBag.transform);
        grid.gridImage.sprite = item.itemImage;
        grid.girdNum.text = item.itemNum.ToString();
    }

    /// <summary>
    /// 将背包数据仓库中所有物体显示在UI上
    /// </summary>
    public static void updateItemToUI()
    {
        for (int i = 0; i < bagController.myBag.transform.childCount; i++)
        {
            Destroy(bagController.myBag.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < bagController.mainItem.itemList.Count; i++)
        {
            insertItemToUI(bagController.mainItem.itemList[i]);
        }
    }
}
