using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropling : MonoBehaviour
{
    enum DropState { Water, Vapor, Ice };
    DropState currentState = DropState.Water;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveRight(Rigidbody rb, float xVal)
    {
        rb.AddForce(transform.forward * xVal);
    }

    public void Jump(Rigidbody rb, float yVal)
    {
        rb.AddForce(transform.up * yVal);
    }
}
