using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    private int ID;
    private Grid grid;
    private PreviewSystem previewSystem;
    private TowerDatabaseSO database;
    private GridData objectData;
    private TowerPlacer towerPlacer;

    public PlacementState(int iD, Grid grid, PreviewSystem previewSystem, TowerDatabaseSO database, GridData objectData, TowerPlacer towerPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.objectData = objectData;
        this.towerPlacer = towerPlacer;

        selectedObjectIndex = database.towersData.FindIndex(data => data.ID == ID);
        if (selectedObjectIndex > -1)
        {
            previewSystem.StartShowingPlacementPreview(database.towersData[selectedObjectIndex].Prefab, database.towersData[selectedObjectIndex].Size);
        }
        else
        {
            throw new System.Exception($"No object with ID {iD}");
        }
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
        {
            return;
        }
        int index = towerPlacer.PlaceObject(database.towersData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

        GridData selectedData = objectData;
        selectedData.AddObjectAt(gridPosition, database.towersData[selectedObjectIndex].Size, database.towersData[selectedObjectIndex].ID, index);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
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

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }

    public void RefundCost()
    {
        towerPlacer.RefundTowerCost(database.towersData[selectedObjectIndex].Prefab);
    }
}
