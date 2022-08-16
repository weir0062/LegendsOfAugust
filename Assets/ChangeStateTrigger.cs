using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeStateTrigger : MonoBehaviour
{

    public DropState stateToChangeTo = DropState.Water;
    BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (pc) 
        {
            pc.ChangeState(stateToChangeTo);
        }
    }
}
