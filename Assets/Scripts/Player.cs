using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : PathAlignedObject
{
    Rigidbody rb;
    Animator ani;

    [Header("Movement")]
    public float moveMaxSpeed, moveAccel;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Floor Check")]
    
    public Transform floorCheck;
    public LayerMask floorLayer;

    public bool onFloor;

    [Header("Drag")]
    [Range(0f, 1f)]
    public float floorDrag;
    [Range(0f, 1f)]
    public float airDrag;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
    }

    Collider[] cols = new Collider[10];

    private void Update()
    {
        if (Physics.OverlapSphereNonAlloc(floorCheck.position, 0.1f, cols, floorLayer) > 0)
            onFloor = true;
        else
            onFloor = false;


        ani.SetBool("OnFloor", onFloor);
        ani.SetFloat("velY", rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W))
            Jump();
    }

    private void Jump()
    {
        if (onFloor)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            ani.Play("Player_Jump", 0, 0f);
        }
    }

    private void FixedUpdate()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        Vector2 simplifiedVel = rb.velocity;
        simplifiedVel.y = 0;
        if (simplifiedVel.magnitude < moveMaxSpeed)
        {
            rb.velocity += hor * Right * moveAccel;
        }

        rb.velocity = new Vector3(rb.velocity.x * (1 - (onFloor ? floorDrag : airDrag)), rb.velocity.y, rb.velocity.z * (1 - (onFloor ? floorDrag : airDrag)));

        ForceIntoPath((pos) => rb.MovePosition(new Vector3(pos.x, transform.position.y, pos.z)));
    }
}
