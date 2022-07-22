using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    private float totalSpinAmount;

    private int maxShootDistance = 6;

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

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition =
                    unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (!isOffsetGridPositionIsInRange(offsetGridPosition))
                {
                    continue;
                }
                if (
                    !LevelGrid
                        .Instance
                        .HasAnyUnitOnGridPosition(testGridPosition)
                )
                {
                    // grid position is empty
                    continue;
                }
                var targetUnit =
                    LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                if (targetUnit.IsEnemy() == unit.IsEnemy())
                {
                    // both units are on same team
                    continue;
                }
                validGridPositionList.Add (testGridPosition);
            }
        }
        return validGridPositionList;
    }

    private bool isOffsetGridPositionIsInRange(GridPosition offsetGridPosition)
    {
        int testDistance =
            Mathf
                .RoundToInt(Mathf
                    .Sqrt(Mathf.Pow(Mathf.Abs(offsetGridPosition.x), 2) +
                    Mathf.Pow(Mathf.Abs(offsetGridPosition.z), 2)));
        if (testDistance > maxShootDistance)
        {
            return false;
        }
        return true;
    }

    public override void TakeAction(
        GridPosition gridPosition,
        Action onActionComplete
    )
    {
        totalSpinAmount = 0;
        isActive = true;
        this.onActionComplete = onActionComplete;
    }
}
