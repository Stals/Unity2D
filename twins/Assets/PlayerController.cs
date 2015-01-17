using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject weaponSlot;

    [SerializeField]
    float anglePerSecond = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        updateWeaponAngle();
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

        Debug.Log("1" + dAngle1 + " 2 " + dAngle2 + " d " + delta);

        return delta;
    }

    Vector3 getNewWeaponAngle()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float deltaY = mousePosition.y - transform.position.y;
        float deltaX = mousePosition.x - transform.position.x;

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2(deltaY, deltaX) * (180 / Mathf.PI);
        return angle;
    }
}
