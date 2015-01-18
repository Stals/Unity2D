using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{
    private static Game instance;
    private Game()
    {
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


    MyGameManager manager;

    public void init(MyGameManager _manager)
    {
        manager = _manager;
    }

    public MyGameManager getManager()
    {
        return manager;
    }


}