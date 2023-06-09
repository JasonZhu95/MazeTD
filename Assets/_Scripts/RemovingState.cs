using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    private Grid grid;
    private PreviewSystem previewSystem;
    private TowerDatabaseSO database;
    private GridData objectData;
    private TowerPlacer towerPlacer;
    private PlayerStats playerStats;
    private bool sellCheck;

    private int indexToSell;

    public RemovingState(Grid grid, PreviewSystem previewSystem, TowerDatabaseSO database, GridData objectData, TowerPlacer towerPlacer, bool sellCheck)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.database = database;
        this.objectData = objectData;
        this.towerPlacer = towerPlacer;
        this.sellCheck = sellCheck;

        previewSystem.StartShowingRemovePreview();
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if (objectData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = objectData;
        }

        if (selectedData == null)
        {
            
        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
            {
                return;
            }
            Vector2 positonToSearch = new Vector2(gridPosition.x, gridPosition.y + 2f);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(positonToSearch, .15f);

            BaseTower closestTower = null;
            float closestDistance = Mathf.Infinity;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                BaseTower tower = hitColliders[i].GetComponent<BaseTower>();
                if (tower != null)
                {
                    Collider2D collider = hitColliders[i];
                    float distance = Vector2.Distance(tower.transform.position, positonToSearch);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTower = tower;
                    }
                }
            }
            playerStats = GameObject.FindWithTag("PlayerStat").GetComponent<PlayerStats>();
            if (!sellCheck)
            {
                playerStats.AddCoins((int)(closestTower.currentValue * .8f));
            }

            selectedData.RemoveObjectAt(gridPosition);
            towerPlacer.RemoveObjectAt(gameObjectIndex, sellCheck);
        }
        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, CheckIfSelectionIsValid(gridPosition));
    }

    private bool CheckIfSelectionIsValid(Vector3Int gridPosition)
    {
        return !objectData.CanPlaceObjectAt(gridPosition, Vector2Int.one);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = CheckIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
    }

    public void RefundCost()
    {
        throw new NotImplementedException();
    }
}
