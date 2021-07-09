using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement ; 

//房间生成脚本
public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up,down,left,right }  ;
    public Direction direction ; 

    [Header("房间信息")] 
    public int RoomTypeNum ;
    public GameObject RoomPrefab ;
    public GameObject Exit ;
    public int RoomNumber ; 
    public int MaxStep ; //最远房间距离
    //public GameObject PlayerAppear ; //玩家生成点 

    //存储房间的List
    public List<Room> Rooms = new List<Room>() ; 

    //定义一些特定的房间，来筛选终点房间
    List<GameObject> FarRooms = new List<GameObject>(); // 最远的
    List<GameObject> LessFarRooms = new List<GameObject>() ; //最远的-1
    List<GameObject> OneWayRoom = new List<GameObject>() ; //只有单独入口的房间

    //墙壁与房间的类别
    public WallType wallType;
    public RoomType roomType ;

    //最后一个房间
    private GameObject EndRoom ; 
    //生成开始与结束时的房间的颜色，用于区分二者与其它房间
    public Color StratColor , EndColor ; 
    
    [Header("位置控制")] 
    //用于控制随机生成的房间的的位置
    public Transform GeneratorPoint ; //初始点坐标

    //定义两个浮点型的位移
    public float XOffset , YOffset ; 
    public LayerMask RoomLayer ;  //房间视图的Layer

    void Start()
    {
        //For循环配上Instantiate 来生成房间
        for (int i = 0; i < RoomNumber; i++)
        {
            RoomTypeNum = Random.Range(1,7);
            if(i == 0 ){
                //第一间（生成初始房）是固定的
                Rooms.Add(Instantiate(roomType.Room0_Start,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                Instantiate(PlayerAppear,Rooms[0].transform.position,Quaternion.identity);
            }else{
                switch(RoomTypeNum)
                {
                    //依据房间类型的随机值，创建房间

                    case 1 : 
                    Rooms.Add(Instantiate(roomType.Room1,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;

                    case 2 : 
                    Rooms.Add(Instantiate(roomType.Room2,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;
                    
                    case 3 : 
                    Rooms.Add(Instantiate(roomType.Room3,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;

                    case 4 : 
                    Rooms.Add(Instantiate(roomType.Room4,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;

                    case 5 : 
                    Rooms.Add(Instantiate(roomType.Room5,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;

                    case 6 : 
                    Rooms.Add(Instantiate(roomType.Room6,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;

                    case 7 : 
                    Rooms.Add(Instantiate(roomType.Room7,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    break ;


                    default : 
                    Rooms.Add(Instantiate(RoomPrefab,GeneratorPoint.position, Quaternion.identity).GetComponent<Room>() );
                    Debug.Log("Find a RoomRange ERROR");
                    break;

                }
            }           

           //每次生成我们都需要改变一下Points 的位置
            ChangePointPos() ;
        }

        EndRoom = Rooms[0].gameObject ;

        foreach (var room in Rooms)
        {//遍历在Rooms列表里面的所有房间
             SetUpRoom(room,room.transform.position);
        }
        //!!要先初始化房间再找EndRoom，不然MaxStep会出错

        FindEndRoom() ; //寻找最后的房间
       
        //特定房间的处理
        //这里就让EndRoom生成一个出口
        //Instantiate(Exit,EndRoom.transform.position,Quaternion.identity);
    }


    public void ChangePointPos()
    {
        do
        {
            //得到随机值，利用(Direction)将int 转换为对应的枚举
            direction = (Direction)Random.Range(0,4) ; 

        switch (direction)
        {
            //随机到不同方向时候的改变
            case Direction.up :
            GeneratorPoint.position += new Vector3(0,YOffset,0);
            break;

            case Direction.down :
            GeneratorPoint.position += new Vector3(0,-YOffset,0);
            break;

            case Direction.left :
            GeneratorPoint.position += new Vector3(-XOffset,0,0);
            break;

            case Direction.right :
            GeneratorPoint.position += new Vector3(XOffset,0,0);
            break;            
            
        }
     }while (Physics2D.OverlapCircle(GeneratorPoint.position,0.2f,RoomLayer)) ;
    }

    //判断四周有没有房间的方法，以此来得到通道与门
    public void SetUpRoom(Room NewRoom , Vector3 RoomPosition)
    {
        NewRoom.RoomUp = Physics2D.OverlapCircle(RoomPosition + new Vector3(0,YOffset,0),0.2f,RoomLayer);
        NewRoom.RoomDown = Physics2D.OverlapCircle(RoomPosition + new Vector3(0,-YOffset,0),0.2f,RoomLayer);
        NewRoom.RoomLeft = Physics2D.OverlapCircle(RoomPosition + new Vector3(-XOffset,0,0),0.2f,RoomLayer);
        NewRoom.RoomRight = Physics2D.OverlapCircle(RoomPosition + new Vector3(XOffset,0,0),0.2f,RoomLayer);
        
        NewRoom.UpdateRoom(XOffset,YOffset);

        switch(NewRoom.DoorNum)
        {   
            //利用Switch + DoorNum来区分各种情况
            case 1 :
                  if(NewRoom.RoomUp) Instantiate(wallType.singleUp,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomDown) Instantiate(wallType.singleBottom,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomLeft) Instantiate(wallType.singleLeft,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomRight) Instantiate(wallType.singleRight,RoomPosition,Quaternion.identity);
                  break ; 
            case 2 :
                  if(NewRoom.RoomLeft && NewRoom.RoomUp)    Instantiate(wallType.doubleLU,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomLeft && NewRoom.RoomRight) Instantiate(wallType.doubleLR,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomLeft && NewRoom.RoomDown)  Instantiate(wallType.doubleLB,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomUp && NewRoom.RoomRight)   Instantiate(wallType.doubleUR,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomUp && NewRoom.RoomDown)    Instantiate(wallType.doubleUB,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomRight && NewRoom.RoomDown) Instantiate(wallType.doubleRB,RoomPosition,Quaternion.identity);
                  break ; 
            case 3 :
                  if(NewRoom.RoomLeft && NewRoom.RoomUp && NewRoom.RoomRight)    Instantiate(wallType.tripleLUR,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomDown && NewRoom.RoomLeft && NewRoom.RoomRight)  Instantiate(wallType.tripleLRB,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomDown && NewRoom.RoomUp && NewRoom.RoomRight)    Instantiate(wallType.tripleURB,RoomPosition,Quaternion.identity);
                  if(NewRoom.RoomLeft && NewRoom.RoomUp && NewRoom.RoomDown)     Instantiate(wallType.tripleLUB,RoomPosition,Quaternion.identity);
                  break ;
            case 4 :
                  if(NewRoom.RoomLeft && NewRoom.RoomUp && NewRoom.RoomRight && NewRoom.RoomDown)  Instantiate(wallType.fourDoors,RoomPosition,Quaternion.identity);
                  break  ;
        }
    }

    public void  FindEndRoom()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            //首先，得到曼哈顿距离上最远的房间
            if (Rooms[i].StepToStart > MaxStep)
            {        
                MaxStep = Rooms[i].StepToStart ; 
            }
        }    

            foreach (var room in Rooms)
            {
                //添加符合条件的房间(最远和次远)
                if(room.StepToStart == MaxStep) FarRooms.Add(room.gameObject);
                if(room.StepToStart == MaxStep -1) LessFarRooms.Add(room.gameObject) ;
            }

            for (int i = 0; i < FarRooms.Count; i++)
            {
                if(FarRooms[i].GetComponent<Room>().DoorNum == 1)    OneWayRoom.Add(FarRooms[i]);
            }

            for (int i = 0; i < LessFarRooms.Count; i++)
            {
                if(LessFarRooms[i].GetComponent<Room>().DoorNum == 1)   OneWayRoom.Add(LessFarRooms[i]);
            } 

            if(OneWayRoom.Count !=0){
                //如果单一门房间存在，那么从中选择一个作为EndRoom
                EndRoom = OneWayRoom[Random.Range(0,OneWayRoom.Count)] ; 
            }else{
                EndRoom = FarRooms[Random.Range(0,FarRooms.Count)] ;
            }    
    }

    [System.Serializable]
    public class WallType {
        //Wall类型的Class

        public GameObject singleLeft,singleRight,singleUp,singleBottom,
                          doubleLU,doubleLR,doubleLB,doubleUR,doubleUB,doubleRB,
                          tripleLUR,tripleLUB,tripleURB,tripleLRB,
                          fourDoors;

    }

    [System.Serializable]
    public class RoomType{
        //同墙壁，我们也预制几个不同的房间。此时我们也创建一个RoomType类型的Class
        public GameObject Room0_Start ,Room1,Room2,Room3,Room4,Room5,Room6,Room7,Room99_End;
    }

}
