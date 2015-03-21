using UnityEngine;
using System.Collections;

public class TextBouncer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        iTween.MoveTo(gameObject, iTween.Hash("x", 75, "time", 1.5, "islocal", true));
        iTween.MoveTo(gameObject, iTween.Hash("y", 50, "time", 0.3, "islocal", true));
        iTween.MoveTo(gameObject, iTween.Hash("y", 0, "time", 1, "islocal", true, "delay", 0.3, "easeType", EaseType.easeOutBounce.ToString()));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
