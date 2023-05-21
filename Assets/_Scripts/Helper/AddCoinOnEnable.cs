using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinOnEnable : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("start", true);
    }

    public void AnimationEventFinished()
    {
        anim.SetBool("start", false);
        gameObject.SetActive(false);
    }
}
