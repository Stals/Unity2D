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

    public bool isVisible()
    {
        return _go.renderer.isVisible;
    }

    protected GameObject  _go;
    protected GameObject target;
    protected EnemyController _self;
};

public class BounceMovementBehaviour : MovementBehaviour
{
    private Vector3 directionVector;

    public BounceMovementBehaviour(EnemyController enemy)
        : base(enemy)
    {
        _go.RotateBy(new Vector3(0, 0, 0.5f), 1f, 0f, EaseType.linear, LoopType.loop);

        float x = Random.Range(0, 2) == 0 ? -0.5f : 0.5f;
        float y = Random.Range(0, 2) == 0 ? -0.5f : 0.5f;

        directionVector = new Vector3(x, y);
    }

    public override void updatePosition() 
    {
        float ScreenHeight = 2.0f * Camera.main.orthographicSize;
        float ScreenWidth = ScreenHeight * Camera.main.aspect;

        Vector3 goPosition = _go.transform.position;

        //  Camera.main.getswc
        Vector3 goSize = _go.renderer.bounds.size; // TODO* _go.transform.localScale;
        if ((goPosition.x - (goSize.x / 2) <= (-ScreenWidth / 2)) ||
            (goPosition.x + (goSize.x / 2) >= (ScreenWidth / 2)))
        {
            directionVector.x *= -1;
        }

        if ((goPosition.y - (goSize.y / 2) <= (-ScreenHeight / 2)) ||
            (goPosition.y + (goSize.y / 2) >= (ScreenHeight / 2)))
        {
            directionVector.y *= -1;
        }

        
        _go.transform.position = _go.transform.position + (directionVector * _self.movementSpeed);  
    }
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

    // TODO update with delay
    public override void updateRotation() {

        Vector2 vectorL = target.transform.position - _go.transform.position;

        float angleZ = Mathf.Rad2Deg * (Mathf.Atan2(vectorL.y, vectorL.x));

        _go.transform.eulerAngles = new Vector3(0, 0, angleZ);
    }

};

public class RangedMovementBehaviour : FollowMovementBehaviour
{
    enum state
    {
        Chasing,
        Inrange
    };
    state currentState;
    float range = 7.5f;
    float rangeDelta = 1.5f; // amount that enemy goes closer than range - to not move all the time

    public RangedMovementBehaviour(EnemyController enemy)
        : base(enemy)
    {
        currentState = state.Chasing;
    }
    public override void updatePosition() {

        if (currentState == state.Inrange) {
            if (!insideDistance(range)) {
                currentState = state.Chasing;
            }
        }else if (currentState == state.Chasing) {
            if (insideDistance(range - rangeDelta) && isVisible())
            {
                currentState = state.Inrange;
            }
            else
            {
                // only if not in range
                base.updatePosition();
            }
        }
    }

    public override void updateRotation() {
        base.updateRotation();

        // if (currentState == state.Inrange)
            //- shoot
    }

    bool insideDistance(float distance) {
        return Vector3.Distance(_self.transform.position, target.transform.position) <= distance;
    }

}




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
            case MovementBehaviourType.Ranged:
                movementBehaviour = new RangedMovementBehaviour(this);
                break;
            default:
                movementBehaviour = new MovementBehaviour(this);
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

        if (hp <= 0)
        {
            excecuteDamageEffect(true);
            DestroySelf();
        }
        else
        {
            excecuteDamageEffect(false);
            animator.SetTrigger("TookDamage");
        }

    }

    void excecuteDamageEffect(bool isKill)
    {
        float effectMultiplier = isKill ? 3 : 1;


        audio.pitch = Random.Range(0.8f, 1.2f);
        audio.Play();

        Game.Instance.getManager().sleepTime(isKill ? 30 : 20);

        VectorGrid grid1 = UnityEngine.Object.FindObjectOfType<VectorGrid>();
        grid1.AddGridForce(this.transform.position, 0.05f * effectMultiplier, 0.2f * (transform.localScale.x * 2), GetComponent<SpriteRenderer>().color, isKill);

        // should depend on weapon strength - as well as knock back
        Game.Instance.getManager().cameraShake.Shake(0.05f * effectMultiplier, 0.03f);
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
