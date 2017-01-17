using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileListCheck : MonoBehaviour {

	public struct movementIndex {
		public bool upLeftMoveEnabled;
		public bool upRightMoveEnabled;
		public bool downLeftMoveEnabled;
		public bool downRightMoveEnabled;

		public bool leftTeleporter;
		public bool rightTeleporter;
		public int teleporterNumber;
	}

	private movementIndex movementTest;

	public int[] teleporterTiles;

	public int platform0;
	public int platform1;
	public int platform2;
	public int platform3;

	public static TileListCheck Instance;


	// Use this for initialization
	void Start () {
		Instance = this;
		movementTest = new movementIndex();
		teleporterTiles = new int[4];
		teleporterTiles [0] = platform0;
		teleporterTiles [1] = platform1;
		teleporterTiles [2] = platform2;
		teleporterTiles [3] = platform3;
	}

	public movementIndex CheckTileMovement(int currentTile) {
		movementTest.leftTeleporter = false;
		movementTest.rightTeleporter = false;
		for (int i = 0; i <= teleporterTiles.Length - 1; i++) {
			if (currentTile == teleporterTiles [i]) {
				movementTest.teleporterNumber = i;
				switch (currentTile) {
				case 4:
					movementTest.leftTeleporter = true;
					movementTest.rightTeleporter = false;
					break;
				case 6:
					movementTest.leftTeleporter = false;
					movementTest.rightTeleporter = true;
					break;
				case 7:
					movementTest.leftTeleporter = true;
					movementTest.rightTeleporter = false;
					break;
				case 10:
					movementTest.leftTeleporter = false;
					movementTest.rightTeleporter = true;
					break;
				case 11:
					movementTest.leftTeleporter = true;
					movementTest.rightTeleporter = false;
					break;
				case 15:
					movementTest.leftTeleporter = false;
					movementTest.rightTeleporter = true;
					break;
				case 16:
					movementTest.leftTeleporter = true;
					movementTest.rightTeleporter = false;
					break;
				case 21:
					movementTest.leftTeleporter = false;
					movementTest.rightTeleporter = true;
					break;
				case 22:
					movementTest.leftTeleporter = true;
					movementTest.rightTeleporter = false;
					break;
				case 28:
					movementTest.leftTeleporter = false;
					movementTest.rightTeleporter = true;
					break;
				}
			}
		}
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
