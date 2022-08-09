using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    enum DropState{ Water, Vapor, Ice };
    DropState currentState = DropState.Water;
    private Rigidbody rb;

    public bool bOnGround;
    //Height the player can jump
    public float jumpSpeed = 400.0f;
    //Movement speed of player
    public float moveSpeed = 5.0f;
    private float distToGround;
    private Vector3 gravityScale;
    public float targetVelocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        distToGround = gameObject.GetComponent<Collider>().bounds.extents.y;
        gravityScale = Physics.gravity;

        ChangeState(DropState.Water);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug State Change Buttons
        if (Input.GetKey(KeyCode.I)) { ChangeState(DropState.Ice);}
        if (Input.GetKey(KeyCode.O)) { ChangeState(DropState.Water);}
        if (Input.GetKey(KeyCode.P)) { ChangeState(DropState.Vapor);}

        //Ground check
        if (Physics.Raycast(transform.position, -transform.up, distToGround + 0.01f))
        {
            bOnGround = true;
        }
        else
        {
            bOnGround = false;
        }

        //State specific controls
        if (currentState == DropState.Water)
        {
           


            if (Input.GetKeyDown(KeyCode.Space) && bOnGround)
            {
                rb.AddForce(transform.up * jumpSpeed);
            }
            if (bOnGround && !Input.anyKey)
            {
                //rb.velocity = Vector3.zero;
            }
        }
        else if (currentState == DropState.Vapor)
        {
            
        }
        else if (currentState == DropState.Ice)
        {
            if (Input.GetKeyDown(KeyCode.Space) && bOnGround)
            {
                rb.AddForce(transform.up * jumpSpeed);
            }
        }

        //Default Controls shared by all states
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(transform.forward * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.forward * moveSpeed);
        }
        Vector3 vel = rb.velocity;
        if (rb.velocity.z < -targetVelocity) { rb.velocity.Set(vel.x, vel.y, -targetVelocity); }
        if (rb.velocity.z > targetVelocity) { rb.velocity.Set(vel.x, vel.y, targetVelocity); }

        //Test
        if (bOnGround && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && currentState == DropState.Water)
        {
            vel = rb.velocity;
            vel.Set(vel.x, vel.y, 0);
            rb.velocity.Set(vel.x, vel.y, vel.z);
        }
    }

    
    private void FixedUpdate()
    {
        if (currentState == DropState.Vapor)
        {

            if (!Physics.Raycast(transform.position, transform.up, distToGround + 0.01f))
            {
                //TODO: adjust force of gravity for comfortable feel
                rb.AddForce(-gravityScale, ForceMode.Acceleration);
            }
            else
            {
                //TODO: Add Timer so we can skirt across ceiling for a bit before we turn back into water form
                ChangeState(DropState.Water);
            }

        }
        
    }
    
    private void ChangeState(DropState state)
    {
        switch(state)
        {
            case DropState.Ice:
                rb.useGravity = true;
                rb.drag = 0.0f;
                rb.mass = 2;
                targetVelocity = 1;
                currentState = state;
                break;
            case DropState.Water:
                rb.useGravity = true;
                rb.drag = 0.5f;
                rb.mass = 1;
                targetVelocity = 2;
                currentState = state;
                break;
            case DropState.Vapor:
                rb.useGravity = false;
                currentState = state;
                break;
            default: break;
        }
    }
}
