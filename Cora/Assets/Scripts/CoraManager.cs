using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject managerObject;
    private InputManager inputManager;
    private Camera cameraMain;

    [SerializeField]
    private Material[] material;

    public GameObject fullHeart;
    public GameObject[] brokenHearts;

    public GameObject currentHeart;

    private int brokenHeartsIndex = 0;
    private int brokenHeartsTotal;
    private int solvedHearts = 0;

    private bool standby = true;


    private void Awake()
    {
        //inputManager = managerObject.GetComponent(typeof(InputManager)) as InputManager;
        inputManager = InputManager.Instance;
        cameraMain = Camera.main;
        currentHeart = fullHeart;

        standby = true;

        brokenHeartsTotal = brokenHearts.Length;
    }

    void Update()
    {
        //Full heart, wait and change
        if ((currentHeart == fullHeart) && (standby == true))
        {
            standby = false;
            StartCoroutine(WaitCoroutineBreak(2f));
        }

        //Restore heart and swap the next broken heart
        else if ((currentHeart != fullHeart) && (brokenHeartsIndex < brokenHeartsTotal))
        {
            //check if broken heart is solved
            if ((currentHeart.GetComponent<BrokenManager>().CurrentState == 1) && (standby == false))
            {
                standby = true;
                RestoreHeart();
            }    
        }

        else if ((currentHeart != fullHeart) && (brokenHeartsIndex == brokenHeartsTotal))
        {
            LastHeart();
        }
    }

    /*    private void OnEnable()
        {
            inputManager.OnStartTouch += Move;
        }
        private void OnDisable()
        {
            inputManager.OnEndTouch -= Move;
        }
    */
    /*    private void Change(Vector2 screenPosition, float time)
        {
            //if (fullHeart.activeSelf == true)
            if (currentHeart != fullHeart)
            {
                currentHeart.SetActive(false);
                fullHeart.SetActive(true);

                //nextheart
                Debug.Log("brokenHeart");
            }

            if (currentHeart == fullHeart)
            {
                fullHeart.SetActive(false);
                brokenHearts[0].SetActive(true);
                currentHeart = brokenHearts[0];

                Debug.Log("fullHeart");
            }
        }*/

    /*    public void Move(Vector2 screenPosition, float time)
        {
            // Use this to Move dropped fragments to focus plane
            // Check this when the movement happens
            Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
            Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
            worldCoordinates.z = 0;


            if (currentHeart == fullHeart)
            {
                BreakHeart();
            }

            else if ((currentHeart != fullHeart) && (brokenHeartsIndex < brokenHearts.Length))
            {
                RestoreHeart();
            }

            else if ((currentHeart != fullHeart) && (brokenHeartsIndex == brokenHearts.Length))
            {
                LastHeart();
            }
            //transform.position = worldCoordinates;
        }*/

    public void BreakHeart()
    {
        fullHeart.SetActive(false);
        brokenHearts[brokenHeartsIndex].SetActive(true);
        currentHeart = brokenHearts[brokenHeartsIndex];

        Debug.Log("brokenHeart index is " + brokenHeartsIndex);

        brokenHeartsIndex++;
    }

    public void RestoreHeart()
    {
        currentHeart.SetActive(false);
        currentHeart = fullHeart;
        currentHeart.SetActive(true);

        Debug.Log("fullHeart");
    }

    public void LastHeart()
    {
        Debug.Log("lastHeart, unsolvable :(");
    }

    IEnumerator WaitCoroutineBreak(float seconds)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);
        BreakHeart();
        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished BreakCoroutine at timestamp : " + Time.time);
    }
}
