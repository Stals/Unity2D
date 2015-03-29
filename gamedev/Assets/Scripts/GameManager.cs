using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    float perBlock = 0.125f;

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

    [SerializeField]
    UILabel linesLeftLabel;

    Player player;

    int currentBet = 0;
    int linesLeft = 9;



    public int hoursLeft;
    [SerializeField]
    UILabel leftHoursLabel;

    int currentWeek = 1;

    [SerializeField]
    UILabel weekCounterLabel;

    [SerializeField]
    ProgressController healthProgress;

    [SerializeField]
    GameObject progressGrid;


    int currentMoney = 50;

    float displayMoney = 0;
    [SerializeField]
    UILabel uiMoneyLabel;


	// Use this for initialization
	void Start () {
        Game.Instance.setGameManager(this);
        player = Game.Instance.getPlayer();

        currentPrizeLabel.text = "";

        hoursLeft = getHoursLeftMax();

        uiMoneyLabel.text = currentMoney.ToString();
        displayMoney = currentMoney;
	}

    int getHoursLeftMax()
    {
        ProgressController[] progs = progressGrid.GetComponentsInChildren<ProgressController>();

        int sum = 0;

        for (int i = 0; i < progs.Length; ++i) {
            ProgressController prog = progs[i];
            sum += prog.getEffectHours();
        }

        return 40 + sum;
    }

    int getMoneyPerWeek()
    {
        ProgressController[] progs = progressGrid.GetComponentsInChildren<ProgressController>();

        int sum = 0;

        for (int i = 0; i < progs.Length; ++i)
        {
            ProgressController prog = progs[i];
            sum += prog.getEffectMoney();
        }

        return sum; 
    }
	
	// Update is called once per frame
	void Update () {



        leftHoursLabel.text = (hoursLeft - Game.Instance.getBoardManager().getSelectedCount()).ToString();
        weekCounterLabel.text = currentWeek.ToString();


        displayMoney = Mathf.Lerp(displayMoney, currentMoney, Time.deltaTime * 5f);

        uiMoneyLabel.text = displayMoney.ToString("F2");
        ///
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
        if (player.getMoney() >= getTotalBet())
        {
            currentBet = getCurrentBet();
            linesLeft = getCurrentLines();

            Game.Instance.getPlayer().substractMoney(getTotalBet());
            Game.Instance.getBoardManager().clearBoard();
            Game.Instance.getBoardManager().createNewBoard = true;
        }else{
            Game.errorHandler().notEnoughMoney();
        }
    }

    public void onLineRemove(int blockCount)
    {
        hoursLeft -= blockCount;

        if (hoursLeft <= 0) {
            currentWeek += 1;
            hoursLeft = getHoursLeftMax();

            healthProgress.currentValue -= 5;
            if (healthProgress.currentValue < 0) {
                healthProgress.currentValue = 0;
            }
            //currentMoney -= 50;
            currentMoney += getMoneyPerWeek();

            // TODO add week
            // - hp
            // give money
        }

        //GJManager.GenerateHighscores(Game.Instance.getPlayer().getMoney());
    }

    public float countMultipier(int blockCount)
    {
        if(blockCount > 0){
            return perBlock * Mathf.Pow(2, blockCount - 1);
        }else{
            return 0;
        }
    }

    public bool canRemoveLine()
    {
		return true;
    }
}
