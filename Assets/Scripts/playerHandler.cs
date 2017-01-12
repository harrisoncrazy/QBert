using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHandler : MonoBehaviour {

	public int currentRow;

	public int currentTile;

	private bool upLeftMoveEnabled = true;
	private bool upRightMoveEnabled = true;
	private bool downLeftMoveEnabled = true;
	private bool downRightMoveEnabled = true;

	// Use this for initialization
	void Start () {
		currentTile = 5;
		currentRow = 3;
	}
	
	// Update is called once per frame
	void Update () {
		CheckTileMovement ();

		if (Input.GetKeyDown(KeyCode.Q) && upLeftMoveEnabled == true) { //moving up and left
			currentTile = currentTile - currentRow;
			currentRow--;
			Debug.Log ("upleft");

			float currentTileTransformX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
			float currentTileTransformY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y;

			transform.position = new Vector3 (currentTileTransformX, currentTileTransformY + 0.15f, 0);
		}
		if (Input.GetKeyDown(KeyCode.E) && upRightMoveEnabled == true) { //moving up and right
			currentTile = currentTile - (currentRow-1);
			currentRow--;
			Debug.Log ("upright");

			float currentTileTransformX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
			float currentTileTransformY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y;

			transform.position = new Vector3 (currentTileTransformX, currentTileTransformY + 0.15f, 0);
		}
		if (Input.GetKeyDown(KeyCode.Z) && downLeftMoveEnabled == true) { //moving down and left
			currentTile = currentTile + currentRow;
			currentRow++;
			Debug.Log ("downLeft");

			float currentTileTransformX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
			float currentTileTransformY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y;

			transform.position = new Vector3 (currentTileTransformX, currentTileTransformY + 0.15f, 0);
		}
		if (Input.GetKeyDown(KeyCode.C) && downRightMoveEnabled == true) { //moving down and right
			currentTile = currentTile + currentRow+1;
			currentRow++;
			Debug.Log ("downright");

			float currentTileTransformX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
			float currentTileTransformY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y;

			transform.position = new Vector3 (currentTileTransformX, currentTileTransformY + 0.15f, 0);
		}
	}

	void CheckTileMovement() {
		switch (currentTile) {
		case 1:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 2:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 3:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 4:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 5:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 6:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 7:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 8:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 9:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 10:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 11:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 12:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 13:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 14:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 15:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 16:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 17:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 18:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 19:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 20:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 21:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
		case 22:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 23:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 24:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 25:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 26:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 27:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		case 28:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
		}
	}
}