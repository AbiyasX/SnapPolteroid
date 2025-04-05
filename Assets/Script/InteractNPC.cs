using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class InteractNPC : MonoBehaviour
{
    public bool Active = false;
    public bool PlayerInside = false;
    private LayerMask layerMask;
    private GameObject playerGameObject;

    [SerializeField] UnityEvent StartTriggerEvent = new UnityEvent();

    private void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        layerMask = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (PlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            toggleNPC();
        }
    }

    public void  toggleNPC()
    {
        StartTriggerEvent.Invoke();
    }

    public void isPlayerInside(bool active)
    {
        PlayerInside = active;
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
