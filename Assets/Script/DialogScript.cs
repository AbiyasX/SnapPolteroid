using Unity.Cinemachine;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
   [SerializeField] DialogData dialogData;
   [SerializeField] CinemachineCamera playerCam;
   [Tooltip("Add NPC Camera if needed")]
   [SerializeField] CinemachineCamera npcCam;


    public void runDialog()
    {
        DialogSystem.instance.activateDialog(true);
        DialogSystem.instance.addDialogData(dialogData, playerCam, npcCam);
    }
}
