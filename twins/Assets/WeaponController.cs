using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

    public float anglePerSecond = 15f;
    public float explosionStrength = 1.0f;

    public float baseDamage = 1f;

    public string name = "no name";
    public string shortDescription = "no description";


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Enemy")
        {
            var enemy = coll.gameObject.GetComponent<EnemyController>();
            float damage = getDamage();
            bool isCrit = Random.Range(0, 100) < 5;
            if (isCrit) {
                damage *= 2;
            }

            enemy.takeDamage(damage, isCrit);


            Vector2 v = coll.transform.position - transform.position;

            Vector2 forceVec = v * explosionStrength;
            coll.rigidbody2D.AddForce(forceVec, ForceMode2D.Impulse);

        }
    }

    // TODO задавать разброс для каждого оружия
    public float getDamage()
    {
        return baseDamage + Random.Range(0, 2f);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      
	}
}
