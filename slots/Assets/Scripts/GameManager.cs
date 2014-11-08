using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	BoardGenerator generator;

	// Use this for initialization
	void Start () {
		generator = new BoardGenerator (8, 8, 5);
		Board firstBoard = generator.generateBoard ();
		int i = firstBoard.at (3, 3).id;
		int l = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
