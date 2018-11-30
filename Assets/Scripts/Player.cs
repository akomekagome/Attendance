using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Toukou{

    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        private AudioSource audioSource;
        private bool isInvincible =false;
        private float initVecmagnitude = 10;

        private Subject<float> sendScore = new Subject<float>();
        private Subject<Vector3> sendPos = new Subject<Vector3>();

        public IObservable<float> OnSendScore{ get { return sendScore; } }
        public IObservable<Vector3> OnSendPos{ get { return sendPos; }}

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            GameManager.Instance.OnInitClass.Subscribe(_ => Init());
        }

        private void Init()
        {
            var startpos = transform.position.x;

            this.UpdateAsObservable()
                .Where(_ => transform.position.y < Constants.DEAD_POS_Y)
                .Subscribe(_ =>
                {
                    Destroy(gameObject);
                //ゲーム終了を通知
                    GameManager.Instance?.End();
                });
            
            this.OnCollisionEnter2DAsObservable()
                .Where(c => c.gameObject.tag == "Enemy")
                .Subscribe(c => HitedEnemyProgress(c));

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                //発行
                    sendScore?.OnNext(transform.position.x - startpos);
                    sendPos?.OnNext(transform.position);
                });
            
        }

        private void HitedEnemyProgress(Collision2D c){
            //無敵状態の時（未実装
            if (isInvincible)c.gameObject.GetComponent<Enemy>()?.Dead();
            else Dead();
        }

        public void Dead(){
            rb.simulated = false;
            animator.Play("Dead", 0, 0.0f);
            audioSource.PlayOneShot(AudioClips.Instance.HitSE);
            var initVec = new Vector2(1, 10).normalized * initVecmagnitude;
            //等加速度運動
            Physics.Instance.UniformlyAcceleratedMotion(this.transform, initVec);
        }
    }
}