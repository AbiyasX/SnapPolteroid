using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class CameraScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera cameraPOV;
    public float mouseSensitivity = 0.1f;
    public Transform playerBody;
    public InputSystem_Actions inputActions;
    public GameObject CameraUI;
    private CinemachinePanTilt povComponent;

    public float flashRange = 20f;
    public float flashAngle = 30f;
    public float flashCooldown = 2f;
    public LayerMask ghostLayer;

    private float lastFlashTime;

    [SerializeField] UnityEvent onCamera;
    [SerializeField] UnityEvent offCamera;

    private Vector2 lookInput;
    [SerializeField] private bool useCamera = false;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }
    void OnEnable()
    {
        povComponent = cameraPOV.GetComponent<CinemachinePanTilt>();
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;
        inputActions.Disable();
    }

    

    void Update()
    {
        usingCamera();
        if (!useCamera) return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) Flash();
        povComponent.PanAxis.Value += lookInput.x * mouseSensitivity;
        povComponent.TiltAxis.Value -= lookInput.y * mouseSensitivity;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void usingCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            useCamera = !useCamera;
            if (useCamera && UI_InventorySystem.instance.HasItem("PolaroidCamera"))
            {

                UI_InventorySystem.instance.Toggle_InvUI(false);
                CameraUI.SetActive(true);
                cameraPOV.Priority = 1;
                InputControl.Instance.playerMovement(false);
                onCamera.Invoke();
            }
            else
            {
                UI_InventorySystem.instance.Toggle_InvUI(true);
                CameraUI.SetActive(false);
                InputControl.Instance.playerMovement(true);
                cameraPOV.Priority = 0;
                offCamera.Invoke();
            }
        }
    }

    void Flash()
    {
        Debug.Log("Flash!");

        Collider[] hits = Physics.OverlapSphere(transform.position, flashRange, ghostLayer);

        foreach (Collider hit in hits)
        {
            Vector3 dirToGhost = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToGhost);

            if (angle < flashAngle)
            {
                if (Physics.Raycast(transform.position, dirToGhost, out RaycastHit rayHit, flashRange, ghostLayer))
                {
                    Ghost ghost = rayHit.collider.GetComponent<Ghost>();
                    if (ghost != null)
                    {
                        ghost.TakeDamage(20f);
                    }
                }
            }
        }
    }
}
