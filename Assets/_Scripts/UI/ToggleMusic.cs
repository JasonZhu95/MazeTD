using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMusic : MonoBehaviour
{
    [SerializeField] private GameObject musicOnButton;
    [SerializeField] private GameObject musicOffButton;

    public void OnButtonClickOff()
    {
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);
    }

    public void OnButtonClickOn()
    {
        musicOffButton.SetActive(false);
        musicOnButton.SetActive(true);
    }
}
