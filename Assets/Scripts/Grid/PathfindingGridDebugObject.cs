using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PathfindingGridDebugObject : GridDebugObject
{
    [SerializeField]
    TextMeshPro gCostText;

    [SerializeField]
    TextMeshPro hCostText;

    [SerializeField]
    TextMeshPro fCostText;

    private PathNode pathNode;

    public override void SetGridObject(object gridObject){
        pathNode = (PathNode)gridObject;

        base.SetGridObject(gridObject);
    }

    protected override void Update()
    {
        base.Update();

        gCostText.text = pathNode.GetGCost().ToString();
        hCostText.text = pathNode.GetHCost().ToString();
        fCostText.text = pathNode.GetFCost().ToString();
    }
}
