using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Toukou{
    
    public class Field : MonoBehaviour {

        [SerializeField] private GameObject enemyPopPlaces = null;
        [SerializeField] private GameObject eaglePopPlaces = null;
        private List<Vector3> enemyPopPlacesPos = new List<Vector3>();
        private List<Vector3> eaglePopPlacesPos = new List<Vector3>();

        private void Start()
        {
            //敵を湧かせる処理
            if (enemyPopPlaces != null) InstantEnemyProgress();
            //鷲を湧かせる処理
            if (eaglePopPlaces != null && EnemyManager.Instance.EaglePopNumber > 0) InstantEagleProgress();
        }

        private void InstantEnemyProgress(){
            //enemypopPlaces下のオブジェクトの位置をlistに格納
            foreach (Transform enemypopPlace in enemyPopPlaces.transform) enemyPopPlacesPos.Add(enemypopPlace.position);
            //生成する数を決める処理
            var n = EnemyManager.Instance.EnemyPopNumber + UnityEngine.Random.Range(0, 2);
            if (n > enemyPopPlacesPos?.Count()) n = enemyPopPlacesPos.Count();
            //ランダムに並べ替えてn個取り出して新しいリストに格納
            var enemylist = enemyPopPlacesPos.OrderBy(i => Guid.NewGuid()).Take(n);
            //新しいリストに格納された位置を元に敵を生成
            foreach (var instantpos in enemylist) EnemyManager.Instance.InstantEnemy(instantpos);
        }

        private void InstantEagleProgress(){
            //eaglepopPlaces下のオブジェクトの位置をlistに格納
            foreach (Transform eaglepopPlace in eaglePopPlaces.transform) eaglePopPlacesPos?.Add(eaglepopPlace.position);
            //生成する数を決める処理
            var x = EnemyManager.Instance.EaglePopNumber;
            if (x > eaglePopPlacesPos.Count()) x = eaglePopPlacesPos.Count();
            //ランダムに並べ替えてx個取り出して新しいリストに格納
            var eaglelist = eaglePopPlacesPos.OrderBy(i => Guid.NewGuid()).Take(x);
            //新しいリストに格納された位置を元に敵を生成
            foreach (var instantpos in eaglelist) EnemyManager.Instance.InstantEagle(instantpos);
        }
    }
}