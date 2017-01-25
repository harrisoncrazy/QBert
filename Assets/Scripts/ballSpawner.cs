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

	// Use this for initialization
	void Start () {
		realSpawnDelay = spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.Instance.isEnding == false) {
			if (GameManager.Instance.isFalling == false) {
				realSpawnDelay -= Time.deltaTime;

				if (realSpawnDelay <= 0) {
					int rando = Random.Range (1, 3);
					if (rando == 1) {
						redBall ball = ((GameObject)Instantiate (redBaller, leftSpawner.transform)).GetComponent<redBall> ();

						if (isSnakeSpawned == false) {//testing to decide if its a purple ball or not
							int rando2 = Random.Range(1, 7);
							if (rando2 <= 2) {
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
							int rando2 = Random.Range(1, 7);
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
