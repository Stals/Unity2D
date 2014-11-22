using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerView {

    [SerializeField]
    GameObject bulletPrefab;

    public override void Awake()
    {
        base.Awake();

        gameObject.RotateBy(new Vector3(0, 0, 1f), 4f, 0f, EaseType.linear, LoopType.loop);
    }

    void FixedUpdate()
    {
        updateMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            shoot();
        }
    }

    void updateMovement()
    {
        Vector2 movementVector = new Vector2(0, 0);

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        Vector3 translationDirection = new Vector3(movementVector.x, movementVector.y, 0);
        translationDirection = Vector3.ClampMagnitude(translationDirection, 1);

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + (translationDirection.x * Player.movementSpeed),
                                         currentPos.y + (translationDirection.y * Player.movementSpeed));

    }

    void shoot()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(-30f, 30f));
        bullet.GetComponent<BulletView>().player = this;
    }
}
