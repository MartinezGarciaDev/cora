using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehaviour : MonoBehaviour, IDrag
{
    [SerializeField]
    private GameObject origin;
    //[SerializeField]
    //private GameObject brokenManager;
    [SerializeField]
    private Material[] statesMaterials;

    //private int[] states = { 0, 1, 2, 3 };
    private int startingState = 0;
    private int currentState = 0;
    
    private Rigidbody rb;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Awake()
    {
        //get original position and rotation
        originalPosition = origin.transform.position;
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
                gameObject.tag = "Draggable";
                this.GetComponent<MeshRenderer>().material = statesMaterials[0];
                break;
            // broken
            case 1:
                Debug.Log("state 1 = broken");
                gameObject.tag = "Draggable";
                break;
            // solved
            case 2:
                Debug.Log("state 2 = solved");
                gameObject.tag = "Solved";
                //change material (get from manager)
                this.GetComponent<MeshRenderer>().material = statesMaterials[1];
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
        // POLISH use coroutine to make the rotation meanwhile?

        // if idle, become broken
        if (currentState == 0)
            currentState = 1;

        transform.rotation = originalRotation;
    }

    public void onEndDrag()
    {
        //after broken and not solved, become idle
        if (currentState == 1)
            currentState = 0;
        //enable gravity, frozen axis
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.zero;
    }

 /*   if stays on the ground, don't let it move?
    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Floor"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        
        //if changed to state solved(2), play a soft glass sound (wine glass tapped), send notification to coramanager
        //if all pieces are solved, go for state all_solved (3)
    }

    private void OnCollisionStay(Collision collision)
    {
        //make it draggable only if it's the piece on top of everything else
    }
}
