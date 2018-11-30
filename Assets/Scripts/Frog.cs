using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Toukou{
    
    public class Frog : MonoBehaviour {

        private Rigidbody2D rb;
        private bool isJumping = false;
        private bool canJump = true;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float jumpPower = 500f;
        [SerializeField] private JumpJudge jumpJudge = null;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //接地時の処理を登録
            jumpJudge?.OnNoticeIsGround?.Subscribe(_ => isJumping = false).AddTo(this.gameObject);
            //初期化処理を登録
            GetComponent<Enemy>().OnNoticeCanMove?.Subscribe(_ => Init());
        }


        private void Init(){

            this.FixedUpdateAsObservable()
                .Where(_ => canJump && !isJumping)
                .Subscribe(_ =>
                {
                    Jump();
                    canJump = false;
                    isJumping = true;
                //接地するまでの間移動
                    this.FixedUpdateAsObservable()
                        .TakeWhile(_2 => isJumping)
                        .Subscribe(_2 => Move(),
                                   () => {
                    //ジャンプ後の待機処理
                                       Observable.Timer(TimeSpan.FromSeconds(Constants.FROG_WAIT_TIME))
                                                 .Subscribe(_3 => canJump = true);
                                   });
                });

            this.UpdateAsObservable()
                .Subscribe(_ => AnimationChange());
        }

        private void AnimationChange(){
            if (isJumping) animator.SetBool("isjumping", rb.velocity.y > 0.01f ? true : false);
        }

        private void Move(){
            rb.velocity = new Vector2(speed * -1f, rb.velocity.y); 
        }
                           
        private void Jump(){
            rb.AddForce(Vector2.up * jumpPower);
        }
    }
}