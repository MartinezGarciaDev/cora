using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject managerObject;
    private InputManager inputManager;
    private Camera cameraMain;

    public GameObject fullHeart;
    public GameObject[] brokenHearts;

    public GameObject currentHeart;

    //public List<int> brokenHeartsIndex;
    private int brokenHeartsIndex = 0;


    private void Awake()
    {
        //inputManager = managerObject.GetComponent(typeof(InputManager)) as InputManager;
        inputManager = InputManager.Instance;
        cameraMain = Camera.main;
        currentHeart = fullHeart;

        //int[] brokenHeartsIndex = new int[brokenHearts.Length];
        //for (int i = 0; i < brokenHearts.Length; i++)
        //{
        //    brokenHeartsIndex.Add(i);
        //}
    }

/*    private void Start()
    {
        for (int i = 0; i < brokenHearts.Length; i++)
        {
            brokenHeartsIndex[i] = i;
        }
    }*/

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

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

    public void Move(Vector2 screenPosition, float time)
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
    }

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
        Debug.Log("lastHeart, unsolvable");
    }
}
