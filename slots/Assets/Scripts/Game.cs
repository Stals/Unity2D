using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game  {
	private static Game instance;
	private Game() {
        // create player
	}
	public static Game Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new Game();
			}
			return instance;
		}
	}

	GameManager gameManager;
	BoardManager boardManager;

    /*public void init(GameManager _manager)
	{
	}*/

	public void setBoardManager(BoardManager _manager){
		boardManager = _manager;
	}

	public BoardManager getBoardManager()
	{
		return boardManager;
	}
}