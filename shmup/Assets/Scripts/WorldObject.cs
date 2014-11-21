using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {

    [SerializeField]
    float speed = 0.02f;

    Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        _transform.position = new Vector3(_transform.position.x - speed, _transform.position.y);
    }
}
