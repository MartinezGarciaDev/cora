using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class DragAndDropTouch : Singleton<DragAndDropTouch>
{
    [SerializeField]
    private float dragPhysicsSpeed = 10;
    [SerializeField]
    private float dragSpeed = .1f;
    [SerializeField]
    private float focusDistance = 2.29f;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private TouchControls touchControls;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
        touchControls = new TouchControls();

    }
    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch start position = " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null) OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);

        Ray ray = mainCamera.ScreenPointToRay(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && ((hit.collider.gameObject.CompareTag("Draggable")) ||
                hit.collider.gameObject.layer == LayerMask.NameToLayer("Draggable") || hit.collider.gameObject.GetComponent<IDrag>() != null))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch end position = " + touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        if (OnEndTouch != null) OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.onStartDrag();
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        
        while (touchControls.Touch.TouchPress.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(focusDistance) - clickedObject.transform.position;
                rb.velocity = direction * dragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(focusDistance), ref velocity, dragSpeed);
                yield return null;
            }
        }

        iDragComponent?.onEndDrag();
    }
}
