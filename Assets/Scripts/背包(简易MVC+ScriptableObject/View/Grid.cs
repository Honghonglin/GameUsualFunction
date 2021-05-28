using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//ScriptableObject可以用来创建不需要挂载到场景物体上的脚本，
//这样可以将游戏数据保存于本地，不会在每次游戏结束时重置数据，我们可以利用这一点来设计出数据的存储仓库
public class Grid : MonoBehaviour
{
    public Image gridImage;
    public Text girdNum;
}
