﻿using UnityEngine;
using System.Collections;

public class WeaponSlotController : MonoBehaviour {

    public WeaponController currentWeapon = null;

    float previouslyReturnedAngle = -1;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            chooseRandomWeapon();
        }

    }

    public void Awake()
    {
        chooseRandomWeapon();
    }

    public void chooseRandomWeapon()
    {
        if (currentWeapon) {
            currentWeapon.gameObject.SetActive(false);
        }

        WeaponController[] weapons = GetComponentsInChildren<WeaponController>(true);

        //exclude current weapon from random, checking link, and re randoming
        WeaponController randomWeapon = null;
        do
        {
            int rndID = Random.Range(0, weapons.Length);
            randomWeapon = weapons[rndID];
        } while (randomWeapon == currentWeapon);

        randomWeapon.gameObject.SetActive(true);
        currentWeapon = randomWeapon;

        playChangeWeaponAnimation();
    }

    public void playChangeWeaponAnimation()
    {
        MyGameManager manager = Game.Instance.getManager();
        if (manager)
        {
            manager.grid.AddGridForce(manager.playerController.transform.position, 0.2f, 0.7f, Color.green, true);
        }
    }

    public void updateWeaponAngle(bool useGamepad)
    {
        float newAngle = getNewWeaponAngle(useGamepad).z;
        float currentAngle = gameObject.transform.localEulerAngles.z;

        float timeDelta = getAngleDelta(newAngle, currentAngle) / currentWeapon.anglePerSecond;

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.LerpAngle(currentAngle, newAngle, 1f / timeDelta);

        gameObject.transform.localEulerAngles = angle;
    }

    float getAngleDelta(float dAngle1, float dAngle2)
    {
        float delta = Mathf.Max(dAngle1, dAngle2) - Mathf.Min(dAngle1, dAngle2);
        if (180 < delta)
        {
            delta = 360 - delta;

        }
        delta = Mathf.Abs(delta);

        return delta;
    }

    Vector3 getNewWeaponAngle(bool useGamepad)
    {
        if (useGamepad)
        {
            previouslyReturnedAngle = getNewWeaponAngleGamepad();
        }
        else
        {
            previouslyReturnedAngle = getNewWeaponAngleMouse();
        }

        Vector3 angle = Vector3.zero;
        angle.z = previouslyReturnedAngle;

        return angle;
    }

    float getNewWeaponAngleMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        float deltaY = mousePosition.y - transform.position.y;
        float deltaX = mousePosition.x - transform.position.x;

        return Mathf.Atan2(deltaY, deltaX) * (180 / Mathf.PI);
    }

    float getNewWeaponAngleGamepad()
    {
        float rotationX = Input.GetAxis("Joy X");
        float rotationY = -Input.GetAxis("Joy Y");

        // do not move sword if stick released
        if (rotationX == 0 && rotationY == 0)
        {
            return gameObject.transform.localEulerAngles.z;
        }

        return  Mathf.Atan2(rotationY, rotationX) * (180 / Mathf.PI);
    }
}
