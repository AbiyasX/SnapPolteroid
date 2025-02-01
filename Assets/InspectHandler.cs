using DG.Tweening;
using UnityEngine;

public class InspectHandler : MonoBehaviour
{
   
    public static InspectHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Inspection(Transform item)
    {
        item.DOMove(transform.position, 0.5f);
    }

}
