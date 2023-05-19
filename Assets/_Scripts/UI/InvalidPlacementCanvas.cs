using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvalidPlacementCanvas : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("start", true);
    }

    public void DisableAfterAnimation()
    {
        gameObject.SetActive(false);
    }
}
