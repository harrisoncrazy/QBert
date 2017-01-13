using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHandler : MonoBehaviour {

	public static playerHandler Instance;

	public int totalConvertedTiles = 0;

	public bool isEnding = false;
	public bool isWinning = false;

	private SpriteRenderer mainSprite;

	//Player Sprites
	public Sprite topLeftIdle;
	public Sprite topRightIdle;
	public Sprite botLeftIdle;
	public Sprite botRightIdle;

	public Sprite topLeftJump;
	public Sprite topRightJump;
	public Sprite botLeftJump;
	public Sprite botRightJump;

	private int jumpFace;

	public int currentRow;

	public int currentTile;


	//movement bools
	private bool upLeftMoveEnabled = true;
	private bool upRightMoveEnabled = true;
	private bool downLeftMoveEnabled = true;
	private bool downRightMoveEnabled = true;


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

	// Use this for initialization
	void Start () {
		Instance = this;
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();

		currentTile = 5;
		currentRow = 3;
		CheckTileMovement ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving == true) { //bezier curve for movement
			if (isEnding == false) {
				curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
				curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
				transform.position = new Vector3 (curveX, curveY, 0);

				BezierTime = BezierTime + Time.deltaTime * 2f;

				if (BezierTime >= 0.95) {//setting the sprite to landing slightly before it actually finishes
					if (jumpFace == 1) {
						mainSprite.sprite = topLeftIdle;
					} else if (jumpFace == 2) {
						mainSprite.sprite = topRightIdle;
					} else if (jumpFace == 3) {
						mainSprite.sprite = botLeftIdle;
					} else if (jumpFace == 4) {
						mainSprite.sprite = botRightIdle;
					}
				}

				if (BezierTime >= 1) { //end of the jump
					BezierTime = 0;
					isMoving = false;
					transform.position = new Vector3 (endPointX, endPointY, 0); //setting the player to an exact final value
					GameObject.Find ("tile" + currentTile + "Base").GetComponent<tileHandler> ().changeTile (); //changing the landed tile
				}
			}
		}
		if (isMoving == false) { //if the tile isnt currently moving 
			if (isEnding == false) {//if the game isnt currently ending
				if (Input.GetKeyDown (KeyCode.Q)) { //moving up and left
					if (upLeftMoveEnabled == true) {
						jumpFace = 1; //int for reseting to proper idle sprite after jump is done
						mainSprite.sprite = topLeftJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
						controlPointY = startPointY + 0.5f;

						currentTile = currentTile - currentRow; //changing to the next tile based on math
						currentRow--;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x; //the final destination tile
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						isMoving = true;

						CheckTileMovement (); //checking what tiles the player is able to jump too
					} else {
						jumpFace = 1;
						mainSprite.sprite = topLeftJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
						controlPointY = startPointY + 0.5f;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.15f; //the final destination tile
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.25f;
						mainSprite.sortingOrder = currentRow;
						isMoving = true;
						StartCoroutine ("EndGame");
					}
				}
				if (Input.GetKeyDown (KeyCode.E)) { //moving up and right
					if (upRightMoveEnabled == true) {
						jumpFace = 2;
						mainSprite.sprite = topRightJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;
						controlPointY = startPointY + 0.5f;

						currentTile = currentTile - (currentRow - 1);
						currentRow--;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						isMoving = true;

						CheckTileMovement ();
					} else {
						jumpFace = 2;
						mainSprite.sprite = topRightJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
						controlPointY = startPointY + 0.5f;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.15f; //the final destination tile
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.25f;
						mainSprite.sortingOrder = currentRow;
						isMoving = true;
						StartCoroutine ("EndGame");
					}
				}
				if (Input.GetKeyDown (KeyCode.Z)) { //moving down and left
					if (downLeftMoveEnabled == true) {
						jumpFace = 3;
						mainSprite.sprite = botLeftJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;
						controlPointY = startPointY + 0.25f;

						currentTile = currentTile + currentRow;
						currentRow++;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						isMoving = true;

						CheckTileMovement ();
					} else {
						jumpFace = 3;
						mainSprite.sprite = botLeftJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
						controlPointY = startPointY + 0.5f;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.15f; //the final destination tile
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.25f;
						mainSprite.sortingOrder = currentRow + 1;
						isMoving = true;
						StartCoroutine ("EndGame");
					}
				}
				if (Input.GetKeyDown (KeyCode.C)) { //moving down and right
					if (downRightMoveEnabled == true) {
						jumpFace = 4;
						mainSprite.sprite = botRightJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;
						controlPointY = startPointY + 0.25f;

						currentTile = currentTile + currentRow + 1;
						currentRow++;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						isMoving = true;

						CheckTileMovement ();
					} else {
						jumpFace = 3;
						mainSprite.sprite = botRightJump;
						startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
						startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

						controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
						controlPointY = startPointY + 0.5f;

						endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.15f; //the final destination tile
						endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.25f;
						mainSprite.sortingOrder = currentRow + 1;
						isMoving = true;
						StartCoroutine ("EndGame");
					}
				} 
			}
		}

		if (totalConvertedTiles == 28) { //ending!
			isEnding = true;
			isWinning = true;
			StartCoroutine ("EndGame");
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		isEnding = true;
		StartCoroutine ("EndGame");
	}

	IEnumerator EndGame() {
		yield return new WaitForSeconds (3.0f);
		SceneManager.LoadScene ("level1");
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
			break;
		case 3:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 4:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 5:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 6:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 7:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 8:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 9:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 10:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 11:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 12:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 13:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 14:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 15:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 16:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 17:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 18:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 19:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 20:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 21:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 22:
			upLeftMoveEnabled = false;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 23:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 24:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 25:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 26:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 27:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = true;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		case 28:
			upLeftMoveEnabled = true;
			upRightMoveEnabled = false;
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			break;
		}
	}
}