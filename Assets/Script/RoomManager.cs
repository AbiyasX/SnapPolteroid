using Unity.Cinemachine;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Transform Player;

    [SerializeField] CinemachineCamera LivingRoomCam;
    [SerializeField] CinemachineCamera KitchenRoomCam;
    [SerializeField] CinemachineCamera BedRoomCam;

    private void Start()
    {
        GotoRoom(1);
    }

    public void GotoRoom(int val)
    {
        CharacterController controller = Player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;
        switch (val){
            case 1:
                cameraPriority(1, 0, 0);
                Player.transform.position = new Vector3(0,-1.36f, 0.84f);
                break;
            case 2:
                cameraPriority(0, 1, 0);
                Player.transform.position = new Vector3(-55.24f, -1.36f, 0.84f);
                break;
            case 3:
                cameraPriority(0, 0, 1);
                Player.transform.position = new Vector3(66.86f, -1.36f, 0.84f);
                break;
        }
        if (controller != null) controller.enabled = true;
    }


    private void cameraPriority(int val1, int val2, int val3)
    {
        LivingRoomCam.Priority = val1;
        BedRoomCam.Priority = val2;
        KitchenRoomCam.Priority = val3;
    }
}
