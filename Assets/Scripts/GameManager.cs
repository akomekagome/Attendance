using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Toukou{
    
    public class GameManager : Singleton<GameManager> {

        //初期化処理を登録するためのsubject
        private Subject<Unit> initClass = new Subject<Unit>();
        [SerializeField] private Button button = null;
        private AudioSource audioSource;

        //購読側を公開
        public IObservable<Unit> OnInitClass { get { return initClass; } }

        private void Start()
        {
            button?.gameObject.SetActive(false);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = AudioClips.Instance.Bgm;
            audioSource.Play();
            initClass.OnNext(Unit.Default);
            //解放
            initClass.OnCompleted();
        }

        public void End()
        {
            audioSource.Stop();
            button?.gameObject.SetActive(true);
            button?.OnClickAsObservable().Subscribe(_ => Retry()).AddTo(this.gameObject);
        }

        private void Retry(){
            // 現在のScene名を取得する
            Scene loadScene = SceneManager.GetActiveScene();
            // Sceneの読み直し
            SceneManager.LoadScene(loadScene.name);
        }
    }

}