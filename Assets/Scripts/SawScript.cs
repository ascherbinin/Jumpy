﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour {

	CircleCollider2D _collider;

	// Use this for initialization
	void Start () {
		_collider = gameObject.GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Enter");
		if (other.gameObject.tag == "Player")
		{
			var goRB = other.gameObject.GetComponent<Rigidbody2D> ();
			var goCollider = other.gameObject.GetComponent<BoxCollider2D> ();
			goRB.freezeRotation = false;
			other.gameObject.GetComponent<PlatformerCharacter2D> ().isAlive = false;
			var deltaX = other.gameObject.transform.position.x - _collider.offset.x;
			var deltaY = other.gameObject.transform.position.y - _collider.offset.y;
			var forceVector = new Vector2 (-deltaX, -deltaY).normalized;
			Debug.Log (forceVector);
			goRB.AddForce (forceVector * 2, ForceMode2D.Impulse);
			goRB.AddTorque(5, ForceMode2D.Impulse);
			Debug.Log("Touch player");
//			Destroy(gameObject);
		}
	}
}
