using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MoveBlock : MonoBehaviour {

    private Rigidbody2D rb;
    [SerializeField] private float unitVec_x = 1f;
    private float speed = 3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Where(c => c.gameObject.tag == "Ground")
            //取得を一フレーム待つ
            .ThrottleFirstFrame(1)
            .Subscribe(_ => Reverse());

        this.FixedUpdateAsObservable()
            .Subscribe(_ => Move());
    }

    private void Reverse(){
        unitVec_x *= -1f;
    }

    private void Move(){
        rb.velocity = Vector3.right * unitVec_x * speed;
    }
}
