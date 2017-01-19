using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public int totalConvertedTiles = 0;

	//ending values
	public bool inputDelay = true;

	public bool isFalling = false;
	public bool isEnding = false;
	public bool isWinning = false;


	//Audio Files
	public AudioClip QBertjump;
	public AudioClip Balljump;
	public AudioClip QBertfall;
	public AudioClip endMusic;
	public AudioClip teleportMusic;
	public AudioClip startMusic;
	public AudioSource audioMain;

	//Current Player Gameobject
	public GameObject playerPrefab;
	public GameObject currentPlayer;

	// Use this for initialization
	void Start () {
		Instance = this;
		audioMain = GetComponent<AudioSource> ();
		audioMain.PlayOneShot (startMusic, 1.0f);
		StartCoroutine ("StartDelay");

		GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		player.GetComponent<playerHandler> ().currentTile = 5;
		player.GetComponent<playerHandler> ().currentRow = 3;
		player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));

	}
	
	// Update is called once per frame
	void Update () {
		if (totalConvertedTiles == 28) { //ending, filling in all tiles
			if (isWinning == false) {
				isEnding = true;
				isWinning = true;
				GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.endMusic, 0.7f);
				StartCoroutine ("WinGame");
			}
		}
	}


	//Starting delay for begining the level
	IEnumerator StartDelay() {
		yield return new WaitForSeconds (2f);
		inputDelay = false;
	} 

	//Loading a new scene
	IEnumerator EndGame() {
		yield return new WaitForSeconds (3.0f);
		GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		player.GetComponent<playerHandler> ().currentTile = 5;
		player.GetComponent<playerHandler> ().currentRow = 3;
		player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));
		isEnding = false;
		isFalling = false;
		//SceneManager.LoadScene ("level1");
	}

	IEnumerator WinGame() {
		yield return new WaitForSeconds (3.0f);
		SceneManager.LoadScene ("level1");
	}
}
