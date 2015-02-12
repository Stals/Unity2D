using UnityEngine;
using System.Collections;


public class GameObjectActivator : MonoBehaviour {

    [SerializeField]
    GameObject objectToActivate;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable() {
        objectToActivate.SetActive(true);
    }

    void OnDisable()
    {
         objectToActivate.SetActive(false);
    }

}
