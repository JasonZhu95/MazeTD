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
    [SerializeField] private PreviewSystem preview;
    [SerializeField] private TowerPlacer towerPlacer;

    private GridData objectData;
    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    // Variables for Pathfinding placement validity
    [SerializeField] private Seeker seeker;
    [SerializeField] private GameObject endOfPath;
    private Path path;

    private int id;

    [SerializeField] private GameObject invalidPlacementCanvas;

    IBuildingState buildingState;
    IBuildingState removingState;

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
        buildingState = new RemovingState(grid, preview, database, objectData, towerPlacer, false);
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
        if (!IsPathReachable(seeker.gameObject.transform, endOfPath.transform))
        {
            removingState = new RemovingState(grid, preview, database, objectData, towerPlacer, true);
            removingState.OnAction(gridPosition);
            invalidPlacementCanvas.SetActive(true);
            buildingState.RefundCost();
            StartCoroutine(RescanAfterDelay());
        }
        StopPlacement();
    }
    private IEnumerator RescanAfterDelay()
    {
        yield return new WaitForSeconds(.1f);
        AstarPath.active.Scan(AstarPath.active.data.graphs[0]);
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

    private bool IsPathReachable(Transform start, Transform target)
    {
        // Get the appropriate GraphNode for the start and target positions
        GraphNode startNode = AstarPath.active.GetNearest(start.position).node;
        GraphNode targetNode = AstarPath.active.GetNearest(target.position).node;

        // Check if a path is possible between the start and target nodes
        return PathUtilities.IsPathPossible(startNode, targetNode);
    }
}
