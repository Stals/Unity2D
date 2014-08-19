﻿using UnityEngine;
using System.Collections;

public enum PlayerDirection{
	Left,
	Right
}

public class Player : MonoBehaviour {

	[SerializeField]
	UIButton buttonLeft;
    bool buttonLeftPressed;

	[SerializeField]
	UIButton buttonRight;
    bool buttonRightPressed;

    private float currentAngle = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        updatePosition();
	}

	void FixedUpdate()
	{
        updateMove();
	}

    void updatePosition()
    {
        float currentRadius = getRadius();
        transform.position = new Vector3(currentRadius * Mathf.Cos(currentAngle), 
                                         currentRadius * Mathf.Sin(currentAngle));
    }

    void updateMove()
    {
        bool isLeft = isLeftDirectionActive ();
        bool isRight = isRightDirectionActive ();
        
        if (isLeft && isRight) {
            shoot ();

        } else if (isLeft) {
            move(PlayerDirection.Left);
            
        } else if (isRight) {
            move(PlayerDirection.Right);
        }
    }

	bool isLeftDirectionActive()
	{
        return (Input.GetKey(KeyCode.LeftArrow)) ||
            buttonLeftPressed;
	}

	bool isRightDirectionActive()
	{
        return (Input.GetKey(KeyCode.RightArrow)) ||
            buttonRightPressed;
	}

	void shoot()
	{
	}

    void move(PlayerDirection direction)
    {
        if (direction == PlayerDirection.Right)
        {
            currentAngle += getSpeed();
            Debug.Log("LEFT");
        } else
        {
            currentAngle -= getSpeed();
            Debug.Log("RIGHT");
        }

        // TODO помня текущий радиус
        // причем наверное он двигается в бит - а круги двигаются испольуя его отклонение от радиуса?
        // тогда все могу тупо умножать нечто на переменну в игроке * на коэф если нужно
    }


    // должен быть способ лучше мне кажется
    // как минимум вынести в отдельный класс это дело
    public void onDirButtonPress(UIButton button)
    {
        if (button == buttonLeft){
            buttonLeftPressed = true;

        } else if (button == buttonRight){
            buttonRightPressed = true;
        }
    }

    public void onDirButtonRelease(UIButton button)
    {
        if (button == buttonLeft){
            buttonLeftPressed = false;
            
        } else if (button == buttonRight){
            buttonRightPressed = false;
        }
    }

    float getRadius()
    {
        return 2f;//innerCircle.radiusX += 5;
    }

    float getSpeed()
    {
        return 0.05f;
    }
}
