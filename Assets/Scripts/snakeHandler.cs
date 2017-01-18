using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeHandler : MonoBehaviour {

	public TileListCheck.movementIndex[] movementIndex;

	private bool gridInit = false;

	// Use this for initialization
	void Start () {
		movementIndex = new TileListCheck.movementIndex[28];
	}
	
	// Update is called once per frame
	void Update () {
		if (gridInit == false) {
			for (int i = 0; i <= movementIndex.Length-1; i++) {
				movementIndex [0] = TileListCheck.Instance.CheckTileMovement (i);
			}
			gridInit = true;
		}
	}
}
