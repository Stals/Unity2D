using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;



public class TutorialManager : MonoBehaviour {

    private int UPGRADES_TUTORIAL_INDEX = 3;
    private int HINT_FADEOUT_TIME = 250;

    [SerializeField]
    List<GameObject> panels;

    [SerializeField]
    GameObject tutorialEnemies;

    [SerializeField]
    List<UpgradeView> upgradeButtons;

    public static bool isCompleted = false;

	// Use this for initialization
	void Start () {
        GameSceneManager.tutorialManager = this;

        if (!isCompleted)
        {
            showPanel(0);
            tutorialEnemies.SetActive(true);
        }
	}

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void showPanel(int index){
        if (index != 0)
        {
            GameObject go = panels[index - 1];
            go.GetComponent<UITweener>().PlayReverse();
            //NGUITools.SetActive(go, false);
        }

        if(index >= panels.Count){
            isCompleted = true;
            GameSceneManager.world.init(); // start actual game
            return;
        }

        Observable.Timer(TimeSpan.FromMilliseconds(250)).Subscribe(i =>
        {
            setPanelActive(index);          

            Observable.Timer(TimeSpan.FromMilliseconds(getTimeForIndex(index))).Subscribe(l =>
            {
                showPanel(index + 1);
            }); 
        }); 
    }

    void setPanelActive(int index)
    {
        NGUITools.SetActive(panels[index], true);

        if (index == UPGRADES_TUTORIAL_INDEX)
        {
            int time = getTimeForIndex(index);

            int sections = upgradeButtons.Count;

            for(int i = 0; i < sections; ++i ){
                int singleDelay = (time / (sections + 1));
                // Нужен так как иначе в Observable передастся последнее состояние i
                int tmpIndex = i;
                Observable.Timer(TimeSpan.FromMilliseconds(singleDelay * (i + 1))).Subscribe(l =>
                {
                    upgradeButtons[tmpIndex].hoverOn();

                    Observable.Timer(TimeSpan.FromMilliseconds(singleDelay - HINT_FADEOUT_TIME)).Subscribe(p =>
                    {
                        upgradeButtons[tmpIndex].hoverOut();
                    });

                });
                
            }

        }

    }


    int getTimeForIndex(int index) {
        switch (index)
        {
            case 0: return 3000;
            case 1: return 4000;
            case 2: return 4000;
            case 3: return 6000;
            case 4: return 2000;
            default: return 100;
        }
    }   
}
