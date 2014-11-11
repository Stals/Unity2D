using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game  {
	private static Game instance;
	private Game() {
        // create player
        player = new Player(100);

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
	Player player;

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

    public void setGameManager(GameManager _manager)
    {
        gameManager = _manager;
    }

    public GameManager getGameManager()
    {
        return gameManager;
    }

    public Player getPlayer(){
    	return player;
    }
}