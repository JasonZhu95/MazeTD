using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;

    private Vector3 lastPosition;

    public event Action OnClicked;
    public event Action OnExit;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public Vector3 GetCursorAnyPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Vector2 rayOrigin = sceneCamera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, 100, placementLayerMask);
        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }

    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = sceneCamera.nearClipPlane;
        Vector2 rayOrigin = sceneCamera.ScreenToWorldPoint(mousePos);
        float x = Mathf.Clamp(rayOrigin.x, minX, maxX);
        float y = Mathf.Clamp(rayOrigin.y, minY, maxY);
        Vector2 clampedRayOrigin = new Vector3(x, y, 0);
        RaycastHit2D hit = Physics2D.Raycast(clampedRayOrigin, Vector2.zero, 100, placementLayerMask);
        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
