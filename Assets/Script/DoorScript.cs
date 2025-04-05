using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class DoorScript : MonoBehaviour
{

    [SerializeField] public bool isLock = false;
    [SerializeField] public UnityEvent doorOpen;
    [SerializeField] public UnityEvent doorClose;
    public void DoorLock(bool _Active)
    {
        isLock = _Active;
    }

   

    public void InteractDoor(bool isOpen)
    {
        
        if (isOpen)
        {
            if (!isLock)
            {
                transform.DOLocalRotate(new Vector3(0, -70, 0), 1f);
                doorOpen.Invoke();
            }
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
            doorClose.Invoke();
        }
    }


}
