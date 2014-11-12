using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    AudioSource removeLine;

    [SerializeField]
    AudioSource click;

	// Use this for initialization
	void Start () {
        Game.Instance.setSoundManager(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playRemoveLine()
    {
        removeLine.Play();
    }

    public void playClick()
    {
        click.Play();
    }
}
