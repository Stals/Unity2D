using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerView { 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void IsInvurnalableChanged(Boolean value) {
        base.IsInvurnalableChanged(value);
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();

        if (value)
        {
            Observable.Interval(TimeSpan.FromMilliseconds(150)).Subscribe(l =>
            {
                renderer.enabled = !renderer.enabled;
            }).DisposeWhenChanged(Player.IsInvurnalableProperty);
        }
        else
        {
            renderer.enabled = true;
        }

    }


    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float spread = 30f;

    [SerializeField]
    float recoil = 0.01f;

    /// Invokes ShootExecuted when the Shoot command is executed.
    public override void ShootExecuted() {
        base.ShootExecuted();

        for (int i = 0; i < Player.bulletsPerShot; ++i) {
            createBullet();
        }

        Vector3 currentPosition = transform.position;
        transform.position = new Vector3(currentPosition.x - recoil, currentPosition.y);
    }

    void createBullet()
    {
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(-spread, spread));
        bullet.GetComponent<BulletView>().player = this;
    }

    public override void Awake()
    {
        base.Awake();

        GameSceneManager.player = this;

        gameObject.RotateBy(new Vector3(0, 0, 1f), 4f, 0f, EaseType.linear, LoopType.loop);
    }

    void FixedUpdate()
    {
        updateMovement();

        if (Input.GetKey(KeyCode.Space))
        {
            if (Player.canShoot)
            {
                ExecuteShoot();
            }
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
}
