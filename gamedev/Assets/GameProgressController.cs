using UnityEngine;
using System.Collections;

public class GameProgressController : ProgressController {

    [SerializeField]
    UILabel name;

    const int maxLevel = 6;

    void FixedUpdate()
    {
        name.text = string.Format("GAME {0} / {1}", currentLevel, maxLevel -1);

        if (currentLevel >= maxLevel)
        {
            // TODO show gameover
        }
    }

    public override void setNewMaxValue()
    {
        switch (currentLevel)
        {
            case 2:
                maxValue = 20;
                break;
            case 3:
                                maxValue = 25;
                break;
            case 4:
                                maxValue = 30;
                break;
            case 5:
                                maxValue = 60;
                break;
            default:
                break;
        }
    }

    protected override void effectLevelup(GameObject go) {
        go.GetComponentInChildren<UILabel>().text = "GAME FINISHED!";
    }

}
