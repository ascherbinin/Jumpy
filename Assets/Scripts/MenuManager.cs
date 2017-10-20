using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text progressText;

	public void OnClickPlay() {
		StartCoroutine(LoadingScreenGame());
	}

	IEnumerator LoadingScreenGame(){
		var async = SceneManager.LoadSceneAsync ("GameScene");
		async.allowSceneActivation = false;
		while (async.isDone == false) {
			float textProgress = async.progress * 100;
			progressText.text = "Loading " + Mathf.Round(textProgress) + "%...";

			if (async.progress == 0.9f) {
				progressText.text = "Loading 100%...";
				async.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
