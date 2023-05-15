using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    public void OnButtonClick()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
