using UnityEngine;
using System.Collections;

public class FakeBlockController : MonoBehaviour {

    [SerializeField]
    GameObject particleObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnFinishAnimation()
    {
        GameObject go = NGUITools.AddChild(transform.parent.gameObject, particleObject);
        go.transform.localEulerAngles = transform.localEulerAngles;

        go.transform.position = transform.position;


        // TODO add animation
        Destroy(this.gameObject);
    }
}
