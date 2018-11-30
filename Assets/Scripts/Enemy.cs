using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Toukou;
using System;

namespace Toukou{
    
    public class Enemy : MonoBehaviour {

        Subject<Unit> noticeCanMove = new Subject<Unit>();

        public IObservable<Unit> OnNoticeCanMove{ get { return noticeCanMove; }}

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => transform.position.y < Constants.DEAD_POS_Y)
                .Subscribe(_ => Destroy(gameObject));

            //一定の距離になった時登録された初期化処理を発行
            this.UpdateAsObservable()
                .TakeWhile(_ => noticeCanMove != null)
                .Where(_ => transform.position.x < Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x + Constants.ENEMY_CANMOVEPOS_X_COMPLEMENT)
                .Take(1)
                .Subscribe(_ =>
                {
                    noticeCanMove.OnNext(Unit.Default);
                    noticeCanMove.OnCompleted();
                });
        }

        //敵が死んだ時のメソッド（未使用
        public void Dead(){
            this.GetComponent<Rigidbody2D>().simulated = false;
            int x;
            do { x = UnityEngine.Random.Range(-1, 2); } while (x == 0);
            var initVec = new Vector2(x, UnityEngine.Random.Range(1, 11));
            Physics.Instance.UniformlyAcceleratedMotion(this.transform, initVec);
        }
    }
    
}