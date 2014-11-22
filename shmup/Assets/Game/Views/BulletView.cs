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
        transform.localPosition = new Vector3(transform.localPosition.x + Bullet.speed, transform.localPosition.y);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		coll.GetComponent<EnemyView> ().ExecuteTakeDamage (1);
        Destroy(this.gameObject);
	}
}
