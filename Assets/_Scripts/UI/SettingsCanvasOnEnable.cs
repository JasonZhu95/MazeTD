using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCanvasOnEnable : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("start", true);
    }

    public void CloseCanvas()
    {
        Time.timeScale = 1f;
        anim.SetBool("start", false);
    }

    public void DeactivateCanvas()
    {
        gameObject.SetActive(false);
    }
}
