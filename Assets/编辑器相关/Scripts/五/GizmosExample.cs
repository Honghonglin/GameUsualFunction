using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//OnDrawGizmos在每一帧都会被调用，其渲染的Gizmos是一直可见的
//而OnDrawGizmosSelected是当物体被选中的时候才会显示。
public class GizmosExample : MonoBehaviour
{
    //绘制效果一直显示
    private void OnDrawGizmos()
    {
        //Gizmos.color作为全局的静态变量
        //为了防止这里的color修改会对其他地方的绘制造成影响
        //所以在绘制完Gizmos的时候，将Gizmos.color修改为原先的值。
        var color = Gizmos.color;
        Gizmos.color = Color.white;
        Gizmos.DrawCube(transform.position, Vector3.one);
        Gizmos.color = color;
    }
    //绘制效果在选中对象时显示
    private void OnDrawGizmosSelected()
    {
        var color = Gizmos.color;

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        


        Gizmos.color = Color.red;
        var direction = transform.TransformDirection(Vector3.forward) * 5;  //    在物体的前方绘制一个5米长的线
        Gizmos.DrawRay(transform.position, direction);


        Gizmos.color = color;
    }
}