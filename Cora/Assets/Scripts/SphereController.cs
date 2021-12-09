using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour, IDrag
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void onStartDrag()
    {
        rb.useGravity = false;
        //Freeze x and z rotation axis. Reset x z rotation to origin? Make the object parallel to camera plane
    }
    public void onEndDrag()
    {
        rb.useGravity = true;
        rb.velocity = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
