using UnityEngine;
using System.Collections;

public class WeaponSlotController : MonoBehaviour {

    public void updateWeaponAngle(float anglePerSecond, bool useGamepad)
    {
        float newAngle = getNewWeaponAngle(useGamepad).z;
        float currentAngle = gameObject.transform.localEulerAngles.z;

        float timeDelta = getAngleDelta(newAngle, currentAngle) / anglePerSecond;

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
            return getNewWeaponAngleGamepad();
        }
        else
        {
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
        float rotationY = -Input.GetAxis("Joy Y");

        // do not move sword if stick released
        if (rotationX == 0 && rotationY == 0)
        {
            return gameObject.transform.localEulerAngles;
        }

        Vector3 angle = Vector3.zero;
        angle.z = Mathf.Atan2(rotationY, rotationX) * (180 / Mathf.PI);
        return angle;
    }
}
