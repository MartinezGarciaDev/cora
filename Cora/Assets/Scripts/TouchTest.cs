using UnityEngine;

public class TouchTest : MonoBehaviour
{
    [SerializeField]
    private GameObject managerObject;
    private InputManager inputManager;
    private Camera cameraMain;

    private void Awake()
    {
        inputManager = managerObject.GetComponent(typeof(InputManager)) as InputManager;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {
        // Use this to Move dropped fragments to focus plane
        // Check this when the movement happens
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        
        transform.position = worldCoordinates;
    }
}
