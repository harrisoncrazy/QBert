using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileHandler : MonoBehaviour {

	public int tileNumber;

	private SpriteRenderer mainSprite;

	public Sprite defaultTile;
	public Sprite changedTile;

	private bool isStepped = false;

	public float timer = 0.1f;
	private bool swapped = false;

	// Use this for initialization
	void Start () {
		mainSprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHandler.Instance.isWinning == true) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				if (swapped == false) {
					mainSprite.sprite = defaultTile;
					swapped = true;
				} 
				else if (swapped == true) {
					mainSprite.sprite = changedTile;
					swapped = false;
				}
				timer = 0.1f;
			}
		}
	}

	public void changeTile() {
		if (isStepped == false) {
			mainSprite.sprite = changedTile;
			playerHandler.Instance.totalConvertedTiles++;
			isStepped = true;
		}
	}
}
