using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour {

    public BeatTicker beatTicker;

	// Use this for initialization
	void Start () {
		Game.Instance.init (beatTicker);
        beatTicker.begin(Game.Instance.getCurrentSong());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
