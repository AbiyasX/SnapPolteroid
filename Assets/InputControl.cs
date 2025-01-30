
using DG.Tweening;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputControl : MonoBehaviour
{
    [Header("Player Configuration")]
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] GameObject PlayerGameObject;
    [SerializeField] Transform playerTransform;
    [SerializeField] float PlayerSpeed;
    [SerializeField] CinemachineCamera cam;


    private bool isInteract = false;
    private CharacterController charController;
    private InputSystem_Actions inputActions;
    private Vector2 inputVector;
    private float lastRotationZ;
    private float initialYPosition;

    private void Awake()
    {
        initialYPosition = transform.position.y;
        inputActions = new InputSystem_Actions();
        charController = GetComponent<CharacterController>();   
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();

    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        moveHandler();
    }

    public void moveHandler()
    {
        Vector3 move = new Vector3(inputVector.x, initialYPosition, inputVector.y).normalized * PlayerSpeed * Time.deltaTime;

        if (inputVector.x < 0) playerSprite.flipX = true;
        if (inputVector.x > 0) playerSprite.flipX = false;

        float targetRotationZ = inputVector.x * -5;
        if (Mathf.Abs(targetRotationZ - lastRotationZ) > 0.1f) // Prevent redundant calls
        {
            playerTransform.DOLocalRotate(new Vector3(0, 0, targetRotationZ), 0.5f)
                .SetEase(Ease.OutQuad);
            lastRotationZ = targetRotationZ;
        }

        charController.Move(move);
        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }
}
