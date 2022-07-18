using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    private Vector3 targetPosition;

    private Unit unit;

    [SerializeField]
    private Animator unitAnimator;

    [SerializeField]
    private int maxMoveDistance;

    private void Awake()
    {
        targetPosition = this.transform.position;
        unit = this.GetComponent<Unit>();
    }

    private void Update()
    {
        float stoppingDistance = 0.1f;
        if (
            Vector3.Distance(targetPosition, transform.position) >
            stoppingDistance
        )
        {
            Vector3 moveDirection =
                (targetPosition - transform.position).normalized;
            float moveSpeed = 4;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            float rotateSpeed = 10;
            transform.forward =
                Vector3
                    .Lerp(transform.forward,
                    moveDirection,
                    rotateSpeed * Time.deltaTime);
            unitAnimator.SetFloat("IsWalking", 1);
        }
        else
        {
            unitAnimator.SetFloat("IsWalking", 0);
        }
    }

    public void Move(GridPosition targetPosition)
    {
        this.targetPosition =
            LevelGrid.Instance.GetWorldPosition(targetPosition);
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
}
