using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class BoardManager : MonoBehaviour {

	int width = 8;
	int height = 6;

	BoardGenerator generator;
	
    [SerializeField]
    GameObject exampleObject;

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

    VectorLine line = null;
    Vector3[] lineVector;

    float boardWidth;
    float boardHeight;

    public bool createNewBoard = false;
    
    // Use this for initialization
	void Start () {
        Vector3 size = exampleObject.GetComponentInChildren<SpriteRenderer> ().bounds.size;
		objectWidth = size.x;
		objectHeight = size.y;

        boardWidth = objectWidth * width + paddingX * (width - 1) - objectWidth;
        boardHeight = objectHeight * height + paddingY * (height - 1) - objectHeight;
        
        selectedBlocks = new List<Block> ();

		generator = new BoardGenerator (objectPrefabs.Count, width, height);
        Game.Instance.setBoardManager (this);

        createBoard ();
		positionBoard ();

        gameObject.RotateBy(new Vector3(0, 0, -0.005f), 0.5f, 0, EaseType.easeInOutSine, LoopType.pingPong);
	}


	void positionBoard()
	{
        //transform.position = new Vector3(-1.206132f, -0.8818362f, transform.position.z);

        /*
        float boardWidth = objectWidth * width + paddingX * (width - 1) - objectWidth / 2f;
        float boardHeight = objectHeight * height + paddingY * (height - 1) - objectHeight / 2f;

		transform.position = new Vector3( -boardWidth / 2f, -boardHeight / 2f, transform.position.z);*/
	}

	// Update is called once per frame
	void Update () {
        updateSelectedLine();

		// mouse just released
		if(Input.GetMouseButtonUp(0)){
			if((selectedBlocks.Count >= 3)){
                if(Game.Instance.getGameManager().canRemoveLine()){
                    Game.Instance.getGameManager().onLineRemove(selectedBlocks.Count);
    				foreach(Block block in selectedBlocks){
    					removeBlock(block);
    				}
    				updateBlocksPosition();
                }else{
                    Game.errorHandler().noLinesLeft();
                }
			}
            endSelection();
		}

        if(createNewBoard){

            float rotation = gameObject.transform.localEulerAngles.z;
            float delta = 0.001f;
            if(((rotation - 1.8f) < delta) || (rotation < delta)) {
                createBoard();
                createNewBoard = false;
            }
        }
	}

    void endSelection()
    {
        foreach (Block block in selectedBlocks)
        {
            block.setSelected(false);
        }
        selectedBlocks.Clear();
        clearDisplayedLine();
    }

    void updateSelectedLine()
    {
        clearDisplayedLine();

        int pointsCount = selectedBlocks.Count;
        if (pointsCount < 2)
            return;

        Vector3[] points = new Vector3[selectedBlocks.Count];
        for(int i = 0; i < pointsCount; ++i){
            Vector3 pos = selectedBlocks[i].transform.position;
            pos.z = -5;
            points[i] = pos;
        }

        line = new VectorLine("LineRenderer", points, lineMaterial, 7.0f, Vectrosity.LineType.Continuous, Joins.Weld);
        line.Draw3D();
    }

    void clearDisplayedLine()
    {
        if (line != null)
        {
            Destroy(line.vectorObject);
            line = null;
        }
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

    public void clearBoard()
    {
        foreach(Transform child in transform) {
            iTween.Stop(child.gameObject);
            //Destroy(child.gameObject.GetComponent<iTween>());
            iTween.MoveBy(child.gameObject, iTween.Hash("amount", new Vector3(0, -3f, 0),
                                                  "time", 0.3f, "delay", 0f, "islocal", true, 
                                                  "easetype", "easeInQuad"));
            Destroy(child.gameObject, 1f);
        }
    }

	public void createBoard()
	{
		currentBoard = generator.generateBoard ();

		for (int x = 0; x < width; ++x) {
			for(int y = 0; y < height; ++y){
				BoardObject boardObject = currentBoard.at(x, y);
				int id = boardObject.id;
				GameObject obj = (GameObject)Instantiate(objectPrefabs[id], Vector3.zero, Quaternion.identity);
				obj.transform.parent = transform;
                Vector3 startPos = getPosition(x, y);
                startPos.y += ((y + 1) *( objectHeight * Random.Range(1, 3) )) + objectHeight * 7;
                obj.transform.localPosition = startPos;

                // делать немного выше и вызывать update position

                obj.transform.localEulerAngles = new Vector3(0, 0, -4.5f);

				Block block = obj.GetComponent<Block>();
				block.setIDs(x, y);
				boardObject.setBlock(block);
             }
		}

        updateBlocksPosition();
	}

	public Vector3 getPosition(int x, int y){
        
        float xPos = 0;
		if(x != 0){
			xPos = (objectWidth * x) + (paddingX * x);
		}

        xPos -= boardWidth / 2;

		float yPos = 0;
		if(y != 0){
			yPos = (objectHeight * y) + (paddingY * y);
		}

        yPos -= boardHeight / 2;

		return new Vector3(xPos, yPos, 0);
	}

	public void onBlockTrigger(Block _block){
        // check for backtracking//
        if (alreadySelected(_block))
        {
            // deselect last
            if(selectedBlocks.Count >= 2){
                if(selectedBlocks[selectedBlocks.Count - 2] == _block){
                    Block lastBlock = getLastSelectedBlock();
                    lastBlock.setSelected(false);
                    selectedBlocks.RemoveAt(selectedBlocks.Count - 1);
                }
            }
        }

        // check for adding new
		else if (sameColorAsPrevious (_block) &&
			nearPrevious (_block)){

				selectedBlocks.Add (_block);
                _block.setSelected(true);
		}
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

    bool alreadySelected(Block block)
    {
        return block.isSelected();
    }

    public int getSelectedCount()
    {
        return selectedBlocks.Count;
    }
}
