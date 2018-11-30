using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Toukou{
    
    public class Opossum : MonoBehaviour {
        
        private Rigidbody2D rb;
        private float unitVec_x = 1f;
        [SerializeField] private float speed = 4f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            //初期化処理を登録
            GetComponent<Enemy>()?.OnNoticeCanMove?.Subscribe(_ => Init());
        }

        void Init () {
            this.FixedUpdateAsObservable()
                .Subscribe(_ => Move());

            this.OnCollisionEnter2DAsObservable()
                .Where(c => c.gameObject.tag != "Ground")
                //取得を1フレーム待つ
                .ThrottleFirstFrame(1)
                .Subscribe(_ => Reverse());

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(_ => Reverse());
        }

        private void Move(){
            rb.velocity = new Vector2(-speed * unitVec_x, rb.velocity.y);
        }

        private void Reverse(){
            var tmp = transform.localScale;
            tmp.x *= -1f;
            transform.localScale = tmp;

            unitVec_x *= -1f;
        }
    }
}
