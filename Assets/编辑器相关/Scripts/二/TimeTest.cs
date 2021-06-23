using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    [ShowTime(false)]
    public int time = 3605;

    [ShowTime(true)]
    public int time1 = 3605;
}
