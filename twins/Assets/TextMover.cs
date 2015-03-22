using UnityEngine;
using System.Collections;

public class TextMover : MonoBehaviour {

    //UIWidget widget;

	// Use this for initialization
	void Start () {
        //widget = GetComponent<UIWidget>();

        gameObject.MoveBy(new Vector3(Random.Range(-0.25f, 0.25f), 0, 0), 2f, 0, true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
