using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolderChangeZ : MonoBehaviour
{

    private void Update()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            Vector3 newPosition = child.position;
            newPosition.z = newPosition.y;
            child.position = newPosition;
        }
    }
}
