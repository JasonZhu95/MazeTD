using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerToLightningAnimation : MonoBehaviour
{
    [SerializeField] private LightningTower lightningTower;

    public void AnimationEventFireEnd()
    {
        lightningTower.LightningAnimationEnd();
    }
}
