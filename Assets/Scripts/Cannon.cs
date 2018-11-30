using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using Toukou;

public class Cannon : MonoBehaviour {

    [SerializeField] private Bullet bullet = null;

	void Start () {
        this.UpdateAsObservable()
            .Where(_ => transform.position.x < Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).x + Constants.ENEMY_CANMOVEPOS_X_COMPLEMENT)
            .Take(1).Subscribe(_ =>
            {
            //2秒ごとに
                Observable.Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(Constants.BULLET_INTERVAL_TIME))
                          .Where(_2 => bullet != null)
                          .Subscribe(_2 => Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 180)))
                          .AddTo(this.gameObject);
            });
	}
}
