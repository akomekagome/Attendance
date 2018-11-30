using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using Toukou;

public class Physics : Singleton<Physics> {

    [SerializeField] private float gravityMagnification = 2f;

    //等加速度運動をするメソッド
    public void UniformlyAcceleratedMotion(Transform tf, Vector2 initVec){
        var elapsedTime = 0f;
        var moveStartPos = tf.position;

        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                if (tf == null) return;
                elapsedTime += Time.deltaTime;
                tf.position = Vector3.Lerp(tf.position
                                          , new Vector3(moveStartPos.x + initVec.x * elapsedTime,
                                                        moveStartPos.y + (initVec.y * elapsedTime) - (0.5f * (Constants.G * gravityMagnification) * elapsedTime * elapsedTime), 0f)
                                                  , 1f);
            });
    }
}
