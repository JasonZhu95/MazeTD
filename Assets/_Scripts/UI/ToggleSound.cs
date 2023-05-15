using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSound : MonoBehaviour
{
    [SerializeField] private GameObject soundOnButton;
    [SerializeField] private GameObject soundOffButton;

    public void OnButtonClickOff()
    {
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
    }

    public void OnButtonClickOn()
    {
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
    }
}
