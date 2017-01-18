using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeHandler : MonoBehaviour {

	public TileListCheck.movementIndex movementIndex;

	private bool gridInit = false;

	public int movementDir = 0;
	//an int for storing the movement direction
	//1 == UP LEFT
	//2 == UP RIGHT
	//3 == DOWN LEFT
	//4 == DOWN RIGHT

	//pathfinding
	public int currentSnakeTile;
	public int currentSnakeRow;
	public int currentPlayerTile;
	public int currentPlayerRow;

	public float delay = 1.0f;

	// Use this for initialization
	void Start () {
		//movementIndex = new TileListCheck.movementIndex[28];
		currentSnakeRow = 7;
		currentSnakeTile = 23;
	}
	
	// Update is called once per frame
	void Update () {/*
		if (gridInit == false) {//Initilizing the movement availiblity matrix
			for (int i = 0; i <= movementIndex.Length-1; i++) {
				movementIndex [0] = TileListCheck.Instance.CheckTileMovement (i);
			}
			gridInit = true;
		}*/

		delay -= Time.deltaTime;

		if (delay <= 0) {
			PickRoute ();
			switch (movementDir) {
			case 1:
				currentSnakeTile = currentSnakeTile - currentSnakeRow;
				currentSnakeRow--;
				break;
			case 2:
				currentSnakeTile = currentSnakeTile - (currentSnakeRow - 1);
				currentSnakeRow--;
				break;
			case 3:
				currentSnakeTile = currentSnakeTile + currentSnakeRow;
				currentSnakeRow++;
				break;
			case 4:
				currentSnakeTile = currentSnakeTile + (currentSnakeRow + 1);
				currentSnakeRow++;
				break;
			}
			delay = 1.0f;
		}


		transform.position = new Vector3 (GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x, GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f, 0); //setting exact point
		currentPlayerTile = playerHandler.Instance.currentTile;
		currentPlayerRow = playerHandler.Instance.currentRow;
	}

	void PickRoute() {
		movementIndex = TileListCheck.Instance.CheckTileMovement (currentSnakeTile);
		//if the player is above
		if (currentSnakeRow > currentPlayerRow) {
			if (movementIndex.upLeftMoveEnabled == true && movementIndex.upRightMoveEnabled == false) { //if right up movement is disabled
				movementDir = 1;
			}
			else if (movementIndex.upLeftMoveEnabled == false && movementIndex.upRightMoveEnabled == true) {//if left up movement is disabled
				movementDir = 2;
			}
			else if (movementIndex.upLeftMoveEnabled == true && movementIndex.upRightMoveEnabled == true) {//if both movement avenues are avalible
				//checking if the either of the options is the player tile
				if ((currentSnakeTile - currentSnakeRow) == currentPlayerTile) {
					movementDir = 1;
				} else if ((currentSnakeTile - (currentSnakeRow - 1)) == currentPlayerTile) {
					movementDir = 2;
				} else {
					int rando = Random.Range (1, 3);
					if (rando == 1) {
						movementDir = 1;
					}
					if (rando == 2) {
						movementDir = 2;
					}
				}
			}
		}

		if (currentSnakeRow < currentPlayerRow) { //if the player is below
			if (movementIndex.downLeftMoveEnabled == true && movementIndex.downRightMoveEnabled == false) { //if right up movement is disabled
				movementDir = 3;
			}
			if (movementIndex.downLeftMoveEnabled == false && movementIndex.downRightMoveEnabled == true) {//if left up movement is disabled
				movementDir = 4;
			}
			if (movementIndex.downLeftMoveEnabled == true && movementIndex.downRightMoveEnabled== true) {//if both movement avenues are avalible
				if ((currentSnakeTile + currentSnakeRow) == currentPlayerTile) {
					movementDir = 3;
				} else if ((currentSnakeTile + (currentSnakeRow + 1)) == currentPlayerTile) {
					movementDir = 4;
				} else {
					int rando = Random.Range (1, 3);
					if (rando == 1) {
						movementDir = 3;
					}
					if (rando == 2) {
						movementDir = 4;
					}
				}
			}
		}

		if (currentSnakeRow == currentPlayerRow) {
			int rando = Random.Range (1, 5);
			switch (rando) {
			case 1:
				movementDir = 1;
				break;
			case 2:
				movementDir = 2;
				break;
			case 3:
				movementDir = 3;
				break;
			case 4:
				movementDir = 4;
				break;
			}
		}
	}
}
