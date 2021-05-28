using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjExtension
{
    public static List<GameObject> GetChild(this Transform obj)
    {
        List<GameObject> tempArrayobj = new List<GameObject>();
        foreach (Transform child in obj)
        {
            tempArrayobj.Add(child.gameObject);
        }
        return tempArrayobj;
    }
}
