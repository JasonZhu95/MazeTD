using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator;
    [SerializeField] Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TowerDatabaseSO database;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private AudioSource validAudioSource;
    [SerializeField] private AudioSource invalidAudioSource;
    [SerializeField] private PreviewSystem preview;
    [SerializeField] private TowerPlacer towerPlacer;

    private GridData objectData;
    private int selectedObjectIndex = -1;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        StopPlacement();
        objectData = new GridData();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        mouseIndicator.SetActive(true);
        selectedObjectIndex = database.towersData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        preview.StartShowingPlacementPreview(database.towersData[selectedObjectIndex].Prefab, database.towersData[selectedObjectIndex].Size);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if ( placementValidity == false)
        {
            invalidAudioSource.Play();
            return;
        }
        validAudioSource.Play();
        int index = towerPlacer.PlaceObject(database.towersData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

        GridData selectedData = objectData;
        selectedData.AddObjectAt(gridPosition, database.towersData[selectedObjectIndex].Size, database.towersData[selectedObjectIndex].ID, index);
        preview.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        bool enemyValidity = false;
        GridData selectedData = objectData;

        // Check if an enemy currently occupies the space
        Vector2 gridPosition2D = new Vector2(gridPosition.x, gridPosition.y);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(gridPosition2D + new Vector2(.5f, 2.5f), new Vector2(1f, 1f), 0f);

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyValidity = true;
            }
        }

        return selectedData.CanPlaceObjectAt(gridPosition, database.towersData[selectedObjectIndex].Size) && !enemyValidity;
    }

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        preview.StopShowingPreview();
        mouseIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            objectData.DebugDictionaryValues();
        }
        if (selectedObjectIndex < 0)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3 mousePositionOutsideGrid = inputManager.GetCursorAnyPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPosition != gridPosition)
        {
            mouseIndicator.transform.position = mousePositionOutsideGrid;

            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            preview.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            lastDetectedPosition = gridPosition;
        }
    }
}
