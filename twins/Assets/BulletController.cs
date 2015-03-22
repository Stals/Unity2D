using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    [SerializeField]
    float speed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!renderer.isVisible) {
            Destroy(gameObject);
        }
	}

    void FixedUpdate()
    {
        transform.Translate(speed, 0, 0);
    }
}
