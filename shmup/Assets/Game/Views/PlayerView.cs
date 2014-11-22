using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerView {

    void FixedUpdate()
    {
        updateMovement();

        /*if (Input.GetKeyDown(KeyCode.Space))
        {

        }*/
    }

    void updateMovement()
    {
        Vector2 movementVector = new Vector2(0, 0);

       /* if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movementVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementVector.x = 1;
        }*/

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        /*
        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + (movementVector.x * Player.movementSpeed),
                                         currentPos.y + (movementVector.y * Player.movementSpeed));*/

        Vector3 translationDirection = new Vector3(movementVector.x, movementVector.y, 0);
        translationDirection = Vector3.ClampMagnitude(translationDirection, 1);

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + (translationDirection.x * Player.movementSpeed),
                                         currentPos.y + (translationDirection.y * Player.movementSpeed));

    }

}
