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

    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        gridSystem.GetGridObject(gridPosition).SetUnit(unit);
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        return gridSystem.GetGridObject(gridPosition).GetUnit();
    }

    public void ClearUnitAtGridPosition(GridPosition gridPosition)
    {
        gridSystem.GetGridObject(gridPosition).SetUnit(null);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) =>
        gridSystem.GetGridPosition(worldPosition);
}
