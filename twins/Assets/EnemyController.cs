using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeDamage()
    {
        Debug.Log("HIT");
        animator.SetTrigger("TookDamage");

        var g = GetComponent<Rigidbody2D>();
        //g.AddForce()
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }

}
