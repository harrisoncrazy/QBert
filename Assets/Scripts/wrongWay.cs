using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongWay : MonoBehaviour {

	private SpriteRenderer mainSprite;

	public Sprite defaultSpriteLeft;
	public Sprite defaultSpriteRight;
	public Sprite jumpingSpriteLeft;
	public Sprite jumpingSpriteRight;

	private TileListCheck.movementIndex movementTest;//movement index, refers to an outside script which automatically checks tile routes

	public int currentRow;

	public int currentTile;

	public bool jumpingStarted = false;

	//Bezier Curve Variables
	public bool isMoving = false;
	public bool ballDelayed = false;

	private float startPointX = 0;
	private float startPointY = 0;

	private float controlPointX = 0;
	private float controlPointY = 0;

	private float endPointX = 0;
	private float endPointY = 0;

	private float curveX;
	private float curveY;

	public float BezierTime = 0;

	private bool atEnd = false;

	// Use this for initialization
	void Start () {
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();

		movementTest = new TileListCheck.movementIndex();

		currentRow = 7;
		currentTile = 22;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentTile == 1) {
			atEnd = true;
		}
		if (currentTile == 3) {
			atEnd = true;
		}
		if (currentTile == 6) {
			atEnd = true;
		}
		if (currentTile == 10) {
			atEnd = true;
		}
		if (currentTile == 15) {
			atEnd = true;
		}
		if (currentTile == 21) {
			atEnd = true;
		}
		if (currentTile == 28) {
			atEnd = true;
		}

		if (GameManager.Instance.isEnding == false) {
			if (GameManager.Instance.isFalling == false) {
				if (jumpingStarted == false) {
					transform.position = Vector3.MoveTowards (transform.position, new Vector3 ((GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f), (GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f)), 2f * Time.deltaTime); //moving towards the point

					if (transform.position.y <= GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f) {//reaching the point
						transform.position = new Vector3 (GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f, GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f, 0); //setting exact point
						jumpingStarted = true;//starting bounce
						StartCoroutine ("ballDelay");
					}
				}

				if (jumpingStarted == true) {
					int rando = Random.Range (0, 3);
					if (isMoving == true) {//bezier curve for jumping from tile to tile
						curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
						curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
						transform.position = new Vector3 (curveX, curveY, 0);

						BezierTime = BezierTime + Time.deltaTime * 1f;

						if (BezierTime >= 0.95) {//setting the sprite to landing slightly before it actually finishes
							if (mainSprite.sprite == jumpingSpriteLeft) {
								mainSprite.sprite = defaultSpriteLeft;
							}
							if (mainSprite.sprite == jumpingSpriteRight) {
								mainSprite.sprite = defaultSpriteRight;
							}
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
							if (rando == 1) {//moving left
								if (atEnd == false) {
									mainSprite.sprite = jumpingSpriteLeft;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									controlPointX = startPointX;
									controlPointY = startPointY + 0.25f;

									currentTile = currentTile - (currentRow - 1);
									currentRow--;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									isMoving = true;
									ballDelayed = true;

								} else if (atEnd == true) {
									mainSprite.sprite = jumpingSpriteLeft;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									controlPointX = startPointX - 0.15f;
									controlPointY = startPointY + 0.25f;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.6f;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.6f;

									isMoving = true;
									ballDelayed = true;
									StartCoroutine ("deleteSelf");
								}
							}
						
							if (rando == 2) {//moving right
								if (atEnd == false) {
									mainSprite.sprite = jumpingSpriteRight;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									controlPointX = startPointX - 0.15f;
									controlPointY = startPointY + 0.25f;

									currentTile++;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									isMoving = true;
									ballDelayed = true;
								} else if (atEnd == true) {
									mainSprite.sprite = jumpingSpriteRight;
									startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x - 0.125f;
									startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.085f;

									controlPointX = startPointX;
									controlPointY = startPointY + 0.25f;

									endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.6f;
									endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.6f;

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
		yield return new WaitForSeconds (0.35f);
		ballDelayed = false;
	}

	IEnumerator deleteSelf() {
		yield return new WaitForSeconds (1.0f);
		Destroy (this.gameObject);
	}
}
