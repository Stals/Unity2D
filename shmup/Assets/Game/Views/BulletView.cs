using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class BulletView {
	void Start(){
	}

	void FixedUpdate(){
		transform.position = new Vector3 (transform.position.x, transform.position.y + Bullet.speed);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		coll.GetComponent<EnemyView> ().ExecuteTakeDamage (1);
	}
}
