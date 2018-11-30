using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Toukou;

public class MainCamera : MonoBehaviour {

    private float startPos_y;

    private void Awake()
    {
        startPos_y = transform.position.y;
        //プレイヤーの位置を送ってもらった時の処理を登録しています
        FindObjectOfType<Player>()?.OnSendPos.Subscribe(v => CameraMove(v));
    }

    private void CameraMove(Vector3 pos){
        //プレイヤーが一定の距離を超えた時にカメラを移動
        if (pos.x > transform.position.x - Constants.CAMERA_CAN_MOVEPOS_X_COMPLEMENT) MoveX(pos.x);
        //プレイヤーの位置が一定の高さ以上になった時にカメラを移動
        if (pos.y > Constants.CAMERA_CAN_MOVEPOS_Y) MoveY(pos.y);
    }
    
    public void MoveX(float x)
    {
        var tmp = transform.position;
        tmp.x = x + Constants.CAMERA_CAN_MOVEPOS_X_COMPLEMENT;
        transform.position = tmp;
    }

    public void MoveY(float y)
    {
        var tmp = Camera.main.transform.position;
        tmp.y = y - Constants.CAMERA_CAN_MOVEPOS_Y + startPos_y;
        Camera.main.transform.position = tmp;
    }
}
