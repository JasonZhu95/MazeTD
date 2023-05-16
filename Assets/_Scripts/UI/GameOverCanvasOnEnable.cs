using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvasOnEnable : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("start", true);
    }
}
