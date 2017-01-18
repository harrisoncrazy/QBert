using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHandler : MonoBehaviour {

	public static playerHandler Instance;

	public int startingTile;
	public int startingRow;

	public int totalConvertedTiles = 0;

	private bool inputDelay = true;

	public bool isFalling = false;
	public bool isEnding = false;
	public bool isWinning = false;

	public bool isAtTeleporter = false;
	public bool startingTeleport = false;
	public bool stoppedTeleport = false;

	private SpriteRenderer mainSprite;

	private Transform topTile;

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

	public TileListCheck.movementIndex movementTest;//movement index, refers to an outside script which automatically checks tile routs

	private GameObject teleporterPlatform;

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

	//Audio Files
	public AudioClip jump;
	public AudioClip fall;
	public AudioClip endMusic;
	public AudioClip teleportMusic;
	public AudioClip startMusic;
	AudioSource audioMain;

	// Use this for initialization
	void Start () {
		Instance = this;
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
		audioMain = GetComponent<AudioSource> ();
		movementTest = new TileListCheck.movementIndex();
		topTile = GameObject.Find ("tile" + 1 + "Base").transform;

		currentTile = startingTile;
		currentRow = startingRow;
		audioMain.PlayOneShot (startMusic, 1.0f);
		StartCoroutine ("StartDelay");
	}
	
	// Update is called once per frame
	void Update () {
		if (inputDelay == false) {
			if (isMoving == true) { //bezier curve for movement
				if (isEnding == false) {
					if (startingTeleport != true) {
						curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
						curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
						transform.position = new Vector3 (curveX, curveY, 0);

						BezierTime = BezierTime + Time.deltaTime * 2f;

						if (isAtTeleporter != true) {
							if (isFalling == false) {
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

							if (isFalling == true) {
								if (BezierTime >= 1) {
									transform.position = Vector3.MoveTowards (transform.position, new Vector3 (endPointX, endPointY - 1.0f), 1f * Time.deltaTime); //falling down
								}
							}
						}
					}
					if (isAtTeleporter == true) {
						if (stoppedTeleport != true) {
							if (BezierTime >= 1) {
								transform.position = new Vector3 (teleporterPlatform.transform.position.x, teleporterPlatform.transform.position.y + 0.075f);
								if (startingTeleport == false) {
									audioMain.PlayOneShot (teleportMusic, 0.7f);
									startingTeleport = true;
								}
							}
						}
						if (stoppedTeleport == true) {
							transform.position = Vector3.MoveTowards (transform.position, new Vector3 (topTile.position.x, topTile.position.y + 0.15f), 1f * Time.deltaTime); //falling down
							if (transform.position == new Vector3 (topTile.position.x, topTile.position.y + 0.15f)) {//once fallen down to the tile, ending the teleport sequence
								startingTeleport = false;
								stoppedTeleport = false;
								isAtTeleporter = false;
								isMoving = false;
								mainSprite.sortingOrder = 2;
								BezierTime = 0;
								TileListCheck.Instance.teleporterTiles [movementTest.teleporterNumber] = 0; //destroying the teleport in the array
								GameObject.Find ("tile" + currentTile + "Base").GetComponent<tileHandler> ().changeTile (); //changing the landed tile
							}
						}
					}
				}
			}
			if (isMoving == false) { //if the tile isnt currently moving 
				if (isEnding == false) {//if the game isnt currently ending
					if (Input.GetKeyDown (KeyCode.Q)) { //moving up and left
						CheckTileMovement ();//checking what tiles the player is able to jump too
						if (movementTest.leftTeleporter != true) { //testing if a teleporter is accessible
							if (movementTest.upLeftMoveEnabled == true) {
								audioMain.PlayOneShot (jump, 0.7f);
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

								mainSprite.sortingOrder = currentRow + 1;
								isMoving = true;
							} else {
								if (movementTest.leftTeleporter != true) {
									audioMain.PlayOneShot (fall, 0.7f);
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
									isFalling = true;
									StartCoroutine ("EndGame");
								}
							}
						}
						if (movementTest.leftTeleporter == true) {
							teleporterPlatform = GameObject.Find ("Platform" + movementTest.teleporterNumber);
							audioMain.PlayOneShot (jump, 0.7f);
							jumpFace = 1;
							mainSprite.sprite = topLeftJump;
							startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							endPointX = teleporterPlatform.transform.position.x; //the final destination tile
							endPointY = teleporterPlatform.transform.position.y + 0.075f;
							currentRow = 1;
							currentTile = 1;
							TileListCheck.Instance.teleporterTiles [movementTest.teleporterNumber] = 0;
							isAtTeleporter = true;
							isMoving = true;
						}
					}

					if (Input.GetKeyDown (KeyCode.E)) { //moving up and right
						CheckTileMovement ();
						if (movementTest.rightTeleporter != true) {
							if (movementTest.upRightMoveEnabled == true) {
								audioMain.PlayOneShot (jump, 0.7f);
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

								mainSprite.sortingOrder = currentRow + 1;
								isMoving = true;
							} else {
								if (movementTest.rightTeleporter != true) {
									audioMain.PlayOneShot (fall, 0.7f);
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
									isFalling = true;
									StartCoroutine ("EndGame");
								}
							}
						} 
						if (movementTest.rightTeleporter == true) {
							teleporterPlatform = GameObject.Find ("Platform" + movementTest.teleporterNumber);
							audioMain.PlayOneShot (jump, 0.7f);
							jumpFace = 2;
							mainSprite.sprite = topRightJump;
							startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							endPointX = teleporterPlatform.transform.position.x; //the final destination tile
							endPointY = teleporterPlatform.transform.position.y + 0.075f;
							currentRow = 1;
							currentTile = 1;
							isAtTeleporter = true;
							isMoving = true;
						}
					}
					if (Input.GetKeyDown (KeyCode.Z)) { //moving down and left
						CheckTileMovement ();
						if (movementTest.downLeftMoveEnabled == true) {
							audioMain.PlayOneShot (jump, 0.7f);
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

							mainSprite.sortingOrder = currentRow + 1;
							isMoving = true;
						} else {
							audioMain.PlayOneShot (fall, 0.7f);
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
							isFalling = true;
							StartCoroutine ("EndGame");
						}
					}
					if (Input.GetKeyDown (KeyCode.C)) { //moving down and right
						CheckTileMovement ();
						if (movementTest.downRightMoveEnabled == true) {
							audioMain.PlayOneShot (jump, 0.7f);
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

							mainSprite.sortingOrder = currentRow + 1;
							isMoving = true;
						} else {
							audioMain.PlayOneShot (fall, 0.7f);
							jumpFace = 4;
							mainSprite.sprite = botRightJump;
							startPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x;
							startPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y + 0.15f;

							controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
							controlPointY = startPointY + 0.5f;

							endPointX = GameObject.Find ("tile" + currentTile + "Base").transform.position.x + 0.15f; //the final destination tile
							endPointY = GameObject.Find ("tile" + currentTile + "Base").transform.position.y - 0.25f;
							mainSprite.sortingOrder = currentRow + 1;
							isMoving = true;
							isFalling = true;
							StartCoroutine ("EndGame");
						}
					} 
				}
			}

			if (totalConvertedTiles == 28) { //ending, filling in all tiles
				if (isWinning == false) {
					isEnding = true;
					isWinning = true;
					audioMain.PlayOneShot (endMusic, 0.7f);
					StartCoroutine ("EndGame");
				}
			}
		}
	}
	/*
	void OnTriggerEnter2D(Collider2D col) {
		isEnding = true;
		StartCoroutine ("EndGame");
	}*/

	IEnumerator EndGame() {
		yield return new WaitForSeconds (3.0f);
		SceneManager.LoadScene ("level1");
	}

	IEnumerator StartDelay() {
		yield return new WaitForSeconds (2f);
		inputDelay = false;
	} 

	void CheckTileMovement(){
		movementTest = TileListCheck.Instance.CheckTileMovement (currentTile);
	}
}