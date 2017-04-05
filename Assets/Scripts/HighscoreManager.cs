using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreManager : MonoBehaviour {

	public static HighscoreManager Instance;

	public TextAsset highscoreList;

	public struct playerData {
		public string playerName;
		public int score;
	}

	public Text place1;
	public Text place2;
	public Text place3;
	public Text place4;
	public Text place5;
	public Text place6;
	public Text place7;
	public Text place8;
	public Text place9;
	public Text place10;

	// Use this for initialization
	void Start () {
		Instance = this;
		/*
		highscores = TextAssetManager.TextToList (highscoreList);

		int iterator = 0;
		for (int i = 0; i < 10; i++) {
			playerData toAdd;
			toAdd.playerName = highscores [i + iterator];
			string tempString = highscores [i + 1 + iterator];
			int tempInt;
			int.TryParse(tempString, out tempInt);
			toAdd.score = tempInt;

			finalHighscoreList[i] = toAdd;
			//Debug.Log (finalHighscoreList [i].playerName + ": " + finalHighscoreList[i].score);
			iterator++;
		}*/

		setHighscoreList (highscoreHolder.Instance.finalHighscoreList);
		unloadHighscoreList (highscoreHolder.Instance.finalHighscoreList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addHighscoreList(playerData toAdd) {
		highscoreHolder.Instance.finalHighscoreList [highscoreHolder.Instance.finalHighscoreList.Length] = toAdd;
		sortHighscoreList (highscoreHolder.Instance.finalHighscoreList);
	}

	void sortHighscoreList(playerData[] toSort) {
		for (int i = 0; i < toSort.Length; i++) {
			for (int j = 0; j < toSort.Length; j++) {
				if (toSort [i].score > toSort [j].score) {
					playerData temp;
					temp = toSort [i];
					toSort [i] = toSort [j];
					toSort [j] = temp;
				}
			}
		}

		if (toSort.Length > 10) {
			int over = toSort.Length - 10;
			for (int i = 0; i < over; i++) {
				playerData nullVal;
				nullVal.playerName = null;
				nullVal.score = 0;
				toSort [10 + i] = nullVal;
			}
		}
		for (int i = 0; i < 10; i++) {
			Debug.Log (highscoreHolder.Instance.finalHighscoreList [i].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[i].score);
		}
	}

	public void setHighscoreList(playerData[] toSet) {
		place1 = GameObject.Find ("placeOneText").GetComponent<Text>();
		place2 = GameObject.Find ("placeTwoText").GetComponent<Text>();
		place3 = GameObject.Find ("placeThreeText").GetComponent<Text>();
		place4 = GameObject.Find ("placeFourText").GetComponent<Text>();
		place5 = GameObject.Find ("placeFiveText").GetComponent<Text>();
		place6 = GameObject.Find ("placeSixText").GetComponent<Text>();
		place7 = GameObject.Find ("placeSevenText").GetComponent<Text>();
		place8 = GameObject.Find ("placeEightText").GetComponent<Text>();
		place9 = GameObject.Find ("placeNineText").GetComponent<Text>();
		place10 = GameObject.Find ("placeTenText").GetComponent<Text>();

		sortHighscoreList (toSet);
		place1.text = highscoreHolder.Instance.finalHighscoreList [0].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[0].score;
		place2.text = highscoreHolder.Instance.finalHighscoreList [1].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[1].score;
		place3.text = highscoreHolder.Instance.finalHighscoreList [2].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[2].score;
		place4.text = highscoreHolder.Instance.finalHighscoreList [3].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[3].score;
		place5.text = highscoreHolder.Instance.finalHighscoreList [4].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[4].score;
		place6.text = highscoreHolder.Instance.finalHighscoreList [5].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[5].score;
		place7.text = highscoreHolder.Instance.finalHighscoreList [6].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[6].score;
		place8.text = highscoreHolder.Instance.finalHighscoreList [7].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[7].score;
		place9.text = highscoreHolder.Instance.finalHighscoreList [8].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[8].score;
		place10.text = highscoreHolder.Instance.finalHighscoreList [9].playerName + ": " + highscoreHolder.Instance.finalHighscoreList[9].score;
	}

	public void unloadHighscoreList(playerData[] unload) {
		int iterator = 0;
		for (int i = 0; i < 10; i++) {
			highscoreHolder.Instance.highscores [i + iterator] = highscoreHolder.Instance.finalHighscoreList[i].playerName;
			highscoreHolder.Instance.highscores [i + 1 + iterator] = highscoreHolder.Instance.finalHighscoreList[i].score.ToString();
			iterator++;
		}
	}
}
