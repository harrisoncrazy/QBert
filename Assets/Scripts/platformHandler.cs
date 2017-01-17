using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformHandler : MonoBehaviour {

	public int platformNum;
	public bool leftPlatform;

	public Transform topTile;

	public float delay = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.movementTest.teleporterNumber == platformNum) {
			if (playerHandler.Instance.startingTeleport == true) {
				if (leftPlatform == true) {
					if (delay > 0) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x - 0.25f, transform.position.y + .25f), .8f * Time.deltaTime); //moving out
						delay -= Time.deltaTime;
					}
					if (delay < 0) {
						transform.position = Vector3.MoveTowards (transform.position, new Vector3(topTile.position.x, topTile.position.y + 0.5f), .8f * Time.deltaTime);
					}
				}
				if (leftPlatform != true) {
					transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x + 0.25f, transform.position.y + .25f), 1f * Time.deltaTime); //moving out
				}
			}
		}
	}
}
