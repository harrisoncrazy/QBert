using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redBall : MonoBehaviour {

	private SpriteRenderer mainSprite;

	public Sprite BallJump;
	public Sprite BallHit;

	public int currentRow;

	public int currentTile;

	private TileListCheck.movementIndex movementTest;//movement index, refers to an outside script which automatically checks tile routs

	private bool bouncingStarted = false;

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

	public AudioClip jump;
	AudioSource audioMain;

	// Use this for initialization
	void Start () {
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
		audioMain = GetComponent<AudioSource> ();

		movementTest = new TileListCheck.movementIndex();
	}

	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.isEnding == false) {
			if (playerHandler.Instance.isFalling == false) {
				if (bouncingStarted == false) { //falling down to the first point on the grid
					transform.position = Vector3.MoveTowards (transform.position, GameObject.Find ("tile" + currentTile + "Base").transform.position, 2f * Time.deltaTime); //moving towards the point

					if (transform.position.y <= GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f) {//reaching the point
						transform.position = new Vector3 (GameObject.Find ("tile" + currentTile + "Base").transform.position.x, GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f, 0); //setting exact point
						mainSprite.sprite = BallHit; //changing sprite
						bouncingStarted = true;//starting bounce
						CheckTileMovement();
						StartCoroutine ("ballDelay");
					}
				}

				if (bouncingStarted == true) {
					int rando = Random.Range (0, 3);
					if (isMoving == true) {//bezier curve for jumping from tile to tile
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
							transform.position = new Vector3 (endPointX, endPointY, 0); //setting the ball to an exact final value
						}
					}
					if (isMoving == false) {//if not currently moving tiles
						if (ballDelayed == false) { //if falling has finished
							if (rando == 1) {
								if (movementTest.downLeftMoveEnabled == true) { //moving down and left
									if (atEnd == false) {
										audioMain.PlayOneShot (jump, 0.7f);
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
								if (movementTest.downRightMoveEnabled == true) { //moving down and right
									if (atEnd == false) {
										audioMain.PlayOneShot (jump, 0.7f);
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
		movementTest = TileListCheck.Instance.CheckTileMovement (currentTile);
		if (currentTile >= 22) {
			atEnd = true;
		}
	}
}
