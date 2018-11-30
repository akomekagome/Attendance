using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;
using Toukou;


public class Timer : MonoBehaviour {

    private Text timerText;
    private int minute = 10;

    private void Awake()
    {
        timerText = GetComponent<Text>();
        GameManager.Instance?.OnInitClass?.Subscribe(_ => Init());
    }

    private void Init(){
        //2秒ごとに時間を進める
        Observable.Interval(TimeSpan.FromSeconds(2))
                  .TakeWhile(_ => minute < 49)
                  .Subscribe(_ => AdvanceTime(),
                             () =>
                             {
                                 AdvanceTime();
                                 FindObjectOfType<Player>()?.Dead();
                                 FindObjectOfType<Chime>()?.PlayChilme();
                             }).AddTo(this.gameObject);
    }

    private void AdvanceTime(){
        minute++;
        timerText.text = "8:" + minute.ToString();
    }
}
