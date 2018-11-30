using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toukou;
using System.Linq;

public class EnemyManager : Singleton<EnemyManager> {

    [SerializeField] private List<Enemy> enemys = new List<Enemy>();
    public int EnemyPopNumber { get; private set; } = 1;
    public int EaglePopNumber { get; private set; } = 0;

    //敵を湧かせるメソッド
    public void InstantEnemy(Vector3 instantPos){
        var enemy = Instantiate(enemys[Random.Range(0, enemys.Count() - 1)], instantPos, Quaternion.identity);
        enemy.transform.parent = this.transform; 
    }

    public void InstantEagle(Vector3 instantPos){
        var eagle = Instantiate(enemys[enemys.Count - 1], instantPos, Quaternion.identity);
        eagle.transform.parent = this.transform;
    }

    //一定距離ごとに敵が湧く数を増やすメッソド
    public void EnemyPopLevelChange(){EnemyPopNumber++;}

    public void EaglePopLevelChange(){EaglePopNumber++;}
}
