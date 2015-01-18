using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            var enemy = coll.gameObject.GetComponent<EnemyController>();
            enemy.takeDamage();            
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
