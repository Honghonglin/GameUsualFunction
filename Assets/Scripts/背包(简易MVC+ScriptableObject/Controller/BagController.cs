using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagController : MonoBehaviour
{
    //单例模式
    static BagController bagController;

    public static BagController Controller => bagController;

    private void Awake()
    {
        if (bagController != null)
        {
            Destroy(this);

        }
        bagController = this;
        Init();
        gameObject.SetActive(false);
    }
    //每次游戏启动前，动态的更新背包UI元素
    private void OnEnable()
    {
        //updateItemToUI();
    }
    //背包数据仓库、格子中物体预制体、和UI中显示物体元素的父元素 仅仅是为了方便查看
    public MainItem mainItem;

    public Dictionary<string, Grid> itemDic = new Dictionary<string, Grid>();

    public Grid gridPrefab;


    public GameObject myBag;

    /// <summary>
    /// 在UI中将一个物体的数据仓库显示出来
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item item)
    {
        Grid grid;
        if (!bagController.mainItem.itemList.Contains(item))
        {
            bagController.mainItem.itemList.Add(item);
            grid = Instantiate(bagController.gridPrefab, bagController.myBag.transform);
            bagController.itemDic.Add(item.itemName, grid);
        }
        else
        {
            grid = bagController.itemDic[item.itemName];
        }
        grid.UpdateInfo(item);
    }

    private void OnDestroy()
    {
        foreach (var item in mainItem.itemList)
        {
            item.RemoveEventListener(insertItemToUI);
        }
    }

    /// <summary>
    /// 将背包数据仓库中所有物体显示在UI上
    /// </summary>
    public static void Init()
    {
        foreach (var item in bagController.mainItem.itemList)
        {
            Grid grid = Instantiate(bagController.gridPrefab, bagController.myBag.transform);
            bagController.itemDic.Add(item.itemName, grid);
            item.AddEventListener(insertItemToUI);
            grid.UpdateInfo(item);
        }
    }



}
