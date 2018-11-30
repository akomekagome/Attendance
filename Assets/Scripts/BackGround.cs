using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toukou;
using UniRx;
using UniRx.Triggers;


namespace Toukou{
    
    public class BackGround : MonoBehaviour {

        private Vector3 tmp;

        private void Start()
        {
            tmp = transform.position;
            this.LateUpdateAsObservable()
                .Subscribe(_ => Move());
        }

        public void Move(){
            tmp.x = Camera.main.transform.position.x;
            transform.position = tmp;
        }
    }
}
