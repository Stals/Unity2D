using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    GameObject weaponSlot;

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
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Debug.Log(mousePosition);

        float deltaY = mousePosition.y - transform.position.y;
        float deltaX = mousePosition.x - transform.position.x;

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2(deltaY, deltaX) * (180 / Mathf.PI);

        weaponSlot.transform.localEulerAngles = angle;
    }
}
