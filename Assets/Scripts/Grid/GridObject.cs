using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem<GridObject> gridSystem;

    private GridPosition gridPosition;

    public List<Unit> unitList;

    public GridObject(
        GridSystem<GridObject> gridSystem,
        GridPosition gridPosition
    )
    {
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        unitList = new List<Unit>();
    }

    public List<Unit> GetUnitList()
    {
        return unitList;
    }

    public void AddUnit(Unit unit)
    {
        this.unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        this.unitList.Remove(unit);
    }

    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    public bool HasAnyUnit()
    {
        return this.unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        if (!HasAnyUnit())
        {
            return null;
        }
        return unitList[0];
    }
}
