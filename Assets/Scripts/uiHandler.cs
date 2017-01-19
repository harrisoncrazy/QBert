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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ArrowAnim ();
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

	}
}
