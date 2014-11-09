using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
    int[] lines = new int[] {1, 2, 3, 4, 5, 6, 7, 8 , 9};
    int[] bets = new int[] {1, 2, 3, 4, 5, 10, 25, 50 , 100, 200, 300, 400, 500, 1000, 1250, 1500};

    [SerializeField]
    UILabel currentPrizeLabel;

    [SerializeField]
    UILabel currentLinesLabel;

    [SerializeField]
    UILabel currentBetLabel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void nextBet()
    {
    }

    public void prevBet()
    {
    }

    public void nextLines()
    {
    }

    public void prevLines()
    {
    }

    public void spin()
    {
    }
}
