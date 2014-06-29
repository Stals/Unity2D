using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static int playerScore1 = 0;
	public static int playerScore2 = 0;

	public GUISkin theSkin;

	public static void Score(string wallName) {
		if(wallName == "leftWall") {
			++playerScore2;
		} else if(wallName == "rightWall") {
			++playerScore1;
		}

		Debug.Log("Player score");
	}
	
	void OnGUI()
	{
		GUI.skin = theSkin;
		GUI.Label(new Rect(Screen.width/2-150, 25, 100, 100), "" + playerScore1);
		GUI.Label(new Rect(Screen.width/2+150, 25, 100, 100), "" + playerScore2);
	}
}
