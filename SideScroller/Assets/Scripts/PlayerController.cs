﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

    public float gravity = 20;
    public float speed = 8;
    public float acceleration = 30;
    public float jumpHeight = 12;

    private float currentSpeed;
    private float targetSpeed;
    public Vector2 amountToMove;

    private PlayerPhysics playerPhysics;

	void Start () {
        playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	void Update () {
        targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
        currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);

        if (playerPhysics.grounded)
        {
            amountToMove.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                amountToMove.y = jumpHeight;
            }
        }

        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        playerPhysics.Move(amountToMove * Time.deltaTime);
	}

    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }
}
