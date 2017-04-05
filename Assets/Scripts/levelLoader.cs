using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelLoader : MonoBehaviour {

	public string LevelToLoad;

	public void LoadLevel() {
		Application.LoadLevel (LevelToLoad);
		TextAssetManager.ListToText (highscoreHolder.Instance.highscores);
		HighscoreManager.Instance.setHighscoreList (highscoreHolder.Instance.finalHighscoreList);
		//HighscoreManager.Instance.unloadHighscoreList (HighscoreManager.Instance.finalHighscoreList);
		Time.timeScale = 1.0f;
	}

	public void LoadLevelCloseGameManager() {
		Destroy(GameObject.Find("GameManager"));

		Application.LoadLevel (LevelToLoad);
		TextAssetManager.ListToText (highscoreHolder.Instance.highscores);
		HighscoreManager.Instance.setHighscoreList (highscoreHolder.Instance.finalHighscoreList);
		//HighscoreManager.Instance.unloadHighscoreList (HighscoreManager.Instance.finalHighscoreList);
		Time.timeScale = 1.0f;
	}
}
