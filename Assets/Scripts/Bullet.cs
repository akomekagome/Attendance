using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start () {
        this.UpdateAsObservable()
            .Subscribe(_ => rb.velocity = Vector3.left * speed);
        
        this.OnCollisionEnter2DAsObservable()
            .Where(c => c.gameObject.GetComponent<Cannon>() == null)
            .Subscribe(_ => Destroy(gameObject));
	}
}
