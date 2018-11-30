using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Toukou
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private Field[] Fields = null;
        //生成可能なステージの範囲
        private int canInstantRange;
        private float rangeChangeScore = 4f;
        private bool hasChanged = false;
        private Field field;
        private Score score;

        private void Awake()
        {
            score = FindObjectOfType<Score>();
            canInstantRange = Fields.Length - 3;
        }

        private void Start()
        {
            //ある程度の距離になった時生成可能なステージを増やしています
            score.ObserveEveryValueChanged(c => c.score)
                 .TakeWhile(_ => canInstantRange <= Fields.Length)
                 .Where(x => x > rangeChangeScore)
                 .Subscribe(_ =>
                 {
                     rangeChangeScore += 3f;
                     canInstantRange++;
                     hasChanged = true;
                 }).AddTo(this.gameObject);
        }

        public void CreateStage(){
            var instantPos = new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x, Constants.FIELD_CREATEPOS_y);
            //生成可能なステージが増えた時は最初必ず生成されるようにしています
            if(hasChanged){
                field = Instantiate(Fields[canInstantRange - 1], instantPos, Quaternion.identity);
                hasChanged = false;
            }else{field = Instantiate(Fields[Random.Range(0, canInstantRange)], instantPos, Quaternion.identity);}
            field.transform.parent = this.transform;
        }
    }
}
    
