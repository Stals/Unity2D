using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject weaponSlot;

    [SerializeField]
    float anglePerSecond = 1;

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


        trail = GetComponentInChildren<WeaponTrail>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void FixedUpdate()
    {
        updateMovement();

        updateWeaponAngle();


        trail.Itterate(Time.deltaTime);
        trail.UpdateTrail(Time.time, Time.deltaTime);
    }

    void updateWeaponAngle()
    {
        float newAngle = getNewWeaponAngle().z;
        float currentAngle = weaponSlot.transform.localEulerAngles.z;

        float timeDelta =  getAngleDelta(newAngle, currentAngle) / anglePerSecond;

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.LerpAngle(currentAngle, newAngle, 1f / timeDelta);

        weaponSlot.transform.localEulerAngles = angle;
    }

    float getAngleDelta(float dAngle1, float dAngle2)
    {
        float delta = Mathf.Max(dAngle1, dAngle2) - Mathf.Min(dAngle1, dAngle2);
        if (180 < delta) {
          delta = 360 - delta;
        
        }
        delta = Mathf.Abs(delta);

        return delta;
    }

    Vector3 getNewWeaponAngle()
    {
        if (useGamepad) {
            return getNewWeaponAngleGamepad();
        }else{
            return getNewWeaponAngleMouse();
        }
    }

    Vector3 getNewWeaponAngleMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float deltaY = mousePosition.y - transform.position.y;
        float deltaX = mousePosition.x - transform.position.x;

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2(deltaY, deltaX) * (180 / Mathf.PI);
        return angle;
    }

    Vector3 getNewWeaponAngleGamepad()
    {
        float rotationX = Input.GetAxis("Joy X");
        float rotationY = - Input.GetAxis("Joy Y");

        // do not move sword if stick released
        if (rotationX == 0 && rotationY == 0) {
            return weaponSlot.transform.localEulerAngles;
        }

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2(rotationY, rotationX) * (180 / Mathf.PI);
        return angle;
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
