using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] private GameObject soundOnButton;
    [SerializeField] private GameObject soundOffButton;
    private GameObject soundManagerGO;
    private AudioSource[] audioSources;

    private void Start()
    {
        soundManagerGO = FindObjectOfType<SoundManager>().gameObject;
        audioSources = soundManagerGO.GetComponentsInChildren<AudioSource>();
    }

    public void OnButtonClickOff()
    {
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name != "MusicTheme")
            {
                audioSource.volume = 0f;
            }
        }
    }

    public void OnButtonClickOn()
    {
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name != "MusicTheme")
            {
                audioSource.volume = .1f;
            }
        }
    }
}
