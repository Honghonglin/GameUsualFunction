using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//拓展：实现场景中一直显示主摄像机的视野范围
public class MainCameraGizmos : MonoBehaviour
{
    private Camera mainCamera;

    private void OnDrawGizmos()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        Gizmos.color = Color.green;
        //设置gizmos的矩阵   
        Gizmos.matrix = Matrix4x4.TRS(mainCamera.transform.position, mainCamera.transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, mainCamera.fieldOfView, mainCamera.farClipPlane, mainCamera.nearClipPlane, mainCamera.aspect);
    }
}
