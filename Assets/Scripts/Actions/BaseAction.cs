using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted;

    public static event EventHandler OnAnyActionCompleted;

    protected bool isActive;

    protected Unit unit;

    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = this.GetComponent<Unit>();
    }

    protected virtual void Start()
    {
    }

    public abstract string GetActionName();

    public abstract void TakeAction(
        GridPosition gridPosition,
        Action onActionComplete
    );

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        var valid = GetValidGridPositionList();
        return valid.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidGridPositionList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;

        OnAnyActionStarted?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();

        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public EnemyAiAction GetBestEnemyAiAction()
    {
        List<EnemyAiAction> enemyAiActionList = new List<EnemyAiAction>();
        List<GridPosition> validActionGridPositionList =
            GetValidGridPositionList();
        foreach (GridPosition gridPosition in validActionGridPositionList)
        {
            EnemyAiAction enemyAiAction = GetEnemyAiAction(gridPosition);
            enemyAiActionList.Add (enemyAiAction);
        }

        if (enemyAiActionList.Count > 0)
        {
            enemyAiActionList
                .Sort((EnemyAiAction a, EnemyAiAction b) =>
                    b.actionValue - a.actionValue);
            return enemyAiActionList[0];
        }

        // no possible actions
        return null;
    }

    public abstract EnemyAiAction GetEnemyAiAction(GridPosition gridPosition);
}
