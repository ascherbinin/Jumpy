using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPulse : MonoBehaviour {


	public Color toColor = Color.white;
	public float timeToPulse = 1;

	private Color _startColor;
	private Image _image;

	void Start() {
		_image = gameObject.GetComponent<Image> ();
		_startColor = _image.color;
	}

	void Update() {
		var  lerpedColor = Color.Lerp(_startColor, toColor, Mathf.PingPong(Time.time, timeToPulse));
		_image.color = lerpedColor;
	}
}
