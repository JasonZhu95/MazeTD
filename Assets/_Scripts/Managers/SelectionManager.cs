using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public ISelectable selectedObject { get; set; }
    [SerializeField] private GameObject towerUpgradeCanvas;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    private void HandleSelection()
    {
        GameObject canvasObject = towerUpgradeCanvas;

        RectTransform canvasRectTransform = canvasObject.GetComponent<RectTransform>();
        Vector2 mousePosition;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out mousePosition))
        {
            // Mouse is not over the canvas, so return without deselecting
            return;
        }

        bool isMouseOverChildObject = false;

        // Iterate through the child objects of the Canvas GameObject
        for (int i = 0; i < canvasObject.transform.childCount; i++)
        {
            Transform childTransform = canvasObject.transform.GetChild(i);
            RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();

            if (RectTransformUtility.RectangleContainsScreenPoint(childRectTransform, Input.mousePosition))
            {
                // Mouse is over a child object
                isMouseOverChildObject = true;
                break;
            }
        }

        if (isMouseOverChildObject)
        {
            // Mouse is over a child object, so return without deselecting
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Towers");
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, layerMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            ISelectable selectable = hit.collider.GetComponent<ISelectable>();

            if (selectable != null)
            {
                if (selectedObject != null)
                {
                    if (selectedObject != selectable)
                    {
                        selectedObject.OnDeselect();
                        selectedObject = selectable;
                        selectedObject.OnSelect();
                    }
                }
                else
                {
                    selectedObject = selectable;
                    selectedObject.OnSelect();
                }
            }
            else
            {
                if (selectedObject != null)
                {
                    selectedObject.OnDeselect();
                    selectedObject = null;
                }
            }
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.OnDeselect();
                selectedObject = null;
            }
        }
    }
}
