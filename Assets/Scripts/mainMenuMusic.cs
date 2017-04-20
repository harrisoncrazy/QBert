using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuMusic : MonoBehaviour {

	private AudioSource audioMain;

	public AudioClip[] audioList;

	// Use this for initialization
	void Start () {
		audioMain = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (audioMain.isPlaying == false) {
			int rando = Random.Range (0, audioList.Length);

			audioMain.PlayOneShot (audioList [rando]);
		}
	}
}
