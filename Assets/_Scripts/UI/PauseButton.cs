using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PlacementSystem placementSystem;

    public void OnButtonClick()
    {
        placementSystem.StopPlacement();
        Time.timeScale = 0f;
    }
}
