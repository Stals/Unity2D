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
    SoundManager _soundManager;
    ErrorHandler _errorHandler;

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

    public void setSoundManager(SoundManager _manager)
    {
        _soundManager = _manager;
    }
    
    public SoundManager soundManager()
    {
        return _soundManager;
    }

    public void setErrorHandler(ErrorHandler handler){
    	_errorHandler = handler;
    }

    static public ErrorHandler errorHandler()
    {
    	return Instance._errorHandler;
    }

    public Player getPlayer(){
    	return player;
    }
}