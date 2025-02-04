using UnityEngine;
using UnityEngine.Events;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] UnityEvent TriggerEvent = new UnityEvent();
    private void OnMouseDown()
    {
        TriggerEvent.Invoke();
    }

    
}
