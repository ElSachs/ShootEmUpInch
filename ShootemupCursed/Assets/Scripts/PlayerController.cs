using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 input;
    private Vector3 move;
    [SerializeField] float decelerationSpeed;
    [SerializeField] private float maxSpeed;
    private Rigidbody2D self;

    private void Start()
    {
        self = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        if (input != Vector3.zero)
        {
            move = input;
            Debug.Log("je bouge");
        }
        else
        {
            move = Vector3.Lerp(move, Vector3.zero, decelerationSpeed * Time.deltaTime);
        }


        self.velocity = move * maxSpeed * Time.deltaTime;
    }
}
