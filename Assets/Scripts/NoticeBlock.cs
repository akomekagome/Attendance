using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Toukou;

namespace Toukou{
    
    public class NoticeBlock : MonoBehaviour {
        
        void Start () {
            
            //一定距離になった時次のステージを生成するメソッドを呼び出しています
            this.UpdateAsObservable()
                .Where(_ => transform.position.x < Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + Constants.NOTICE_POS_X_COMPLEMENT)
                .Take(1).Subscribe(_ => transform.root.GetComponent<FieldManager>()?.CreateStage());
        }
    }
}
