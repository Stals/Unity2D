using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {

    [SerializeField]
    public int currentValue = 0;

    [SerializeField]
    protected int maxValue = 100;

    [SerializeField]
    public int currentLevel = 1;

    UISlider progressBar;

    [SerializeField]
    UILabel progressLabel;

    [SerializeField]
    UILabel levelLabel;

    [SerializeField]
    GameObject levelupPanelPrefab;

    [SerializeField]
    UILabel effectLabel;

    [SerializeField]
    Color goodEffect;

    [SerializeField]
    Color badEffect;

    [SerializeField]
    UILabel nameLabel;

	// Use this for initialization
    public virtual void Start()
    {
        progressBar = GetComponent<UISlider>();
        progressBar.value = ((float)currentValue) / maxValue;

        if (currentLevel == 0) {
            levelLabel.alpha = 0;
        }
	}
	
	// Update is called once per frame
	public virtual void Update () {
        progressBar.value = Mathf.Lerp(progressBar.value, ((float)currentValue) / maxValue, Time.deltaTime * 5f);
        int dislayValue = (int)Mathf.Round(progressBar.value * maxValue);
        progressLabel.text = string.Format("{0} / {1}", dislayValue, maxValue);


        if (isLevelupBar())
        {
            levelLabel.text = "L." + currentLevel.ToString();

            if (dislayValue >= maxValue)
            {
                 currentLevel += 1;
                 // TODO play animation
                 playLevelup();
                    
                 // TODO update effects

                 progressBar.value = 0;
                 currentValue = 0;
                 setNewMaxValue();
             }
        }

        string effectString = getEffectString();
        effectLabel.text = effectString;

        if (effectLabel.text.Length > 0) {
            if (effectString[0] == '+')
            {
                effectLabel.color = goodEffect;
            }
            else {
                effectLabel.color = badEffect;
            }
        }

	}

    public virtual  void setNewMaxValue()
    {
        maxValue = (int)Mathf.Round(maxValue * 1.5f);
    }

    public virtual void addBlock()
    {
        currentValue += 1;
    }

    bool isLevelupBar()
    {
        return currentLevel >= 1;
    }

    void playLevelup()
    {
       GameObject go = NGUITools.AddChild(transform.parent.gameObject, levelupPanelPrefab);
       effectLevelup(go);
    }

    protected virtual void effectLevelup(GameObject go) { }

    public int getEffectHours() {
        if (nameLabel.text == "HEALTH") {
            if (currentValue >= 95) return 5;
            if (currentValue >= 80) return 3;
            if (currentValue >= 60) return 1;            

            if (currentValue <= 10) return -5;
            if (currentValue <= 25) return -2;
            if (currentValue <= 40) return -1;

        } if (nameLabel.text == "Social") {
            if (currentLevel >= 5) return -5;
            if (currentLevel >= 4) return -4;
            if (currentLevel >= 3) return -3;
            if (currentLevel >= 2) return -1;  
                  
        }
        if (nameLabel.text == "Skill") {
            if (currentLevel >= 5) return currentLevel - 2;
            if (currentLevel >= 4) return 2;
            if (currentLevel >= 3) return 1;
            if (currentLevel >= 2) return 0;
            return -1;
        
        }

        return 0;
    }

    public int getEffectSkill() {
        return 0;
    }

    public int getEffectMoney()
    {
        if (nameLabel.text == "Work")
        {
            return currentLevel * 50;
        }
        return 0;
    }

    public string getEffectString()
    {
        int h = getEffectHours();
        int skill = getEffectSkill();
        int money = getEffectMoney();

        if (h > 0) {
            return string.Format("+{0}H", h);
        }
        if (h < 0)
        {
            return string.Format("{0}H", h);
        }

        if (money > 0)
        {
            return string.Format("+{0}$", money);
        }
        if (money < 0)
        {
            return string.Format("{0}$", money);
        }


   


        return "";
    }

}
