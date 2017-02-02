using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour {

	public GameObject leftSpawner;
	public GameObject rightSpawner;

	private bool leftSpawned;

	public float spawnDelay = 2.0f;
	private float realSpawnDelay;

	public GameObject redBaller;

	private int cooldownRedCounter;

	//snake variables
	public Sprite purpleBall;
	private bool isSnakeSpawned = false;

	//Ugg and wrongway values
	public bool isWrongWay = false;
	private bool snakeWrongSpawned = false;

	public GameObject wrongwaySpawnPoint;
	public GameObject uggSpawnPoint;

	public GameObject wrongwayPrefab;
	public GameObject uggPrefab;

	// Use this for initialization
	void Start () {
		realSpawnDelay = spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (isWrongWay == true && isSnakeSpawned == true) {
			snakeWrongSpawned = true;
		}

		if (GameManager.Instance.isEnding == false) {
			if (GameManager.Instance.isFalling == false) {
				//SPAWNING UGG AND WRONGWAY
				if (isWrongWay == true) {
					realSpawnDelay -= Time.deltaTime;

					if (realSpawnDelay <= 0) {
						int rando = Random.Range (1, 3);
						if (rando == 1) {//spawning wrong way
							wrongWay way = ((GameObject)Instantiate (wrongwayPrefab, wrongwaySpawnPoint.transform)).GetComponent<wrongWay> ();
						} else if (rando == 2) {//spawning ugg
							uggWay uggWay = ((GameObject)Instantiate (uggPrefab, uggSpawnPoint.transform)).GetComponent<uggWay> ();
						}

						realSpawnDelay = spawnDelay;
					}
				}

				//SPAWNING RED BALLS AND SNAKES
				if (snakeWrongSpawned == false) {
					realSpawnDelay -= Time.deltaTime;
	
					if (realSpawnDelay <= 0) {
						int rando = Random.Range (1, 3);
						if (rando == 1) {
							redBall ball = ((GameObject)Instantiate (redBaller, leftSpawner.transform)).GetComponent<redBall> ();

							if (isSnakeSpawned == false) {//testing to decide if its a purple ball or not
								int rando2 = Random.Range (1, 5);
								if (isWrongWay == true) {//if level 3, allows spawning of snake
									rando2 = 1;
								}
								if (rando2 <= 2) {//randomly decides to spawn a snake ball
									ball.isSnake = true;
									ball.GetComponent<SpriteRenderer> ().sprite = purpleBall;
									isSnakeSpawned = true;
								}
							}

							ball.transform.position = new Vector3 (-0.15f, 0.6f);
							ball.currentTile = 2;
							ball.currentRow = 2;
							realSpawnDelay = spawnDelay;
							cooldownRedCounter++;
							if (cooldownRedCounter == 3) {
								//spawnDelay -= 0.5f;
								cooldownRedCounter = 0;
							}
						} else if (rando == 2) {
							redBall ball = ((GameObject)Instantiate (redBaller, rightSpawner.transform)).GetComponent<redBall> ();

							if (isSnakeSpawned == false) {//testing to decide if its a purple ball or not
								int rando2 = Random.Range (1, 4);
								if (isWrongWay == true) {//if level 3, allows spawning of snake
									rando2 = 1;
								}
								if (rando2 <= 2) {
									ball.isSnake = true;
									ball.GetComponent<SpriteRenderer> ().sprite = purpleBall;
									isSnakeSpawned = true;
								}
							}

							ball.transform.position = new Vector3 (0.15f, 0.6f);
							ball.currentTile = 3;
							ball.currentRow = 2;
							realSpawnDelay = spawnDelay;
							cooldownRedCounter++;
							if (cooldownRedCounter == 3) {
								//spawnDelay -= 0.5f;
								cooldownRedCounter = 0;
							}
						}
					}
				}
			}
		}
	}
}
