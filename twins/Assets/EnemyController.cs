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

        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();

        Game.Instance.getManager().sleepTime(20);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }

}
