using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformHandler : MonoBehaviour {

	public int platformNum;
	public bool leftPlatform;

	private Transform topTile;

	public float delay = 0.25f;

	// Use this for initialization
	void Start () {
		topTile = GameObject.Find ("tile" + 1 + "Base").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerHandler.Instance.movementTest.teleporterNumber == platformNum) {
			if (playerHandler.Instance.startingTeleport == true) {
				if (leftPlatform == true) {
					if (delay > 0) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x - 0.25f, transform.position.y + .25f), .8f * Time.deltaTime); //moving out
						delay -= Time.deltaTime;
					} else {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3 (topTile.position.x, topTile.position.y + 0.5f), .8f * Time.deltaTime); //moving up to above the top tile
						if (transform.position.y == topTile.position.y + 0.5f) {
							playerHandler.Instance.stoppedTeleport = true;
						}
					}
				}
				if (leftPlatform != true) {
					if (delay > 0) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x + 0.25f, transform.position.y + .25f), .8f * Time.deltaTime); //moving out
						delay -= Time.deltaTime;
					} else {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3 (topTile.position.x, topTile.position.y + 0.5f), .8f * Time.deltaTime); //moving up to above the top tile
						if (transform.position.y == topTile.position.y + 0.5f) {
							playerHandler.Instance.stoppedTeleport = true;
						}
					}
				}
			}
		}
	}
}
