using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewOffset = 0.05f;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Material previewMaterialPrefab;

    private GameObject previewObject;
    private Material previewMaterialInstance;

    private SpriteRenderer cellIndicatorSR;

    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorSR = cellIndicator.GetComponentInChildren<SpriteRenderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor(size);
        cellIndicator.SetActive(true);
    }

    private void PreparePreview(GameObject previewObject)
    {
        SpriteRenderer renderer = previewObject.GetComponentInChildren<SpriteRenderer>();
        Material materials = renderer.material;
        materials = previewMaterialInstance;
        renderer.material = materials;
    }

    private void PrepareCursor(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, size.y, 1);
            cellIndicatorSR.material.mainTextureScale = size;
        }
    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        MovePreview(position);
        MoveCursor(position);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity)
    {
        Color c = validity ? Color.white : Color.red;
        c.a = 0.4f;
        cellIndicatorSR.material.color = c;
        previewMaterialInstance.color = c;
        Debug.Log(previewMaterialInstance.color);
    }

    private void MoveCursor(Vector3 position)
    {
        cellIndicator.transform.position = position;
    }

    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(position.x, position.y, position.z + previewOffset);
    }
}
