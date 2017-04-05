using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuHandler : MonoBehaviour {

	public string levelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadLevel() {
		SceneManager.LoadScene(levelName);
		Time.timeScale = 1;
	}

	public void Exit() {
		Time.timeScale = 1;
		HighscoreManager.Instance.unloadHighscoreList (highscoreHolder.Instance.finalHighscoreList);
		TextAssetManager.ListToText (highscoreHolder.Instance.highscores);
		Application.Quit ();
	}
}
