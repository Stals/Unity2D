using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public static int playerScore1 = 0;
	public static int playerScore2 = 0;

	public GUISkin theSkin;

	Transform Ball;

	void Start(){
		Ball = (Transform)GameObject.FindGameObjectWithTag("Ball").transform;

	}

	public static void Score(string wallName) {
		if(wallName == "leftWall") {
			++playerScore2;
		} else if(wallName == "rightWall") {
			++playerScore1;
		}

		//Debug.Log("Player score");
	}
	
	void OnGUI()
	{
		GUI.skin = theSkin;
		GUI.Label(new Rect(Screen.width/2-150-18, 25, 100, 100), "" + playerScore1);
		GUI.Label(new Rect(Screen.width/2+150-18, 25, 100, 100), "" + playerScore2);


		const float buttonWidth = 121;
		const float buttonHeight = 53;
		if(GUI.Button(new Rect(Screen.width/2 - buttonWidth/2, 35, buttonWidth, buttonHeight), "RESET"))
		{
			playerScore1 = 0;
			playerScore2 = 0;

			Ball.SendMessage("ResetBall");
		}
	}
}
