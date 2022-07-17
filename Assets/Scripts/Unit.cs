using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private Animator unitAnimator;

    private Vector3 targetPosition;

    private GridPosition gridPosition;

    private void Awake()
    {
        targetPosition = this.transform.position;
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
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

        GridPosition newGridPosition =
            LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid
                .Instance
                .UnitMoveGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
