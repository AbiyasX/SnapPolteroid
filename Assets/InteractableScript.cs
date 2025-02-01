using Unity.VisualScripting;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [SerializeField] bool IsInteacting = false;
    [SerializeField] float rotationSpeed = 5f;
    private Transform cameraTransform;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;



        GameObject cameraObject = GameObject.FindGameObjectWithTag("Camera");
        cameraTransform = cameraObject.transform;
    }

    private void OnMouseDown()
    {
        InspectHandler.Instance.Inspection(transform);
        IsInteacting = true;
    }

    private void Update()
    {
        rotateObject(IsInteacting);
        ResetTransform();
    }

    private void ResetTransform()
    {
        if (!IsInteacting) 
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
        
    }
    

    private void rotateObject(bool active)
    {
            
       if (active && Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;


            transform.Rotate(cameraTransform.up, mouseX, Space.World);
            transform.Rotate(cameraTransform.right, mouseY, Space.World);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsInteacting = false;
            ResetTransform();
        }
    }
}
