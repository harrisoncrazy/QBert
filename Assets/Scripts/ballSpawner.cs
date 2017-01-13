﻿using System.Collections;
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

	// Use this for initialization
	void Start () {
		realSpawnDelay = spawnDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.isEnding == false) {
			realSpawnDelay -= Time.deltaTime;

			if (realSpawnDelay <= 0) {
				int rando = Random.Range (1, 3);
				if (rando == 1) {
					redBall ball = ((GameObject)Instantiate (redBaller, leftSpawner.transform)).GetComponent<redBall> ();
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
