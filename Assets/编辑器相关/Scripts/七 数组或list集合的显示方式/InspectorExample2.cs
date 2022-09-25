using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//通过PropertyField简单显示数组或者集合
public class InspectorExample2 : MonoBehaviour
{
    //序列化
    [SerializeField]
    public int[] intArray;
    [SerializeField]
    public List<string> stringList;
}
