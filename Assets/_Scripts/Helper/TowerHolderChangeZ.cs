using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHolderChangeZ : MonoBehaviour
{
    private int childCount;

    private void Start()
    {
        // Store the initial number of child objects
        childCount = transform.childCount;
    }

    private void Update()
    {
        // Check if the number of child objects has changed
        if (transform.childCount != childCount)
        {
            // Perform actions when a child object is created or destroyed
            Debug.Log("Child object created or destroyed!");
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                Vector3 newPosition = child.position;
                newPosition.z = newPosition.y;
                child.position = newPosition;
            }

            // Update the child count
            childCount = transform.childCount;
        }
    }


}
