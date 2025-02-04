using Unity.Cinemachine;
using UnityEngine;

public class CameraSceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CinemachineCamera[] cam;
    public bool ActiveCam;

    private void Update()
    {
        switchScene();
    }

    public void switchScene()
    {
        if (ActiveCam)
        {
            cam[0].Priority = 1;
            cam[1].Priority = 0;
        }
        else
        {
            cam[0].Priority = 0;
            cam[1].Priority = 1;
        }
        
    }
}
