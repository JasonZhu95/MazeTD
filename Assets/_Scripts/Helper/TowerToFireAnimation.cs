using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerToFireAnimation : MonoBehaviour
{
    [SerializeField] private FireTower fireTower;

    public void AnimationEventFireEnd()
    {
        fireTower.FireAnimationEnd();
    }
}
