using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private GridPosition gridPosition;

    private int gCost;

    private int hCost;

    private int fCost;

    private PathNode cameFromPathNode;

    private bool isWalkable = true;

    public PathNode(GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

    public int GetGCost()
    {
        return gCost;
    }

    public void SetGCost(int value)
    {
        gCost = value;
        CalcFCost();
    }

    public int GetHCost()
    {
        return hCost;
    }

    public void SetHCost(int value)
    {
        hCost = value;
        CalcFCost();
    }

    public int GetFCost()
    {
        return fCost;
    }

    private void CalcFCost()
    {
        fCost = hCost + gCost;
    }

    public PathNode GetCameFromPathNode()
    {
        return cameFromPathNode;
    }

    public void SetCameFromPathNode(PathNode value)
    {
        cameFromPathNode = value;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public bool IsWalkable() {
        return isWalkable;
    }

    public void SetIsWalkable(bool value){
        isWalkable = value;
    }
}
