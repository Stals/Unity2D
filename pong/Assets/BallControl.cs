﻿using UnityEngine;
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
	
	// Update is called once per frame
	void Update() {
	
	}
}
