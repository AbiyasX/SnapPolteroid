using UnityEngine;
using UnityEngine.EventSystems;

public class obtainableScript : MonoBehaviour
{
    [SerializeField] private Items assignedItem;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = GameObject.FindWithTag("Camera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform) 
                {
                   UI_InventorySystem.instance.additem(assignedItem);
                    if(UI_InventorySystem.instance.isInventoryFull == false)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}

