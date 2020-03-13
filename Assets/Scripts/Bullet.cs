using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	public float speed;
	private float timeDestroy;

	// Use this for initialization
	void Start () {
		timeDestroy=1.0f;

		Destroy(gameObject, timeDestroy);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.right*speed*Time.deltaTime);
	}

	void OnTriggerEnter2D(){
		Destroy (gameObject);
	}
}
