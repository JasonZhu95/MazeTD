using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIClick : MonoBehaviour
{
    public void PlayClickSound()
    {
        FindObjectOfType<SoundManager>().Play("UIClick");
    }
}
