using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour, IDrag
{
    //private int[] states = { 0, 1, 2, 3 };
    private int startingState = 0;
    private int currentState = 0;
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Awake()
    {
        //get original position and rotation
        originalPosition = transform.position;
        originalRotation = Quaternion.identity;
        //set state 0
        currentState = startingState;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //frozen on air, full light, heartbeat pulse, stays for 1 second and falls with Rigidbody
        //broken state (1), tag draggable, light off, gray
        //solved(2), light on, pulse soft, tag solved
        //all_solved(3), -> full heart: light on+, pulse as a heart beat. All pieces tag = 
        switch (currentState)
        {
            // idle
            case 0:
                Debug.Log("state 0 = idle");
                break;
            // broken
            case 1:
                Debug.Log("state 1 = broken");
                break;
            // solved
            case 2:
                Debug.Log("state 2 = solved");
                transform.position = originalPosition;
                transform.rotation = originalRotation;

                break;
            // all_solved
            case 3:
                Debug.Log("state 3 = all_solved");
                break;
        }
    }

    //public int CurrentState { get; set; }
    public int CurrentState   // property
    {
        get { return currentState; }   // get method
        set { currentState = value; }  // set method
    }

    public void onStartDrag()
    {
        //disable gravity
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //Freeze x and z rotation axis. Reset x z rotation to origin? Make the object parallel to camera plane
        // POLISH use coroutine to make the rotation meanwhile?
        
        transform.rotation = originalRotation;
    }

    public void onEndDrag()
    {
        //enable gravity, frozen axis
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
        //check collision, if = original position, state becomes solved
    }


    private void OnTriggerStay(Collider other)
    {
        //if state1 and stays on the original position if ((tag == puzzleposition) && (currentposition == originalposition O ALGO ASI))  with a low velocity, changes to state2 solved
        //if changed to state solved(2), play a soft glass sound (wine glass tapped), send notification to ♥
        //if all pieces are solved, go for state all_solved (3)
    }
}
