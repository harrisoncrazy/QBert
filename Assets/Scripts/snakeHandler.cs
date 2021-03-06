﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snakeHandler : MonoBehaviour {

	public TileListCheck.movementIndex movementIndex;

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

	//deciding on going left or right
	private float distToPlayer;
	private bool movingLeft = false;

	//following platform values
	private bool isAtPlatformTile = false;

	private bool soundplayed = false;

	// Use this for initialization
	void Start () {
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		float playerX = GameManager.Instance.currentPlayer.transform.position.x;
		float snakeX = this.transform.position.x;

		if (playerX < snakeX) {
			movingLeft = true;
		} else if (playerX > snakeX) {
			movingLeft = false;
		}

		/*
		if (currentSnakeTile <= 28 && currentSnakeTile > 21) {
			currentSnakeRow = 7;
		}
		if (currentSnakeTile <= 21 && currentSnakeTile > 15) {
			currentSnakeRow = 6;
		}
		if (currentSnakeTile <= 15 && currentSnakeTile > 10) {
			currentSnakeRow = 5;
		}
		if (currentSnakeTile <= 10 && currentSnakeTile > 6) {
			currentSnakeRow = 4;
		}
		if (currentSnakeTile <= 6 && currentSnakeTile > 3) {
			currentSnakeRow = 3;
		}
		if (currentSnakeTile <= 3 && currentSnakeTile > 1) {
			currentSnakeRow = 2;
		}
		if (currentSnakeTile == 1) {
			currentSnakeRow = 1;
		}*/

		if (GameManager.Instance.isEnding == false) {
			if (GameManager.Instance.isFalling == false) {
				if (GameManager.Instance.isDying == false) {
					if (GameManager.Instance.ballGrabbed == false) {
						if (isAtPlatformTile == false) {
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

									if (currentPlayerTile == currentSnakeTile && playerHandler.Instance.isAtTeleporter == true) {
										isAtPlatformTile = true;
										if (playerHandler.Instance.leftPlaftform == true) {
											GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
											mainSprite.sprite = topLeftIdle;

											startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
											startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

											controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
											controlPointY = startPointY + 0.5f;

											endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x - 0.15f; //the final destination tile
											endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y - 0.21f;
											delay = 0.5f;
										} else {
											GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
											mainSprite.sprite = topRightIdle;

											startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
											startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

											controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
											controlPointY = startPointY + 0.5f;

											endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x + 0.15f; //the final destination tile
											endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y - 0.21f;
											delay = 0.5f;
										}
									}
								}
							}
							if (isMoving == false) {
								delay -= Time.deltaTime;

								if (delay <= 0) {
									PickRoute ();
									switch (movementDir) {
									case 1:
										GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
										mainSprite.sprite = topLeftJump;
										startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
										startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
										controlPointY = startPointY + 0.5f;

										currentSnakeTile = currentSnakeTile - currentSnakeRow; //changing to the next tile based on math
										currentSnakeRow--;

										endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
										endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										isMoving = true;
										break;
									case 2:
										GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
										mainSprite.sprite = topRightJump;
										startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
										startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
										controlPointY = startPointY + 0.5f;

										currentSnakeTile = currentSnakeTile - (currentSnakeRow - 1);
										currentSnakeRow--;

										endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
										endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										isMoving = true;
										break;
									case 3:
										GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
										mainSprite.sprite = botLeftJump;
										startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
										startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
										controlPointY = startPointY + 0.5f;

										currentSnakeTile = currentSnakeTile + currentSnakeRow; //changing to the next tile based on math
										currentSnakeRow++;

										endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
										endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										isMoving = true;
										break;
									case 4:
										GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeJump, 0.7f);
										mainSprite.sprite = botRightJump;
										startPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x;
										startPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										controlPointX = startPointX;//curve points for the bezier curve, pics a point slightly above the player to curve the jump
										controlPointY = startPointY + 0.5f;

										currentSnakeTile = currentSnakeTile + (currentSnakeRow + 1);
										currentSnakeRow++;

										endPointX = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.x; //the final destination tile
										endPointY = GameObject.Find ("tile" + currentSnakeTile + "Base").transform.position.y + 0.21f;

										isMoving = true;
										break;
									}
									delay = 0.5f;
								}
							}


							if (playerHandler.Instance.isAtTeleporter == false) {
								currentPlayerTile = playerHandler.Instance.currentTile;
								currentPlayerRow = playerHandler.Instance.currentRow;
							} else if (playerHandler.Instance.isAtTeleporter == true) {
								currentPlayerTile = playerHandler.Instance.lastTeleTile;
								currentPlayerRow = playerHandler.Instance.lastTeleRow;
							}

						} else if (isAtPlatformTile == true) {
							delay -= Time.deltaTime;

							if (delay <= 0) {
								if (playerHandler.Instance.leftPlaftform == true) {
									mainSprite.sprite = topLeftJump;
								} else {
									mainSprite.sprite = topRightJump;
								}

								if (soundplayed == false) {
									soundplayed = true;
									GameManager.Instance.audioMain.PlayOneShot (GameManager.Instance.snakeFall, 0.7f);
								}
								mainSprite.sortingOrder = currentSnakeRow - 1;
								curveX = (((1 - BezierTime) * (1 - BezierTime)) * startPointX) + (2 * BezierTime * (1 - BezierTime) * controlPointX) + ((BezierTime * BezierTime) * endPointX);
								curveY = (((1 - BezierTime) * (1 - BezierTime)) * startPointY) + (2 * BezierTime * (1 - BezierTime) * controlPointY) + ((BezierTime * BezierTime) * endPointY);
								transform.position = new Vector3 (curveX, curveY, 0);

								BezierTime = BezierTime + Time.deltaTime * 2f;

								if (BezierTime >= 1) {
									
									transform.position = Vector3.MoveTowards (transform.position, new Vector3 (endPointX, endPointY - 1.0f), 1f * Time.deltaTime); //falling down
									GameManager.Instance.totalScore += 500;
									StartCoroutine ("destroySelf");
								}
							}
						}
					}
				}
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
					if (movingLeft == true) {
						movementDir = 1;
					}
					if (movingLeft == false) {
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
					if (movingLeft == true) {
						movementDir = 3;
					}
					if (movingLeft == false) {
						movementDir = 4;
					}
				}
			}
		}

		if (currentSnakeRow == currentPlayerRow) {
			if (movementIndex.upLeftMoveEnabled == true && movementIndex.upRightMoveEnabled == false) { //if right up movement is disabled
				movementDir = 1;
			} else if (movementIndex.upLeftMoveEnabled == false && movementIndex.upRightMoveEnabled == true) {//if left up movement is disabled
				movementDir = 2;
			} else if (movementIndex.upLeftMoveEnabled == true && movementIndex.upRightMoveEnabled == true) {//if both movement avenues are avalible
				int rando = Random.Range (1, 3);
				if (movingLeft == true) {
					movementDir = 1;
				}
				if (movingLeft == false) {
					movementDir = 2;
				}

			} else if (movementIndex.downLeftMoveEnabled == true && movementIndex.downRightMoveEnabled == false) { //if right up movement is disabled
				movementDir = 3;
			} else if (movementIndex.downLeftMoveEnabled == false && movementIndex.downRightMoveEnabled == true) {//if left up movement is disabled
				movementDir = 4;
			} else if (movementIndex.downLeftMoveEnabled == true && movementIndex.downRightMoveEnabled == true) {//if both movement avenues are avalible
				int rando = Random.Range (1, 3);
				if (movingLeft == true) {
					movementDir = 3;
				}
				if (movingLeft == false) {
					movementDir = 4;
				}
			
			}
		}
	}

	IEnumerator destroySelf() {
		yield return new WaitForSeconds (1.0f);
		ballSpawner.Instance.isSnakeSpawned = false;
		Destroy (this.gameObject);
	}
}
