using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New MainItem", menuName = "Bag/New MainItem")]
public class MainItem : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}