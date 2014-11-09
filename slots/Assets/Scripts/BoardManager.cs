﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

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

	Board currentBoard;

    [SerializeField]
    Material lineMaterial;

    VectorLine line;
    
    // Use this for initialization
	void Start () {
		Vector3 size = objectPrefabs [0].GetComponentInChildren<SpriteRenderer> ().bounds.size;
		objectWidth = size.x;
		objectHeight = size.y;

		selectedBlocks = new List<Block> ();

		generator = new BoardGenerator (objectPrefabs.Count, width, height);
		createBoard ();
		positionBoard ();

        line = new VectorLine("LineRenderer", new Vector3[2], lineMaterial, 10.0f, Vectrosity.LineType.Continuous);

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
		updateBlocksPosition();
        updateSelectedLine();

		// mouse just released
		if(Input.GetMouseButtonUp(0)){
			if(selectedBlocks.Count >= 3){
				foreach(Block block in selectedBlocks){
					removeBlock(block);
				}
				//TODO with lerp later.
				updateBlocksPosition();
			}
            endSelection();
		}
	}

    void endSelection()
    {
        foreach (Block block in selectedBlocks)
        {
            block.setSelected(false);
        }
        selectedBlocks.Clear();
    }

    void updateSelectedLine()
    {
        // TODO startRotating
        /*
        Vector3 startPoint = new Vector3(0.5f, 0.5f);
        startPoint.z = -1;
        Vector3 endPoint = new Vector3(2.5f, 2.5f);
        endPoint.z = -1;
        
        line.points3 [0] = startPoint;
        line.points3 [1] = endPoint;
        
        line.Draw3D (); // pass local transform?
        */
    }

	void updateBlocksPosition()
	{
		for (int x = 0; x < width; ++x) {
			for (int y = 0; y < height; ++y) {
				Block block = currentBoard.at(x, y).block;
				if(block != null){
					block.updatePosition();
				}
			}
		}
	}

	void removeBlock(Block block)
	{
		// update indexes and positions of the top ones
		// set empty ids to the top ones. like -1
		// without a gameobject
		// this movement should be put into Board class probably
		currentBoard.removeAt (block.x, block.y);
		block.gameObject.SetActive(false);
	}

	void createBoard()
	{
		currentBoard = generator.generateBoard ();

		for (int x = 0; x < width; ++x) {
			for(int y = 0; y < height; ++y){
				BoardObject boardObject = currentBoard.at(x, y);
				int id = boardObject.id;
				GameObject obj = (GameObject)Instantiate(objectPrefabs[id], getPosition(x, y), Quaternion.identity);
				obj.transform.parent = transform;

				Block block = obj.GetComponent<Block>();
				block.setIDs(x, y);
				boardObject.setBlock(block);
			}
		}
	}

	public Vector3 getPosition(int x, int y){
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
		if (sameColorAsPrevious (_block) &&
			nearPrevious (_block)) {

				selectedBlocks.Add (_block);
                _block.setSelected(true);
		}

		// При наведении, добавлять только если тотже цвет или там пусто!!!!!
		// Добавлять только если вокруг предведущий.
	}

	Block getLastSelectedBlock()
	{
		if (selectedBlocks.Count == 0) {
			return null;
		} else {
			return selectedBlocks [selectedBlocks.Count - 1];
		}
	}

	bool sameColorAsPrevious(Block block)
	{
        Block lastBlock = getLastSelectedBlock();
        if (lastBlock == null)
        {
            return true;
        } else
        {
            return string.Equals(lastBlock.getImageName(), block.getImageName());
        }
	}

	bool nearPrevious(Block block)
	{
        Block lastBlock = getLastSelectedBlock();
        if (lastBlock == null)
        {
            return true;
        } else
        {
            bool xOK = Mathf.Abs(lastBlock.x - block.x) <= 1;
            bool yOK = Mathf.Abs(lastBlock.y - block.y) <= 1;
            return xOK && yOK;
        }
	}

}
