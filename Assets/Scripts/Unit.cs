using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static event EventHandler OnAnyActionPointsChanged;

    [SerializeField]
    private bool isEnemy;

    private GridPosition gridPosition;

    private MoveAction moveAction;

    private SpinAction spinAction;

    private BaseAction[] baseActionArray;

    private const int actionPointsMax = 2;

    private int actionPoints = actionPointsMax;

    private HealthSystem healthSystem;

    private void Awake()
    {
        moveAction = this.GetComponent<MoveAction>();
        spinAction = this.GetComponent<SpinAction>();
        baseActionArray = this.GetComponents<BaseAction>();
        healthSystem = this.GetComponent<HealthSystem>();
    }

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void Update()
    {
        GridPosition newGridPosition =
            LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            GridPosition oldGridPos = gridPosition;
            gridPosition = newGridPosition;

            LevelGrid
                .Instance
                .UnitMoveGridPosition(this, oldGridPos, newGridPosition);
        }
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if (
            (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
            (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn())
        )
        {
            actionPoints = actionPointsMax;
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
    {
        return actionPoints >= baseAction.GetActionPointsCost();
    }

    private void SpendActionPoints(int amount)
    {
        actionPoints -= amount;
        OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if (CanSpendActionPointsToTakeAction(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        return false;
    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public void Damage(int dammageAmount)
    {
        healthSystem.Dammage (dammageAmount);
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy (gameObject);
    }
}
