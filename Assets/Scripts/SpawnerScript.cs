using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject playerGO;
	private float _startY;
	public GameObject spawnCrateObject;
	public float timeToSpawn = 2F;
	public float maxX;
	public float minX;

	// Use this for initialization
	void Start () {
		_startY = transform.position.y;
		InvokeRepeating ("Spawn", 0F, timeToSpawn);
		maxX -= spawnCrateObject.transform.localScale.x;
		minX += spawnCrateObject.transform.localScale.x;
	}

	void Update () {
		transform.position = new Vector2(transform.position.x, playerGO.transform.position.y + _startY);
	}

	void Spawn() {
		var crate = Instantiate (spawnCrateObject, new Vector2(Random.Range(minX, maxX) ,gameObject.transform.position.y), Quaternion.identity);
	}
}
