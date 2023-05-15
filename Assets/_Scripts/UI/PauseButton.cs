using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private PlacementSystem placementSystem;

    public void OnButtonClick()
    {
        placementSystem.StopPlacement();
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
