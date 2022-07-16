using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;

    private GridPosition gridPosition;

    public Unit unit;

    public Unit GetUnit()
    {
        return unit;
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString()+ "\n" + unit;
    }
}
