﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//定义特性
public class ShowTimeAttribute : PropertyAttribute
{
    public readonly bool ShowHour;

    //定义构造函数
    public ShowTimeAttribute(bool isShowHour = false)
    {
        ShowHour = isShowHour;
    }
}
