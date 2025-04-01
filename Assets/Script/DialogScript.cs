using Unity.Cinemachine;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
   [SerializeField] DialogData dialogData;
   [SerializeField] CinemachineCamera cam;


    public void runDialog()
    {
        DialogSystem.instance.addDialogData(dialogData, cam);
    }
}
