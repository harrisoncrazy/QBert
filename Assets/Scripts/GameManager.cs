using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	private bool isPaused = false;
	private GameObject pauseScreen;

	public int totalLives;

	public int totalConvertedTiles = 0;
	public int totalScore = 0;
	private int levelNum = 1;

	//ending values
	public bool inputDelay = true;

	public bool isFalling = false;
	public bool isEnding = false;
	public bool isWinning = false;
	public bool isGameOver = false;


	//Audio Files
	public AudioClip QBertjump;
	public AudioClip Balljump;
	public AudioClip QBertfall;
	public AudioClip endMusic;
	public AudioClip teleportMusic;
	public AudioClip startMusic;
	public AudioClip snakeJump;
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

		totalLives = 2;

		GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		player.GetComponent<playerHandler> ().currentTile = 5;
		player.GetComponent<playerHandler> ().currentRow = 3;
		player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));
		currentPlayer = player;

		pauseScreen = GameObject.Find ("PauseMenu");
		pauseScreen.SetActive (false);
	}

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (totalConvertedTiles == 28) { //ending, filling in all tiles
			if (isWinning == false) {
				isEnding = true;
				isWinning = true;
				GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.endMusic, 0.7f);
				totalScore += 1000;

				for (int i = 0; i <= TileListCheck.Instance.teleporterTiles.Length - 1; i++) {
					if (TileListCheck.Instance.teleporterTiles [i] != 0) {
						totalScore += 100;
					}
				}

				StartCoroutine ("WinGame");
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused == false) {
				isPaused = true;
				pauseScreen.SetActive (true);
				Time.timeScale = 0;
			} else {
				isPaused = false;
				pauseScreen.SetActive (false);
				Time.timeScale = 1;
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
		if (totalLives > 0) {
			totalLives--;
			GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
			player.GetComponent<playerHandler> ().currentTile = 5;
			player.GetComponent<playerHandler> ().currentRow = 3;
			player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));
			currentPlayer = player;
			isEnding = false;
			isFalling = false;
		} else if (totalLives < 0) {
			isGameOver = true;
		}
		//SceneManager.LoadScene ("level1");
	}

	IEnumerator WinGame() {
		yield return new WaitForSeconds (3.0f);
		levelNum++;
		SceneManager.LoadScene ("level" + levelNum);
		totalConvertedTiles = 0;
		isWinning = false;
		isEnding = false;
		isFalling = false;

		StartCoroutine ("SpawnPlayer");
		/*
		GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		player.GetComponent<playerHandler> ().currentTile = 5;
		player.GetComponent<playerHandler> ().currentRow = 3;
		player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));
		*/
	}

	IEnumerator SpawnPlayer() {
		yield return new WaitForSeconds (0.5f);

		pauseScreen = GameObject.Find ("PauseMenu");
		pauseScreen.SetActive (false);

		GameObject player = Instantiate (playerPrefab, transform.position, Quaternion.identity);
		player.GetComponent<playerHandler> ().currentTile = 5;
		player.GetComponent<playerHandler> ().currentRow = 3;
		player.transform.position = new Vector3 (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.x, (GameObject.Find ("tile" + player.GetComponent<playerHandler> ().currentTile + "Base").transform.position.y + 0.15f));
		currentPlayer = player;
		isEnding = false;
	}
}
