﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    float perBlock = 0.25f;
    float money = 100;

    int[] lines = new int[] {1, 2, 3, 4, 5, 6, 7, 8 , 9};
    int[] bets = new int[] {1, 2, 3, 4, 5, 10, 25, 50 , 100, 200, 300, 400, 500, 1000, 1250, 1500};

    int currentLinesID = 0;
    int currentBetsID = 0;

    [SerializeField]
    UILabel currentPrizeLabel;

    [SerializeField]
    UILabel currentLinesLabel;

    [SerializeField]
    UILabel currentBetLabel;

    [SerializeField]
    UILabel moneyLabel;

    [SerializeField]
    UILabel totalBetLabel;

	// Use this for initialization
	void Start () {
        Game.Instance.setGameManager(this);

        currentPrizeLabel.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        moneyLabel.text = string.Format("Money: {0}", money);

        currentLinesLabel.text = getCurrentLines().ToString();
        currentBetLabel.text = getCurrentBet().ToString();
        totalBetLabel.text = getTotalBet().ToString();

        float baseMoney = Game.Instance.getBoardManager().getSelectedCount() * perBlock;
        currentPrizeLabel.text = string.Format("{0} x {1} = {2}", baseMoney, getCurrentBet(), baseMoney * getCurrentBet());
	}

    public int getCurrentBet()
    {
        return bets[currentBetsID];
    }

    public int getCurrentLines()
    {
        return lines[currentLinesID];
    }

    public int getTotalBet()
    {
        return getCurrentBet() * getCurrentLines();
    }

    public void nextBet()
    {
        ++currentBetsID;
        if (currentBetsID >= bets.Length)
        {
            currentBetsID = 0;
        }
    }

    public void prevBet()
    {
        --currentBetsID;
        if (currentBetsID < 0)
        {
            currentBetsID = bets.Length - 1;
        }
    }

    public void nextLines()
    {
        ++currentLinesID;
        if (currentLinesID >= lines.Length)
        {
            currentLinesID = 0;
        }
    }

    public void prevLines()
    {
        --currentLinesID;
        if (currentLinesID < 0)
        {
            currentLinesID = lines.Length - 1;
        }
    }

    public void spin()
    {
        if (money >= getTotalBet())
        {
            money -= getTotalBet();
            Game.Instance.getBoardManager().clearBoard();
            Game.Instance.getBoardManager().createBoard();
        }
    }

    public void onLineRemove(int blockCount)
    {
        money += blockCount * perBlock * getCurrentBet();
    }
}
