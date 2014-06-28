using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

	// Use this for initialization
	void Start() {
		int rnd = Random.Range(0, 2);

		if(rnd == 0) {
			rigidbody2D.AddForce(new Vector2(80, 10));
		} else {
			rigidbody2D.AddForce(new Vector2(-80, -10));
		}

	}

	void OnCollisionEnter2D (Collision2D colInfo) {
		if(colInfo.collider.tag == "Player") {
			//rigidbody2D.velocity.y = rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y / 2 + colInfo.collider.rigidbody2D.velocity.y / 3);
		}
	}
}
