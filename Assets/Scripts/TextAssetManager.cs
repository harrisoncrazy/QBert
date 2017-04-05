using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class TextAssetManager {

	public static List<string> TextToList(TextAsset textAsset) {

		string path = "Assets/highscores.txt";
		StreamReader reader = new StreamReader (path);
		List<string> textList = new List<string> ();

		try {
			do {
				textList.Add(reader.ReadLine());
			}
			while (reader.Peek() != -1);
		}
		finally {
			reader.Close();
		}

		return textList;
	}

	public static void ListToText(List<string> textList) {
		string path = "Assets/highscores.txt";
		StreamWriter writer = new StreamWriter (path);

		for (int i = 0; i < textList.Count; i++) {
			writer.WriteLine (textList [i]);
		}

		writer.Close();
	}
}
