using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerGuiController))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    public WeaponSlotController weaponSlot;

    Vector2 prevVector = new Vector2(0, 0);

    [SerializeField]
    bool useGamepad = false;

    WeaponTrail trail;
    PlayerGuiController guiController;

    int money;
    int currentHP;

    public void Awake()
    {
        Game.Instance.setPlayer(this);
    }

	// Use this for initialization
	void Start () {

        guiController = GetComponent<PlayerGuiController>();
       // trail = GetComponentInChildren<WeaponTrail>();
        currentHP = getMaxHP();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        updateMovement();

        weaponSlot.updateWeaponAngle(useGamepad);

       /* trail.Itterate(Time.deltaTime);
        trail.UpdateTrail(Time.time, Time.deltaTime);*/
    }



    void updateMovement()
    {
        Vector2 movementVector = new Vector2(0, 0);

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        // if slowing down
        if (Mathf.Abs(prevVector.x) > Mathf.Abs(movementVector.x))
        {
            if (Mathf.Abs(movementVector.x) < 0.8f)
            {
                movementVector.x /= 3f;
            }
        }

        if (Mathf.Abs(prevVector.y) > Mathf.Abs(movementVector.y))
        {
            if (Mathf.Abs(movementVector.y) < 0.8f)
            {
                movementVector.y /= 3f;
            }
        }


        prevVector = movementVector;

        Vector3 translationDirection = new Vector3(movementVector.x, movementVector.y, 0);
        translationDirection = Vector3.ClampMagnitude(translationDirection, 1);

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + (translationDirection.x * 0.1f),
                                         currentPos.y + (translationDirection.y * 0.1f));

    }

    public void addMoney(int m) {
        money += m;
        guiController.addMoney(m);
    }

    public int getMaxHP()
    {
        return 4;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            if (collision.gameObject.GetComponent<EnemyController>().movementType != MovementBehaviourType.Ranged)
            {
                takeDamage(1);
            }
        }
        else if (collision.gameObject.tag == "Bullet") {
            takeDamage(1);
            Destroy(collision.gameObject);
        }

        //Debug.Log("collision enter");
    }

    void playTakeDamageEffect()
    {
        Game.Instance.getManager().sleepTime(25);

        // should depend on weapon strength - as well as knock back
        Game.Instance.getManager().cameraShake.Shake(0.6f, 0.04f);

        MyGameManager manager = Game.Instance.getManager();
        if (manager)
        {
            manager.grid.AddGridForce(manager.playerController.transform.position, 0.2f, 0.7f, Color.green, true);
        }
        // TODO play sound
        // TODO - force all enemies away (that are closer than N)
    }

    public void takeDamage(int damage) {
        currentHP -= damage;
        // TODO update UI
        
        playTakeDamageEffect();
    }
}
