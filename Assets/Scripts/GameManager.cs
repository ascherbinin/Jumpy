using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;                                      
	public Text coinsText;
	public Text topHeightText;
	public Text currentHeightText;

	private int _coins;
	private int _height;
	private int _topHeight = 0;
	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		//Call the SetupScene function of the BoardManager script, pass it current level number.

	}

	public void AddCoin() {
		_coins++;
		coinsText.text =  _coins.ToString ("000");
	}

	public void ChangeHeight(int meters) {
		_height = meters;
		if (_topHeight < _height) {
			_topHeight = _height;
			topHeightText.text = _topHeight.ToString ("00000");
		}
		currentHeightText.text =  _height.ToString ("00000");
	}

	public void Restart() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

}
