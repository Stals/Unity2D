﻿using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {

    [SerializeField]
    protected int currentValue = 0;

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
}