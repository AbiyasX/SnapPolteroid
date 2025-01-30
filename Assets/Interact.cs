using Unity.Cinemachine;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Interact : MonoBehaviour
{
    public bool Active = false;
    public bool PlayerInside = false;
    public CinemachineCamera Cam;
    private LayerMask layerMask;
    private GameObject playerGameObject;

    private void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player"); 
    }

    private void Update()
    {
        if (PlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }   
    }

    public void interact()
    {
        Active = !Active;
        
        if (Active)
        {
            playerGameObject.SetActive(false);
            Cam.Priority = 1;
        }
        else
        {
            playerGameObject.SetActive(true);
            Cam.Priority = 0;
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
