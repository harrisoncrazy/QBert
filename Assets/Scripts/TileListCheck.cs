using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileListCheck : MonoBehaviour {

	public struct movementIndex {
		public bool upLeftMoveEnabled;
		public bool upRightMoveEnabled;
		public bool downLeftMoveEnabled;
		public bool downRightMoveEnabled;
	}

	private movementIndex movementTest;

	public static TileListCheck Instance;

	// Use this for initialization
	void Start () {
		Instance = this;
		movementTest = new movementIndex();
	}

	public movementIndex CheckTileMovement(int currentTile) {
		switch (currentTile) {
		case 1:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 2:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 3:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 4:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 5:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 6:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 7:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 8:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 9:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 10:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 11:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 12:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 13:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 14:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 15:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 16:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 17:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 18:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 19:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 20:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 21:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = true;
			movementTest.downRightMoveEnabled = true;
			return movementTest;
		case 22:
			movementTest.upLeftMoveEnabled = false;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 23:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 24:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 25:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 26:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 27:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = true;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		case 28:
			movementTest.upLeftMoveEnabled = true;
			movementTest.upRightMoveEnabled = false;
			movementTest.downLeftMoveEnabled = false;
			movementTest.downRightMoveEnabled = false;
			return movementTest;
		}
		return movementTest;
	}
}
