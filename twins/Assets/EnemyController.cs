using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [SerializeField]
    GameObject explosionPrefab;

    [SerializeField]
    int hp = 3;

    [SerializeField]
    float movementSpeed = 0.1f;

    Animator animator;

    GameObject target;

	// Use this for initialization

	void Start () {
        target = Game.Instance.getPlayer().gameObject;
        updateRotation();

        animator = GetComponent<Animator>();
	}

    void updateRotation()
    {
        Vector2 vectorL = target.transform.position - transform.position;

        float angleZ = Mathf.Rad2Deg * (Mathf.Atan2(vectorL.y, vectorL.x));

        transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

    void updatePosition()
    {
        transform.Translate(movementSpeed, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        updateRotation();

        // TODO: archers move only if not in range
        updatePosition();
	}

    public void takeDamage()
    {
        hp -= 1;

        // should depend on weapon strength - as well as knock back
        Game.Instance.getManager().cameraShake.Shake(0.025f, 0.01f);

        if (hp <= 0)
        {
            Game.Instance.getManager().sleepTime(30);
            DestroySelf();
        }
        else
        {
            excecuteDamageEffect();
            animator.SetTrigger("TookDamage");
        }

    }

    void excecuteDamageEffect()
    {
        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();

        Game.Instance.getManager().sleepTime(20);
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

        expl.transform.localScale = transform.localScale * 4;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision enter");
    }

}
