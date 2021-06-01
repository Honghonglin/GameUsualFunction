using System.Collections;
using UnityEngine;
using System;
/*
* 敌人生成波生成器(有参考)
* 将此脚本挂载到一个对象上之后，即可自定义敌人生成波的数量、每波有多少个敌人个体等等
* 只有敌人都被击败才会开始下一波
* 关于敌人生成，实际上可以结合对象池实现复用，因为这里只是演示一下所以直接生成了
*/
public class EnemyCreater : MonoBehaviour
{
    // 生成器状态
    public enum SpawnState{
        SPAWING,WAITING,COUNTING
    }

    // 设置波数
    [System.Serializable]
    public class Wave{
        // 生成波的数量
        public int count;
        // 敌人生成间隔
        public float rate;
    }

    [Header("自定义进攻波信息")]
    public GameObject enermy;
    public Wave[] waves;                // 自定义进攻波
    private int nextWaveCount = 0;      // 下一波
    public float timeBetweenWaves = 5f; // 波之间的时间间隔，默认5s
    public float waveCount;             // 波数计数
    private float countingTime = 1;     // 搜索计时
    public SpawnState state = SpawnState.COUNTING;// 当前状态
    public static Action messionCompleted;  // 进攻完成之后执行的委托
    
    void Start(){
        waveCount = timeBetweenWaves;
        messionCompleted += TestFinished;
        // messionCompleted += ... 要在进攻完成时执行的内容
    }

    void Update()   {
        if(state == SpawnState.WAITING){
            if( !EnermyIsAlive() ){     // 在场不存在敌方物体
                WaveCompleted();        // 进攻完成
            }else{
                return;
            }
        }
        if(waveCount <=0){
            if(state != SpawnState.SPAWING){
                StartCoroutine(SpawnWave(waves[nextWaveCount]));    // 开始下一波
            }
        }else{
            waveCount -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 进攻完成
    /// </summary>
    void WaveCompleted(){
        state = SpawnState.COUNTING;
        waveCount = timeBetweenWaves;
        if(nextWaveCount + 1 > waves.Length - 1){
            messionCompleted?.Invoke();
     
        }else{
            nextWaveCount++;
        }
    }

    /// <summary>
    /// 判断场内是否还存在敌方物体
    /// </summary>
    bool EnermyIsAlive(){
        countingTime -= Time.deltaTime;
        if(countingTime <= 0){
            countingTime = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null){
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 波内敌人的生成和生成间隔
    /// </summary>
    IEnumerator SpawnWave(Wave _wave){
        state = SpawnState.SPAWING;
        for(int i = 0; i< _wave.count; i++){
            SpawnEnermy(enermy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }
        state = SpawnState.WAITING;
        yield break;
    }

    /// <summary>
    /// 生成敌人
    /// </summary>
    void SpawnEnermy(GameObject _enermy){
        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-4.5f,4.5f), _enermy.transform.position.y, UnityEngine.Random.Range(-14.5f,-5.5f));
        _enermy = Instantiate(_enermy, randomPosition, _enermy.transform.rotation); 
        _enermy.name = "enemy";
    }

    /// <summary>
    /// 获取敌人总数
    /// </summary>
    public int GetEnermyNumber(){
        int num = 0;
        for(int i = 0;i<waves.Length;i++){
            num += waves[i].count;
        }
        return num-waves.Length;
    }

    private void TestFinished(){
        Debug.Log("Finish!");
    }
}
