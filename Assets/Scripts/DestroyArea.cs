using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class DestroyArea : MonoBehaviour {

    private Vector3 tmp;

    private void Start()
    {
        tmp = transform.position;

        this.OnTriggerEnter2DAsObservable()
            .Subscribe(c => Destroy(c.gameObject));

        this.LateUpdateAsObservable()
            .Subscribe(_ => Move());
    }

    //カメラに合わせて移動するメソッド
    public void Move()
    {
        tmp.x = Camera.main.transform.position.x;
        transform.position = tmp;
    }
}
