using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TowerDatabaseSO database;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private AudioSource validAudioSource;
    [SerializeField] private AudioSource invalidAudioSource;
    [SerializeField] private PreviewSystem preview;
    [SerializeField] private TowerPlacer towerPlacer;

    private GridData objectData;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    // Variables for Pathfinding placement validity
    [SerializeField] private Seeker seeker;
    [SerializeField] private GameObject endOfPath;
    private Path path;

    IBuildingState buildingState;

    private void Start()
    {
        StopPlacement();
        objectData = new GridData();
        seeker.StartPath(seeker.gameObject.transform.position, endOfPath.transform.position, OnPathComplete);
    }

    private void Update()
    {
        if (buildingState == null)
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3 mousePositionOutsideGrid = inputManager.GetCursorAnyPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if (lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
        }
        else
        {
            Debug.Log("Unable to find a valid path");
        }
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID, grid, preview, database, objectData, towerPlacer);

        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, preview, objectData, towerPlacer);
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

        buildingState.OnAction(gridPosition);

        AstarPath.active.Scan(AstarPath.active.data.graphs[0]);
        seeker.StartPath(seeker.gameObject.transform.position, endOfPath.transform.position, OnPathComplete);
        if (path.vectorPath[path.vectorPath.Count - 1] != endOfPath.transform.position)
        {
            Debug.Log("INVALID PATH");
        }
        StopPlacement();
    }

    public void StopPlacement()
    {
        if (buildingState == null)
        {
            return;
        }
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }
}
