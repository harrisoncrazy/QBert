using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highscoreHolder : MonoBehaviour {

	public static highscoreHolder Instance;

	public HighscoreManager.playerData[] finalHighscoreList = new HighscoreManager.playerData[11];

	public TextAsset highscoreList;

	public List<string> highscores;

	void Awake() {
		if (GameObject.FindGameObjectsWithTag("GameController").Length > 1) {
			Destroy (this.gameObject);
		} else {
			DontDestroyOnLoad (transform.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		Instance = this;

		highscores = TextAssetManager.TextToList (highscoreList);

		int iterator = 0;
		for (int i = 0; i < 10; i++) {
			HighscoreManager.playerData toAdd;
			toAdd.playerName = highscores [i + iterator];
			string tempString = highscores [i + 1 + iterator];
			int tempInt;
			int.TryParse(tempString, out tempInt);
			toAdd.score = tempInt;

			finalHighscoreList[i] = toAdd;
			//Debug.Log (finalHighscoreList [i].playerName + ": " + finalHighscoreList[i].score);
			iterator++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
