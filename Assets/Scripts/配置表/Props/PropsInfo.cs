using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XlsWork;
using XlsWork.PropsXls;

[Serializable]
public class UnitSettings
{
    public int ID;
    public string Name;
    public int Damage;
    public int Hp;
    public int AttackSpeed;
    public string Description;

}

public class PropsInfo : MonoBehaviour
{
    public UnitSettings Settings;

    [Header("配表内ID")]
    public int InitFromID;

}