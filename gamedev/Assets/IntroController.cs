using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

    [SerializeField]
    TweenAlpha tween;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void close (){
        tween.PlayForward();
        Destroy(gameObject, tween.duration);
    }
}
