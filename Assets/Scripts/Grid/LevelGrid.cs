using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    [SerializeField]
    private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug
                .LogError("There is more than one LevelGrid " +
                transform +
                " - " +
                Instance);
            Destroy (gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects (gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }

    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).RemoveUnit(unit);
    }

    public void UnitMoveGridPosition(
        Unit unit,
        GridPosition fromGridPosition,
        GridPosition toGridPosition
    )
    {
        RemoveUnitAtGridPosition (fromGridPosition, unit);
        AddUnitAtGridPosition (toGridPosition, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        gridSystem.GetGridPosition(worldPosition);
}
