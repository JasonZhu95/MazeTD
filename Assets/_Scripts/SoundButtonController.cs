using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonController : MonoBehaviour
{
    [SerializeField] private GameObject musicOn;
    [SerializeField] private GameObject musicOff;
    [SerializeField] private GameObject soundOn;
    [SerializeField] private GameObject soundOff;
    private GameObject soundManagerGO;
    private AudioSource[] audioSources;

    private void OnEnable()
    {
        soundManagerGO = FindObjectOfType<SoundManager>().gameObject;
        audioSources = soundManagerGO.GetComponentsInChildren<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.clip.name == "MusicTheme")
            {
                if (audioSource.volume == 0f)
                {
                    musicOn.SetActive(false);
                    musicOff.SetActive(true);
                }
            }
            if (audioSource.clip.name == "placeBuilding")
            {
                if (audioSource.volume == 0f)
                {
                    soundOn.SetActive(false);
                    soundOff.SetActive(true);
                }
            }
        }
    }
}
