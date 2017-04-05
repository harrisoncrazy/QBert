using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscoreEntry : MonoBehaviour {

	public InputField entryField;
	public Text scoreDisplay;

	// Use this for initialization
	void Start () {
		scoreDisplay.text = GameManager.Instance.totalScore.ToString();
	}

	public void LockInput(InputField input) { //ending water input
		Debug.Log(input.text);
		HighscoreManager.playerData toAdd;
		toAdd.playerName = input.text;
		toAdd.score = GameManager.Instance.totalScore;
		highscoreHolder.Instance.finalHighscoreList [10] = toAdd;
	} 
}
