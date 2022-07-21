using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinAmount;

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, spinAmount, 0);

        totalSpinAmount += spinAmount;
        if (totalSpinAmount >= 360f)
        {
            isActive = false;
            onActionComplete();
        }
    }

    public override void TakeAction(GridPosition _, Action onActionComplete)
    {
        totalSpinAmount = 0;
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        var unitGridPos = unit.GetGridPosition();

        return new List<GridPosition> { unitGridPos };
    }
}
