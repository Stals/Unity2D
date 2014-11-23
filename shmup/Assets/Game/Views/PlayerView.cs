using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;


public partial class PlayerView { 

    /// Invokes AddMoneyExecuted when the AddMoney command is executed.
    public override void AddMoneyExecuted() {
        base.AddMoneyExecuted();

        audio.Play();
    }
 

    /// Subscribes to the property and is notified anytime the value changes.
    public override void healthChanged(Int32 value) {
        base.healthChanged(value);

        if (value <= 0) {
            SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
            Player.IsInvurnalable = false;
            renderer.enabled = false;
            //gameObject.SetActive(false);
        }
    }

    bool isPlayerDead()
    {
        return Player.health <= 0;
    }

    /// Subscribes to the property and is notified anytime the value changes.
    public override void IsInvurnalableChanged(Boolean value) {
        base.IsInvurnalableChanged(value);
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();

        if (value)
        {
            Observable.Interval(TimeSpan.FromMilliseconds(150)).Subscribe(l =>
            {
                // dirty hack
                if (!isPlayerDead())
                {
                    renderer.enabled = !renderer.enabled;
                }
                else {
                    renderer.enabled = false;
                }
               
            }).DisposeWhenChanged(Player.IsInvurnalableProperty);
        }
        else
        {
            renderer.enabled = !isPlayerDead();
        }

    }


    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float spread = 30f;

    [SerializeField]
    float recoil = 0.01f;

    Vector2 prevVector = new Vector2(0, 0);

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
        if (Player.health == 0)
        {
            return;
        }

        updateMovement();

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
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


        /*if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            movementVector.x = -1;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movementVector.x = 1;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movementVector.y = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movementVector.y = -1;
        }*/

        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        // if slowing down
        if(Mathf.Abs(prevVector.x) > Mathf.Abs(movementVector.x)){
            if(Mathf.Abs(movementVector.x) < 0.8f){
                movementVector.x /= 3f;
            }
        }

        if(Mathf.Abs(prevVector.y) > Mathf.Abs(movementVector.y)){
            if(Mathf.Abs(movementVector.y) < 0.8f){
                movementVector.y /= 3f;
            }
        }


        prevVector = movementVector;

        Vector3 translationDirection = new Vector3(movementVector.x, movementVector.y, 0);
        translationDirection = Vector3.ClampMagnitude(translationDirection, 1);

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(currentPos.x + (translationDirection.x * Player.movementSpeed),
                                         currentPos.y + (translationDirection.y * Player.movementSpeed));

    }
}
