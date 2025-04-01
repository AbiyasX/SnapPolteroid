using Unity.Cinemachine;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour
{
    [SerializeField] CinemachineCamera StartCam;
    [SerializeField] CinemachineCamera SettingsCam;
    [SerializeField] CinemachineCamera ExitCam;

    public void StartCamera(bool _active)
    {
        if (_active)
        {
            StartCam.Priority = 1;
        }
        else
        {
            StartCam.Priority = 0;
        }
    }


    public void SettingsCamera(bool _active)
    {
        if (_active)
        {
            SettingsCam.Priority = 1;
        }
        else
        {
            SettingsCam.Priority = 0;
        }
    }

    public void ExitCamera(bool _active)
    {
        if (_active)
        {
            ExitCam.Priority = 1;
        }
        else
        {
            ExitCam.Priority = 0;
        }
    }
}
