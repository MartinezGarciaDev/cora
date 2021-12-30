using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceOriginCheck : MonoBehaviour
{
    private PieceBehaviour pieceBehaviour;
    private Rigidbody parentRb;
    [SerializeField]
    private float limitVelocity = 0.3f;

    // Use this for initialization
    void Awake()
    {
        pieceBehaviour = GetComponentInParent<PieceBehaviour>();
        parentRb = GetComponentInParent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Trigger entered " + parentRb.velocity.magnitude.ToString());
        //checks tag, name_origin, broken state and velocity
        if ((other.CompareTag("Origin")) && 
            (this.name + "_origin" == other.gameObject.name) &&
            (pieceBehaviour.CurrentState == 1) &&
            (parentRb.velocity.magnitude <= limitVelocity))
        {
            pieceBehaviour.CurrentState = 2;
            //play sound
            //play particles
        }
    }
}
