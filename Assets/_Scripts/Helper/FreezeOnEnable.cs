using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeOnEnable : MonoBehaviour
{
    private Animator anim;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("attack", true);
    }

    public void AnimationEventFinished()
    {
        anim.SetBool("attack", false);
        gameObject.SetActive(false);
    }
}
