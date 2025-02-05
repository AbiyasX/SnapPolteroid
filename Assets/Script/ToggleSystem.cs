using UnityEngine;
using UnityEngine.Events;

public class ToggleSystem : MonoBehaviour
{


    [Header("Single Toggle")]
    [SerializeField] UnityEvent TriggerEvent = new UnityEvent();
    [Header("On/Off Toggle")]
    [SerializeField] UnityEvent TriggerEventOn = new UnityEvent();
    [SerializeField] UnityEvent TriggerEventOff = new UnityEvent();
    [SerializeField] public bool isActive;
    private void OnMouseDown()
    {
        Toggle();
        TriggerEvent.Invoke();
    }

    public void Active(bool _Active)
    {
        isActive = _Active;
    }

    private void Toggle()
    {
        isActive = !isActive;
        if (isActive)
        {
            TriggerEventOn.Invoke();
        }
        else
        {
            TriggerEventOff.Invoke();
        }
       
    }
}
