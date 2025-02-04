using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Interact : MonoBehaviour
{
    
    public bool Active = false;
    public bool PlayerInside = false;
    public CinemachineCamera Cam;
    private CinemachineBrain Brain;
    private LayerMask layerMask;
    private GameObject playerGameObject;

    [SerializeField] UnityEvent StartTriggerEvent = new UnityEvent();
    [SerializeField] UnityEvent EndTriggerEvent = new UnityEvent();

    private void Awake()
    {
        Brain = GameObject.FindWithTag("Camera").GetComponent<CinemachineBrain>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player"); 
    }

    private void Update()
    {
        if (PlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            CameraToggle();
        }   
    }

    public void CameraToggle()
    {
        Active = !Active;
        
        if (Active)
        {
            Cam.Priority = 1;
            StartCoroutine(WaitForBlendToComplete(false));
            StartTriggerEvent.Invoke();
        }
        else
        {
            Cam.Priority = 0;
            StartCoroutine(WaitForBlendToComplete(true));
            EndTriggerEvent.Invoke();
        }
    }

    private IEnumerator WaitForBlendToComplete(bool enablePlayer)
    {
        playerGameObject.SetActive(false);

        yield return new WaitUntil(() => Brain.IsBlending); 
        yield return new WaitUntil(() => !Brain.IsBlending);

        if (enablePlayer)
        {
            Cursor.lockState = CursorLockMode.Locked;
            playerGameObject.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            PlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & layerMask) != 0)
        {
            PlayerInside = false;
        }
    }
}
