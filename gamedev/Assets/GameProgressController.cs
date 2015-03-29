using UnityEngine;
using System.Collections;

public class GameProgressController : ProgressController {

    [SerializeField]
    UILabel name;

    [SerializeField]
    ProgressController socialProgressController;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    GameObject successLabel;

    [SerializeField]
    GameObject failLabel;

    const int maxLevel = 9;

    static bool showedEnd = false;

    void FixedUpdate()
    {
        

        if (currentLevel >= maxLevel)
        {
            name.text = string.Format("GAME {0}", currentLevel);

            if (!showedEnd)
            {
                showGameOver();
                showedEnd = true;
            }
        }
        else {
            name.text = string.Format("GAME {0} / {1}", currentLevel, maxLevel - 1);
        }
    }

    public override void setNewMaxValue()
    {
        switch (currentLevel)
        {
            case 2:
                maxValue = 5;
                break;
            case 3:
                maxValue = 10;
                break;
            case 4:
                maxValue = 15;
                break;

            case 5:
                maxValue = 20;
                break;
            case 6:
                                maxValue = 25;
                break;
            case 7:
                                maxValue = 30;
                break;
            case 8:
                                maxValue = 45;
                break;
            default:
                maxValue = (int)(maxValue * 1.5f);
                break;
        }
    }

    protected override void effectLevelup(GameObject go) {
        go.GetComponentInChildren<UILabel>().text = "GAME FINISHED!";
    }

    void showGameOver()
    {
        gameOverPanel.SetActive(true);
        if (socialProgressController.currentLevel >= 4) { 
            successLabel.SetActive(true);
        }else{
            failLabel.SetActive(true);
        }
    }

}
