using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    public WeaponSlotController weaponSlot;

    Vector2 prevVector = new Vector2(0, 0);

    [SerializeField]
    bool useGamepad = false;

    WeaponTrail trail;

    public void Awake()
    {
        Game.Instance.setPlayer(this);
    }

	// Use this for initialization
	void Start () {


       // trail = GetComponentInChildren<WeaponTrail>();
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
}
