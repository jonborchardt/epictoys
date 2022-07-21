using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    private Vector3 targetPosition;

    [SerializeField]
    private Animator unitAnimator;

    [SerializeField]
    private int maxMoveDistance;

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
            unitAnimator.SetFloat("IsWalking", 1);
        }
        else
        {
            unitAnimator.SetFloat("IsWalking", 0);
            isActive = false;
            onActionComplete();
        }

        float rotateSpeed = 10;
        transform.forward =
            Vector3
                .Lerp(transform.forward,
                moveDirection,
                rotateSpeed * Time.deltaTime);
    }

    public void Move(GridPosition targetPosition, Action onActionComplete)
    {
        this.targetPosition =
            LevelGrid.Instance.GetWorldPosition(targetPosition);
        isActive = true;
        this.onActionComplete = onActionComplete;
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        var valid = GetValidGridPositionList();
        return valid.Contains(gridPosition);
    }

    public List<GridPosition> GetValidGridPositionList()
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

    public override string GetActionName(){
        return "Move";
    }
}
