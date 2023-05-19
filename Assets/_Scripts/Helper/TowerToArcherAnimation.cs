using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerToArcherAnimation : MonoBehaviour
{
    [SerializeField] private ArrowTower arrowTower;

    public void AnimationEventArcherEnd()
    {
        arrowTower.ArcherAnimationEnd();
    }
}
