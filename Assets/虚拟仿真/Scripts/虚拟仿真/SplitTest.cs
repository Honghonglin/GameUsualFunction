using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 模型拆分
/// 按W 拆分(可多次) S合并到最初位置
/// </summary>
public class SplitTest : MonoBehaviour
{
    public Transform m_ParObj;//中心点
    private List<GameObject> m_Child;//所有子对象
    private List<Vector3> m_InitPoint = new List<Vector3>();//初始位置

    private void Start()
    {
        m_Child = m_ParObj.GetChild();//获取所有子对象
        for (int i = 0; i < m_Child.Count; i++)
        {
            m_InitPoint.Add(m_Child[i].transform.position);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //拆分
            SplitObject();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //合并
            MergeObject();
        }
    }

    private void SplitObject()
    {
        for (int i = 0; i < m_Child.Count; i++)
        {
            Vector3 tempV3 = SplitObjTest(m_ParObj, m_Child[i].transform);
            m_Child[i].transform.DOMove(tempV3, 3f, false);
        }
    }
    private void MergeObject()
    {
        for (int i = 0; i < m_InitPoint.Count; i++)
        {
            m_Child[i].transform.DOMove(m_InitPoint[i], 3f, false);
        }
    }

    public Vector3 SplitObjTest(Transform m_ParObj, Transform _TargetObj)
    {
        Vector3 tempV3;
        tempV3.x = (_TargetObj.position.x - m_ParObj.position.x) * 2;
        tempV3.y = (_TargetObj.position.y - m_ParObj.position.y) * 2;
        tempV3.z = (_TargetObj.position.z - m_ParObj.position.z) * 2;
        return tempV3;
    }
}
