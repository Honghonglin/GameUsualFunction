using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    private MyData myData;
    // Start is called before the first frame update
    void Start()
    {
        myData = Resources.Load<MyData>("MyData");
        Debug.Log(myData.id);
        Debug.Log(myData.objName);
        Debug.Log(myData.value);
        Debug.Log(myData.isUsed);
    }
}
