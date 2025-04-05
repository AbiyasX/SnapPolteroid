using UnityEngine;
using UnityEngine.Events;

public class BirdCage : MonoBehaviour
{
    [SerializeField] UnityEvent isUnlocked;
    [SerializeField] UnityEvent islocked;
    public void interact()
    {
        if (UI_InventorySystem.instance.HasItem("BirdSeed")
                && UI_InventorySystem.instance.HasItem("Feather")
                && UI_InventorySystem.instance.HasItem("Book"))
        {
            isUnlocked.Invoke();
        }
        else
        {
            islocked.Invoke();
        };
    }
}
