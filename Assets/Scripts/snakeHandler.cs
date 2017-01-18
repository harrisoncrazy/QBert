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

	private float delay = 0.5f;

	//Bezier Curve Variables
	private bool isMoving = false;

	private float startPointX = 0;
	private float startPointY = 0;

	private float controlPointX = 0;
	private float controlPointY = 0;

	private float endPointX = 0;
	private float endPointY = 0;

	private float curveX;
	private float curveY;

	private float BezierTime = 0;

	//sprite variablse
	private SpriteRenderer mainSprite;

	public Sprite topLeftIdle;
	public Sprite topRightIdle;
	public Sprite botLeftIdle;
	public Sprite botRightIdle;

	public Sprite topLeftJump;
	public Sprite topRightJump;
	public Sprite botLeftJump;
	public Sprite botRightJump;

	// Use this for initialization
	void Start () {
		currentSnakeRow = 7;
		currentSnakeTile = 23;

		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.isEnding == false) {
			if (playerHandler.Instance.isFalling == false) {
				if (isMoving == true) {
					curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
					curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
					transform.position = new Vector3 (curveX, curveY, 0);

					BezierTime = BezierTime + Time.deltaTime * 2f;

					if (BezierTime >= 0.95) {//setting the sprite to landing slightly before it actually finishes
						if (movementDir == 1) {
							mainSprite.sprite = topLeftIdle;
						} else if (movementDir == 2) {
							mainSprite.sprite = topRightIdle;
						} else if (movementDir == 3) {
							mainSprite.sprite = botLeftIdle;
						} else if (movementDir == 4) {
							mainSprite.sprite = botRightIdle;
						}
					}

					if (BezierTime >= 1) { //end of the jump
						BezierTime = 0;
						isMoving = false;
						transform.position = new Vector3 (endPointX, endPointY, 0); //setting the player to an exact final value
						mainSprite.sortingOrder = currentSnakeRow + 1;
					}
				}
				if (isMoving == false) {
					delay -= Time.deltaTime;

					if (delay <= 0) {
						PickRoute ();
						switch (movementDir) {
						case 1:
						//audioMain.PlayOneShot (jump, 0.7f);
							mainSprite.sprite = topLeftJump;
							startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							currentSnakeTile = currentSnakeTile - currentSnakeRow; //changing to the next tile based on math
							currentSnakeRow--;

							endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
							endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							isMoving = true;
							break;
						case 2:
					//audioMain.PlayOneShot (jump, 0.7f);
							mainSprite.sprite = topRightJump;
							startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							currentSnakeTile = currentSnakeTile - (currentSnakeRow - 1);
							currentSnakeRow--;

							endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
							endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							isMoving = true;
							break;
						case 3:
					//audioMain.PlayOneShot (jump, 0.7f);
							mainSprite.sprite = botLeftJump;
							startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							currentSnakeTile = currentSnakeTile + currentSnakeRow; //changing to the next tile based on math
							currentSnakeRow++;

							endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
							endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							isMoving = true;
							break;
						case 4:
					//audioMain.PlayOneShot (jump, 0.7f);
							mainSprite.sprite = botRightJump;
							startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							currentSnakeTile = currentSnakeTile + (currentSnakeRow + 1);
							currentSnakeRow++;

							endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
							endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f;

							isMoving = true;
							break;
						}
						delay = 0.5f;
					}
				}


				//transform.position = new Vector3 (GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x, GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.15f, 0); //setting exact point
				currentPlayerTile = playerHandler.Instance.currentTile;
				currentPlayerRow = playerHandler.Instance.currentRow;
			}
		}
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
