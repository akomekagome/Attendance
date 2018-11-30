using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace Toukou{

    public class JumpJudge : MonoBehaviour 
    {
        //接地した時に処理を登録するsubject
        private Subject<Unit> noticeIsGround = new Subject<Unit>();

        //購読側を公開
        public IObservable<Unit> OnNoticeIsGround{ get { return noticeIsGround; }}

        public void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .Where(c => c.gameObject.tag == "Ground")
                .Subscribe(_ => noticeIsGround?.OnNext(Unit.Default));
        }
    }
}