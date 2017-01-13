using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBall : MonoBehaviour {

	private SpriteRenderer mainSprite;

	public Sprite BallJump;
	public Sprite BallHit;

	public int currentRow;

	public int currentTile;

	private bool bouncingStarted = false;

	private bool downLeftMoveEnabled = true;
	private bool downRightMoveEnabled = true;

	//Bezier Curve Variables
	private bool isMoving = false;
	private bool ballDelayed = true;

	private float startPointX = 0;
	private float startPointY = 0;

	private float controlPointX = 0;
	private float controlPointY = 0;

	private float endPointX = 0;
	private float endPointY = 0;

	private float curveX;
	private float curveY;

	private float BezierTime = 0;

	private bool atEnd = false;

	// Use this for initialization
	void Start () {
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.isEnding == false) {
			if (bouncingStarted == false) { //falling down to the first point on the grid
				transform.position = Vector3.MoveTowards (transform.position, GameObject.Find ("tile" + currentTile + "Base").transform.position, 2f * Time.deltaTime); //moving towards the point

				if (transform.position.y <= GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f) {//reaching the point
					transform.position = new Vector3 (GameObject.Find ("tile" + currentTile + "Base").transform.position.x, GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f, 0); //setting exact point
					mainSprite.sprite = BallHit; //changing sprite
					bouncingStarted = true;//starting bounce
					StartCoroutine ("ballDelay");
				}
			}

			if (bouncingStarted == true) {
				int rando = Random.Range (0, 3);
				if (isMoving == true) {
					curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
					curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
					transform.position = new Vector3 (curveX, curveY, 0);

					BezierTime = BezierTime + Time.deltaTime * 1f;

					if (BezierTime >= 0.95) {//setting the sprite to landing slightly before it actually finishes
						mainSprite.sprite = BallHit;
					}

					if (BezierTime >= 1) { //end of the jump
						BezierTime = 0;
						isMoving = false;
						StartCoroutine ("ballDelay");
						transform.position = new Vector3 (endPointX, endPointY, 0); //setting the player to an exact final value
					}
				}
				if (isMoving == false) {
					if (ballDelayed == false) {
						if (rando == 1) {
							if (downLeftMoveEnabled == true) { //moving down and left
								if (atEnd == false) {
									mainSprite.sprite = BallJump;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

									controlPointX = startPointX;
									controlPointY = startPointY + 0.25f;

									currentTile = currentTile + currentRow;
									currentRow++;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

									isMoving = true;
									ballDelayed = true;

									CheckTileMovement ();
								}
							} else if (atEnd == true) {
								mainSprite.sprite = BallJump;
								startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
								startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

								controlPointX = startPointX;
								controlPointY = startPointY + 0.25f;

								endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.15f;
								endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.24f;

								isMoving = true;
								ballDelayed = true;
								StartCoroutine ("deleteSelf");
							}
						}

						if (rando == 2) {
							if (downRightMoveEnabled == true) { //moving down and right
								if (atEnd == false) {
									mainSprite.sprite = BallJump;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

									controlPointX = startPointX;
									controlPointY = startPointY + 0.25f;

									currentTile = currentTile + currentRow + 1;
									currentRow++;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

									isMoving = true;
									ballDelayed = true;

									CheckTileMovement ();
								}
							} else if (atEnd == true) {
								mainSprite.sprite = BallJump;
								startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
								startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

								controlPointX = startPointX;
								controlPointY = startPointY + 0.25f;

								endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.15f;
								endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.24f;

								isMoving = true;
								ballDelayed = true;
								StartCoroutine ("deleteSelf");
							}
						}
					}
				}
			}
		}
	}

	IEnumerator ballDelay() {
		yield return new WaitForSeconds (0.15f);
		ballDelayed = false;
	}

	IEnumerator deleteSelf() {
		yield return new WaitForSeconds (1.0f);
		Destroy (this.gameObject);
	}

	void CheckTileMovement() {
		switch (currentTile) {
		case 1:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 2:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 3:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 4:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 5:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 6:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 7:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 8:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 9:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 10:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 11:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 12:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 13:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 14:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 15:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 16:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 17:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 18:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 19:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 20:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 21:
			downLeftMoveEnabled = true;
			downRightMoveEnabled = true;
			break;
		case 22:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 23:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 24:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 25:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 26:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 27:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		case 28:
			downLeftMoveEnabled = false;
			downRightMoveEnabled = false;
			atEnd = true;
			break;
		}
	}
}
