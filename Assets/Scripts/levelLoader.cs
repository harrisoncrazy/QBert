using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelLoader : MonoBehaviour {

	public string LevelToLoad;

	public void LoadLevel() {
		Application.LoadLevel (LevelToLoad);
		Time.timeScale = 1.0f;
	}
}
