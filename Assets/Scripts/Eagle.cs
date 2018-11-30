using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Toukou;

public class Eagle : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed_x = 5f;
    private float speed_y = 6f;
    private PlayerMove playerMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMove = FindObjectOfType<PlayerMove>();
        //プレイヤーとの距離が一定の距離になった時の処理を登録しています
        FindObjectOfType<Player>()?.OnSendPos?
                                   .Where(v => (transform.position.x - v.x) / (speed_x + playerMove.Speed) <= 1.5f)
                                   .Take(1).Subscribe(v => Move(v)).AddTo(gameObject);
    }

    private void Move(Vector3 pos){

        this.FixedUpdateAsObservable()
            //プレイヤーの高さになるまでy軸の移動
            .TakeWhile(_ => transform.position.y >= pos.y + Constants.EAGLE_MOVELIMIT_Y_COMPLEMENT)
            .Subscribe(_ => rb.velocity = new Vector2(-speed_x, -speed_y),
                       () => {
            //x軸のみの移動
                           this.FixedUpdateAsObservable()
                               .Subscribe(_ => rb.velocity = new Vector2(-speed_x, 0));
                       });
                    
    }
}
