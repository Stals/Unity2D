using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField]
    List<GameObject> panels;

    [SerializeField]
    GameObject tutorialEnemies;

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
            NGUITools.SetActive(panels[index - 1], false);
        }

        if(index >= panels.Count){
            isCompleted = true;
            GameSceneManager.world.init(); // start actual game
            return;
        }

         NGUITools.SetActive(panels[index], true);

         Observable.Timer(TimeSpan.FromMilliseconds(getTimeForIndex(index))).Subscribe(l =>
         {
             showPanel(index + 1);
         }); 
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
