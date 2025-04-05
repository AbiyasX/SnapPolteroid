using UnityEngine;

public class AddVFXScript : MonoBehaviour
{
    [SerializeField] Transform vfxTransform;
    [SerializeField] ParticleSystem particleVFX;

    private void Start()
    {
        UI_InventorySystem.instance.addVFX(vfxTransform, particleVFX);
    }
}
