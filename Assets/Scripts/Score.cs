using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Toukou;
using UnityEngine.UI;
using UniRx.Triggers;

namespace Toukou{
    
    public class Score : MonoBehaviour {

        private Text scoreText;
        public float score = 0; 
        private float changeLevelScore = 2f;
        private float eagleChangeScore = 4f;

        private void Awake()
        {
            var player = FindObjectOfType<Player>();
            player?.OnSendScore?.Subscribe(x => SetSocre(x)).AddTo(this);
            scoreText = GetComponent<Text>();
        }

        private void Start()
        {
            //一定の距離ごとに敵の湧く数を増やすメソッド
            this.UpdateAsObservable()
                .Where(_ => score >= changeLevelScore)
                .Subscribe(_ =>
                {
                    changeLevelScore += Constants.LEVELCHANGE_INTERVAL;
                    EnemyManager.Instance.EnemyPopLevelChange();
                });

            //一定の距離ごとに敵の湧く数を増やすメソッド
            this.UpdateAsObservable()
                .Where(_ => score >= eagleChangeScore)
                .Subscribe(_ =>
                {
                    eagleChangeScore += Constants.LEVELCHANGE_INTERVAL;
                    EnemyManager.Instance.EaglePopLevelChange();
                });
        }

        private void SetSocre(float x){
            
            score = (x / Constants.SCORE_MAGNIFIACAITON);
            if (score <= 0) return;
            scoreText.text = score.ToString("F3") + "Km";
        }
    }
}
