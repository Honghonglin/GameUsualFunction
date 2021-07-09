using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class Room : MonoBehaviour
{
    public Vector3 RoomCenter ;
    //房间中心点到边缘的距离
    public float Roomx,Roomy ;
    //对应的“门”
    public GameObject DoorLeft,DoorRight,DoorUp,DoorDown ;
    //判断四周是否有房
    public bool RoomLeft,RoomRight, RoomUp,RoomDown ;  
    //该房间到初始点的距离（计算MaxStep）
    public int StepToStart ; 
    //房间有几个门
    public int DoorNum ; 

    //其它数值与组件
    private bool HasEntered ; 
    public Text text ; 
   // public List<Transform> EnemyAppear ;
    public Transform AppperPoint;
    private int RandomNum ;
    //private int EnemySum;
    void Start()
    {
        //利用SetActive来判断是否显示门
        DoorRight.SetActive(RoomRight);
        DoorLeft.SetActive(RoomLeft);
        DoorUp.SetActive(RoomUp);
        DoorDown.SetActive(RoomDown);

        RoomCenter = this.transform.position;
        Roomx = 7.0f ;
        Roomy = 3.0f ;

        // //为敌人生成作准备
        // EnemySum = EnemyGenerator.instance.enemySum;
        // GetEnemyPoint();
    }


    //生成敌人的脚本，如果只是单单为了生成随机房间可以不要启用
    // void GetEnemyPoint(){
    //     for (int i = 0; i < EnemySum; i++)
    //     {
    //         RandomNum = Random.Range(0,EnemyAppear.Count);
    //         AppperPoint = EnemyAppear[RandomNum];
    //         EnemyGenerator.instance.CreateEnemy(AppperPoint);

    //     }

    // }

    public void UpdateRoom(float Xoffse , float Yoffse)
    {
        StepToStart = (int)(Mathf.Abs(transform.position.x / Xoffse) + Mathf.Abs(transform.position.y /Yoffse));
        text.text = StepToStart.ToString();

        //算门与累加
        if(RoomUp) DoorNum++ ; 
        if(RoomDown) DoorNum++ ; 
        if(RoomRight) DoorNum++ ; 
        if(RoomLeft) DoorNum++ ; 

    }

    
    //当玩家进入房间时刷新摄像头的方法，视情况启用
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     //如果房间检测到玩家
    //     if(other.CompareTag("Player")){
    //         CameraControl.instance.ChangeTarget(transform);

    //         if(!HasEntered){
    //         GetEnemyPoint();
    //         HasEntered = true ;
    //         }
    //     }
    // }
 
}
