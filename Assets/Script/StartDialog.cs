using UnityEngine;
using UnityEngine.Events;

public class StartDialog : MonoBehaviour
{
    [SerializeField] UnityEvent onstartEvent;

    private void Start()
    {
        onstartEvent.Invoke();
    }

}
