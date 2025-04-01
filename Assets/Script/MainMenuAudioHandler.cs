using UnityEngine;

public class MainMenuAudioHandler : MonoBehaviour
{
    [Header("Start SFX")]
    [SerializeField] AudioClip HoverClip;
    [SerializeField] AudioClip SelectClip;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void selectSFX()
    {
        audioSource.clip = SelectClip;
        audioSource.Play();
    }

    public void HoverSFX()
    {
        audioSource.clip = HoverClip;
        audioSource.Play();
    }

}
