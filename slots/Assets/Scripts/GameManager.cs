using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	BoardGenerator generator;

	[SerializeField]
	List<GameObject> objectPrefabs;

	// Use this for initialization
	void Start () {
		generator = new BoardGenerator (objectPrefabs.Count, 8, 5);
		Board firstBoard = generator.generateBoard ();
		int i = firstBoard.at (3, 3).id;
		int l = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
