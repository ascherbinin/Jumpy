using System.Collections;
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
			goRB.AddForce (new Vector2 (_collider.offset.x - goCollider.offset.x, _collider.offset.y - goCollider.offset.y).normalized * 100);
			Debug.Log("Touch player");
//			Destroy(gameObject);
		}
	}
}
