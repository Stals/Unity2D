using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class RigidSpeedController : MonoBehaviour {

    [SerializeField]
    float maxSpeed = 10f;

    [SerializeField]
    float friction = 0.8f;




    Rigidbody2D body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        body.velocity *= friction;

        if (body.velocity.x > maxSpeed) {
            body.velocity = new Vector2(maxSpeed, body.velocity.y);
        }

        if (body.velocity.y > maxSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, maxSpeed);
        }
    }
}
