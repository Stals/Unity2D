using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

	int width = 8;
	int height = 5;

	BoardGenerator generator;
	
	[SerializeField]
	List<GameObject> objectPrefabs;

	[SerializeField]
	float paddingX;

	[SerializeField]
	float paddingY;

	float objectWidth;
	float objectHeight;

	List<Block> selectedBlocks;

	// Use this for initialization
	void Start () {
		Vector3 size = objectPrefabs [0].GetComponentInChildren<SpriteRenderer> ().bounds.size;
		objectWidth = size.x;
		objectHeight = size.y;

		selectedBlocks = new List<Block> ();

		generator = new BoardGenerator (objectPrefabs.Count, width, height);
		createBoard ();
		positionBoard ();

		Game.Instance.setBoardManager (this);
	}


	void positionBoard()
	{
		float boardWidth = objectWidth * width + paddingX * (width - 1);
		float boardHeight = objectHeight * height + paddingY * (height - 1);

		transform.position = new Vector3( -boardWidth / 2, -boardHeight / 2, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		// mouse just released
		if(Input.GetMouseButtonUp(0)){
			foreach(Block block in selectedBlocks){
				block.gameObject.SetActive(false);
			}
		}
	}

	void createBoard()
	{
		Board board = generator.generateBoard ();

		for (int x = 0; x < width; ++x) {
			for(int y = 0; y < height; ++y){
				BoardObject boardObject = board.at(x, y);
				int id = boardObject.id;
				GameObject obj = (GameObject)Instantiate(objectPrefabs[id], getPosition(x, y), Quaternion.identity);
				obj.transform.parent = transform;

				boardObject.setGO(obj);
			}
		}
	}

	Vector3 getPosition(int x, int y){
		float xPos = 0;
		if(x != 0){
			xPos = (objectWidth * x) + (paddingX * x);
		}

		float yPos = 0;
		if(y != 0){
			yPos = (objectHeight * y) + (paddingY * y);
		}

		return new Vector3(xPos, yPos, 0);
	}

	public void onBlockTrigger(Block _block){
		selectedBlocks.Add (_block);
	}
}
