using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    GameObject explosionPrefab;

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

        // should depend on weapon strength - as well as knock back
        Game.Instance.getManager().cameraShake.Shake(0.025f, 0.01f);

    }

    void DestroySelf()
    {
        createExplosion();
        Destroy(this.gameObject);
    }

    void createExplosion()
    {
        GameObject expl = (GameObject)(Instantiate(explosionPrefab,
                                                   new Vector3(transform.position.x, transform.position.y, 0),
                                                   transform.rotation));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }

}
