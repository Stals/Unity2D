using System;
using UnityEngine;
using System.Collections;

public class BeatTicker : MonoBehaviour {

    public delegate void OnBigBeatEvent();
	public OnBigBeatEvent onBigBeat;

	public delegate void OnBeatEvent();
	public OnBeatEvent onBeat;

	private Song currentSong;
	private int currentBeat = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void begin(Song song)
	{
		currentSong = song;
		scheduleBeat ();
	}

	public void end()
	{
       // StopAction(onTimeElapsed);
	}

	void scheduleBeat()
	{
        InvokeAction(onTimeElapsed, 60f / currentSong.tempo);
	}

	void onTimeElapsed(){
		++currentBeat;

		if (currentBeat == 1) {
			if (onBigBeat != null) {
				onBigBeat ();
			}
		} else {
			if (onBeat != null) {
				onBeat ();
			}	
		}

		if (currentBeat == currentSong.beatsPerBar) {
			currentBeat = 0;
		}

        scheduleBeat();
	}

	protected void InvokeAction(Action action, float t)
	{
		StartCoroutine(InvokeActionRoutine(action, t));
	}
   /* protected void StopAction(Action action)
    {
        StopCoroutine(action);
    }*/
	
	private IEnumerator InvokeActionRoutine(Action action, float t)
	{
		yield return new WaitForSeconds(t);
		action();
	}


}
