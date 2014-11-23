using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    GameObject gameOverPanel;

	// Use this for initialization
	void Start () {
        GameSceneManager.gameOverManager = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showGameOver()
    {

            Observable.Timer(TimeSpan.FromMilliseconds(500)).Subscribe(l =>
        {
            NGUITools.SetActive(gameOverPanel, true);
            Time.timeScale = 0f;
        });
    }

    public void restartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        Time.timeScale = 1f;
    }
}
