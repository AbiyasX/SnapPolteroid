using DG.Tweening;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [SerializeField] public bool isLock = false;

    public void DoorLock(bool _Active)
    {
        isLock = _Active;
    }

   

    public void InteractDoor(bool isOpen)
    {
        
        if (isOpen)
        {
            if(!isLock)transform.DOLocalRotate(new Vector3(0, -70, 0), 1f);
        }
        else
        {
            transform.DOLocalRotate(new Vector3(0, 0, 0), 1f);
        }
    }


}
