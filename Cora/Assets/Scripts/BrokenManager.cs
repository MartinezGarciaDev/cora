using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenManager : MonoBehaviour
{
    private int startingState = 0;
    private int currentState = 0;

    public int SolvedPieces
    { get; set; } 

    [SerializeField]
    private GameObject[] pieces;
    private int totalPieces;

    void Awake()
    {
        currentState = 0;
        SolvedPieces = 0;
        totalPieces = pieces.Length;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("Amount of pieces solved:" + SolvedPieces.ToString());


        if (SolvedPieces == totalPieces)
        {
            currentState = 1;
        }

        switch (currentState)
        {
            // broken
            case 0:
                Debug.Log("state 0 = broken");
                foreach (GameObject piece in pieces)
                {
                    if (piece.GetComponentInChildren<PieceBehaviour>().CurrentState == 2)
                        SolvedPieces++;

                    else if (piece.GetComponentInChildren<PieceBehaviour>().CurrentState < 2)
                        SolvedPieces = 0;
                }

                break;
            // solved
            case 1:
                Debug.Log("state 1 = solved");

                break;
        }
    }


    public int CurrentState   // property
    {
        get { return currentState; }   // get method
        //set { currentState = value; }  // set method
    }


    void ResetIndex()
    {
        SolvedPieces = 0;
    }
}
