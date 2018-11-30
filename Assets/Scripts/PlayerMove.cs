using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Toukou
{

    public class PlayerMove : MonoBehaviour
    {

        private Rigidbody2D rb;
        private Animator animator;
        private AudioSource audioSource;
        private float h;
        private bool canJump = false;
        [SerializeField] public float Speed { get; private set; }= 4f;
        [SerializeField] private float jumpPower = 500f;
        [SerializeField] private JumpJudge jumpJudge = null;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            //初期化処理を登録
            GameManager.Instance.OnInitClass.Subscribe(_ => Init());
            //接地時の処理を登録
            jumpJudge?.OnNoticeIsGround?.Subscribe(_ => canJump = true).AddTo(this.gameObject);
        }

        private void Init()
        {
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Space) && canJump)
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ =>
                {
                    Jump();
                    JumpSound();
                //ジャンプ中にボタンを話した時にy成分を0にしてジャンプの強弱を実装しています
                    this.UpdateAsObservable()
                        .TakeWhile(_2 => !canJump)
                        .Where(_2 => Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0.1f)
                        .AsUnitObservable()
                        .BatchFrame(0, FrameCountType.FixedUpdate)
                        .Subscribe(_2 => JumpStop());
                });
            
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    h = Input.GetAxis("Horizontal");
                    Rotaion();
                    MoveLimit();
                    AnimatorChange();
                });

            this.FixedUpdateAsObservable()
                .Subscribe(_ => Move());
        }

        private void JumpSound(){
            audioSource.PlayOneShot(AudioClips.Instance.JumpSE);
        } 

        private void JumpStop(){
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        private void Move()
        {
            rb.velocity = new Vector2(h * Speed, rb.velocity.y);
        }

        private void Jump()
        {
            canJump = false;
            rb.AddForce(Vector3.up * jumpPower);
            animator.SetTrigger("Jump");
        }

        private void Rotaion(){
            //絶対値で割って-1か1にしていますs
            if(h != 0)transform.localScale = new Vector3(h / Mathf.Abs(h), transform.localScale.y, transform.localScale.z);
        }

        private void MoveLimit(){
            var pos = transform.position;
            //ここでプレイヤーが動ける範囲を制限しています。
            pos.x = Mathf.Clamp(pos.x, Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + Constants.PLAYER_MOVELIMIT_X_COMPLEMENT,
                                Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x);
            transform.position = pos;
        }

        private void AnimatorChange(){
            animator.SetBool("Dash", h != 0 ? true : false);
            if (canJump && rb.velocity.y < -1f)animator.Play("Player_jump4", 0, 0.0f);
            animator.SetBool("isjumping", rb.velocity.y > 0.1f ? true : false);
            animator.SetBool("isfalling", rb.velocity.y < -0.1f ? true : false);
        }
    }
}