using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiHandler : MonoBehaviour {

	//arrow animation values
	public GameObject farLeftArrow;
	public GameObject closeLeftArrow;
	public GameObject farRightArrow;
	public GameObject closeRightArrow;

	private float delay1 = 0.3f;
	private float delay2 = 0.3f;
	private float delay3 = 0.3f;

	public Text scoreText;

	public GameObject displayToTile;
	public Sprite setToTile;
	public Sprite defaultTile;
	private float timer = 0.1f;
	private bool swapped = false;

	//life display values
	private GameObject life1;
	private GameObject life2;
	private GameObject life3;
	private GameObject life4;
	private GameObject life5;
	private GameObject life6;
	private GameObject life7;
	private GameObject life8;
	private GameObject life9;


	// Use this for initialization
	void Start () {
		life1 = GameObject.Find ("life1");
		life2 = GameObject.Find ("life2");
		life3 = GameObject.Find ("life3");
		life4 = GameObject.Find ("life4");
		life5 = GameObject.Find ("life5");
		life6 = GameObject.Find ("life6");
		life7 = GameObject.Find ("life7");
		life8 = GameObject.Find ("life8");
		life9 = GameObject.Find ("life9");
	}
	
	// Update is called once per frame
	void Update () {
		DisplayScore ();
		DisplayLives ();

		if (GameManager.Instance.isWinning == false && GameManager.Instance.isFalling == false && GameManager.Instance.isEnding == false) {
			ArrowAnim ();
		}

		if (GameManager.Instance.isWinning == true) {
			cycleTile ();
		}
	}

	void ArrowAnim() {
		delay1 -= Time.deltaTime;

		if (delay1 <= 0) {
			farLeftArrow.SetActive (true);
			farRightArrow.SetActive (true);

			delay2 -= Time.deltaTime;
			if (delay2 <= 0) {
				closeLeftArrow.SetActive (true);
				closeRightArrow.SetActive (true);

				delay3 -= Time.deltaTime;
				if (delay3 <= 0) {
					farLeftArrow.SetActive (false);
					farRightArrow.SetActive (false);
					closeLeftArrow.SetActive (false);
					closeRightArrow.SetActive (false);
					delay1 = 0.3f;
					delay2 = 0.3f;
					delay3 = 0.3f;
				}
			}
		}
	}

	void DisplayScore() {
		scoreText.text = "" + GameManager.Instance.totalScore;
	}

	void cycleTile() {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			if (swapped == false) {
				displayToTile.GetComponent<Image>().sprite = setToTile;
				swapped = true;
			} 
			else if (swapped == true) {
				displayToTile.GetComponent<Image>().sprite = defaultTile;
				swapped = false;
			}
			timer = 0.1f;
		}
	}

	void DisplayLives() {
		switch (GameManager.Instance.totalLives) {
		case 0:
			life1.SetActive (false);
			life2.SetActive (false);
			life3.SetActive (false);
			life4.SetActive (false);
			life5.SetActive (false);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 1:
			life1.SetActive (true);
			life2.SetActive (false);
			life3.SetActive (false);
			life4.SetActive (false);
			life5.SetActive (false);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 2:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (false);
			life4.SetActive (false);
			life5.SetActive (false);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 3:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (false);
			life5.SetActive (false);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 4:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (false);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 5:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (false);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 6:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (false);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 7:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
			life8.SetActive (false);
			life9.SetActive (false);
			break;
		case 8:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
			life8.SetActive (true);
			life9.SetActive (false);
			break;
		case 9:
			life1.SetActive (true);
			life2.SetActive (true);
			life3.SetActive (true);
			life4.SetActive (true);
			life5.SetActive (true);
			life6.SetActive (true);
			life7.SetActive (true);
			life8.SetActive (true);
			life9.SetActive (true);
			break;

		default:
			break;
		}
	}
}
