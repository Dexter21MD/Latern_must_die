using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    [SerializeField] float moveDistance = 2f;

    [SerializeField] float restDistance = 1f;

    [SerializeField] GameObject laternToFollow;

    Rigidbody2D rb;
    
    //To avoid big numbers in serializeFields affected by Time.FixedDeltaTime 
    
    const float VALUE_INSPECTOR_MULTIPLIER = 10.00f;
    bool isMoving;
    bool isResting;
    float directionX;

    private void Awake() 
    {
        moveSpeed *= VALUE_INSPECTOR_MULTIPLIER;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        isMoving  = IsClose(laternToFollow.transform.position,moveDistance);
        isResting = IsClose(laternToFollow.transform.position,restDistance);        
    }

    private void FixedUpdate() 
    {
        if (isResting)
        {
            ResetVelocity();
        }
        else if (isMoving)
        {
            Move(laternToFollow.transform.position);    
        }
        else
        {
            ResetVelocity();
        }
    }

    private void Move(Vector2 destination)
    {
        Vector3 moveDir = destination - rb.position;
        moveDir = moveDir.normalized;
        directionX = Mathf.Sign(moveDir.x);
        rb.velocity = moveSpeed * Time.fixedDeltaTime * moveDir;
    }

    private void ResetVelocity()
    {
        rb.velocity = new Vector2(0,0);
    }

    private bool IsClose(Vector3 destinationPos, float checkDistance)
    {
        float distance = Vector3.Distance(destinationPos,rb.position);
        return distance <= checkDistance;
    }

    public bool GetMoveStatus()
    {
        return isMoving;
    }

    public bool GetRestStatus()
    {
        return isResting;
    }

    public float GetDirectionX()
    {
        return directionX;
    }
}
