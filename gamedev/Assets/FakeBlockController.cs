using UnityEngine;
using System.Collections;

public class FakeBlockController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFinishAnimation()
    {
        // TODO add animation
        Destroy(this.gameObject);
    }
}
