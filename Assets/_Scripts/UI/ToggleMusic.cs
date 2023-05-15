using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private GameObject musicOnButton;
    [SerializeField] private GameObject musicOffButton;
    private GameObject soundManagerGO;
    private AudioSource[] audioSources;
    private AudioSource musicThemeSource;

    private void Start()
    {
        soundManagerGO = FindObjectOfType<SoundManager>().gameObject;
        audioSources = soundManagerGO.GetComponentsInChildren<AudioSource>();
        foreach(AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "MusicTheme")
            {
                musicThemeSource = audioSource;
                break;
            }
        }
    }

    public void OnButtonClickOff()
    {
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);
        musicThemeSource.volume = 0f;
    }

    public void OnButtonClickOn()
    {
        musicOffButton.SetActive(false);
        musicOnButton.SetActive(true);
        musicThemeSource.volume = .1f;
    }
}
