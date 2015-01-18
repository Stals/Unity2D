using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

              public float explosionStrength = 1.0f;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            var enemy = coll.gameObject.GetComponent<EnemyController>();
            enemy.takeDamage();


            Vector2 v = coll.transform.position - transform.position;

            Vector2 forceVec = v * explosionStrength;
            coll.rigidbody2D.AddForce(forceVec, ForceMode2D.Impulse);

        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}
}
