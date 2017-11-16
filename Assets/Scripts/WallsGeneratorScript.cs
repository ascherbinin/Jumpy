using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsGeneratorScript : MonoBehaviour {


	private float _lastYWallPosition = 5.3F;
	public GameObject playerGO;
	public GameObject wall;
	// Use this for initialization
	void Start () {
		Instantiate (wall, new Vector2(gameObject.transform.position.x, _lastYWallPosition), Quaternion.identity);
		for (int i = 0; i < 3; i++) {
			GenerateWall ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (_lastYWallPosition - playerGO.transform.position.y  < 10) {
			GenerateWall ();
		}
	}

	void GenerateWall() {
		var newLastPositionY = _lastYWallPosition + 11.52F;
		Instantiate (wall, new Vector2(gameObject.transform.position.x, newLastPositionY), Quaternion.identity);
		_lastYWallPosition = newLastPositionY;
	}
}
