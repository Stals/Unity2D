﻿using UnityEngine;
using System.Collections;

enum SizeType {
    Small,
    Normal,
    Big    
};

enum MovementBehaviourType {
    Follow, // steering
    Bounce, // bounces of walls
    Ranged, // gets in range and shoots, slowly moves away if you approach
    Evade //runs away (comes from back)
};

public class MovementBehaviour {

    public MovementBehaviour(EnemyController enemy)
    {
        _self = enemy;
        target = Game.Instance.getPlayer().gameObject;
        _go = enemy.gameObject;
    }

    public virtual void updatePosition() { }
    public virtual void updateRotation() { }

    protected GameObject  _go;
    protected GameObject target;
    protected EnemyController _self;
};

public class BounceMovementBehaviour : MovementBehaviour
{
    public BounceMovementBehaviour(EnemyController enemy)
        : base(enemy)
    { }

    public override void updatePosition() { }
    public override void updateRotation() { }

};

public class FollowMovementBehaviour : MovementBehaviour
{
    public FollowMovementBehaviour(EnemyController enemy)
        : base(enemy)
    { }
    public override void updatePosition() {
        _go.transform.Translate(_self.movementSpeed, 0, 0);    
    }

    public override void updateRotation() {

        Vector2 vectorL = target.transform.position - _go.transform.position;

        float angleZ = Mathf.Rad2Deg * (Mathf.Atan2(vectorL.y, vectorL.x));

        _go.transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

};


public class EnemyController : MonoBehaviour {

    [SerializeField]
    GameObject explosionPrefab;

    [SerializeField]
    int hp = 3;

    [SerializeField]
    public float movementSpeed = 0.1f;

    [SerializeField]
    SizeType sizeType;

    [SerializeField]
    MovementBehaviourType movementType = MovementBehaviourType.Follow;

    MovementBehaviour movementBehaviour;

    Animator animator;


	// Use this for initialization

	void Start () {
        movementInit();
        sizeInit();

        movementBehaviour.updateRotation();

        animator = GetComponent<Animator>();
	}

    void movementInit()
    {
        switch(movementType){
            case MovementBehaviourType.Follow:
                movementBehaviour = new FollowMovementBehaviour(this);
                break;
            case MovementBehaviourType.Bounce:
                movementBehaviour = new BounceMovementBehaviour(this);
                break;
        }
    }

    void sizeInit()
    {
        switch (sizeType)
        {
            case SizeType.Small:
                transform.localScale = (transform.localScale / 2);
                hp /= 2;
                movementSpeed = 0.065f;
                break;

            case SizeType.Big:
                transform.localScale = (transform.localScale * 2);
                hp *= 3;
                movementSpeed = 0.015f;
                break;

            case SizeType.Normal:
                movementSpeed = 0.04f;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        movementBehaviour.updateRotation();

        // TODO: archers move only if not in range
        movementBehaviour.updatePosition();
	}

    public void takeDamage()
    {
        hp -= 1;

        // should depend on weapon strength - as well as knock back
        Game.Instance.getManager().cameraShake.Shake(0.05f, 0.02f);

        if (hp <= 0)
        {
            Game.Instance.getManager().sleepTime(30);


            VectorGrid grid1 = UnityEngine.Object.FindObjectOfType<VectorGrid>();
            grid1.AddGridForce(this.transform.position, 0.15f, 0.2f * (transform.localScale.x * 2) , GetComponent<SpriteRenderer>().color, true);
            
            DestroySelf();
        }
        else
        {
            VectorGrid grid1 = UnityEngine.Object.FindObjectOfType<VectorGrid>();
            grid1.AddGridForce(this.transform.position, 0.05f, 0.2f * (transform.localScale.x * 2), Color.black, false);

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
        //Debug.Log("collision enter");
    }

}
