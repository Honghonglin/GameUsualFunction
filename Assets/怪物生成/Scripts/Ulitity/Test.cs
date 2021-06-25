using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Update()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Enemy");
        if (objects.Length != 0&&Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var obj in objects)
            {
                Destroy(obj);
            }
        }
    }
}
