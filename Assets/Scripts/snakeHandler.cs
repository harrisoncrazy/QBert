using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeHandler : MonoBehaviour {

	public TileListCheck.movementIndex[] movementIndex = new TileListCheck.movementIndex[28];

	// Use this for initialization
	void Start () {
		//movementIndex = new TileListCheck.movementIndex[28];

		for (int i = 0; i <= movementIndex.Length-1; i++) {
			Debug.Log (i);
			movementIndex [0] = TileListCheck.Instance.CheckTileMovement (i);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
