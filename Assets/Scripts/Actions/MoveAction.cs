using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler OnStartMoving;

    public event EventHandler OnStopMoving;

    [SerializeField]
    private int maxMoveDistance;

    private Vector3 targetPosition;

    protected override void Awake()
    {
        base.Awake();
        targetPosition = this.transform.position;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float stoppingDistance = 0.1f;
        Vector3 moveDirection =
            (targetPosition - transform.position).normalized;
        if (
            Vector3.Distance(targetPosition, transform.position) >
            stoppingDistance
        )
        {
            float moveSpeed = 4;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else
        {
            OnStopMoving?.Invoke(this, EventArgs.Empty);
            ActionComplete();
        }

        float rotateSpeed = 10;
        transform.forward =
            Vector3
                .Lerp(transform.forward,
                moveDirection,
                rotateSpeed * Time.deltaTime);
    }

    public override void TakeAction(
        GridPosition targetPosition,
        Action onActionComplete
    )
    {
        this.targetPosition =
            LevelGrid.Instance.GetWorldPosition(targetPosition);

        OnStartMoving?.Invoke(this, EventArgs.Empty);
        ActionStart (onActionComplete);
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();
        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition =
                    unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (unitGridPosition == testGridPosition)
                {
                    continue;
                }
                if (
                    LevelGrid
                        .Instance
                        .HasAnyUnitOnGridPosition(testGridPosition)
                )
                {
                    continue;
                }
                validGridPositionList.Add (testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public override string GetActionName()
    {
        return "Move";
    }
}
