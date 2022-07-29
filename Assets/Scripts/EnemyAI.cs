using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy
    }

    private State state;

    private float timer;

    void Awake()
    {
        state = State.WaitingForEnemyTurn;
    }

    void Start()
    {
        TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
    }

    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        switch (state)
        {
            case State.WaitingForEnemyTurn:
                break;
            case State.TakingTurn:
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    if (TryTakeEnemyAiAction(SetStateTakingTurn))
                    {
                        state = State.Busy;
                    }
                    else
                    {
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case State.Busy:
                break;
        }
    }

    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = State.TakingTurn;
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        if (TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }
        state = State.TakingTurn;
        timer = 2;
    }

    private bool TryTakeEnemyAiAction(Action onEnemyActionComplete)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            if (TryTakeEnemyAiAction(enemyUnit, onEnemyActionComplete))
            {
                return true;
            }
        }
        return false;
    }

    private bool
    TryTakeEnemyAiAction(Unit enemyUnit, Action onEnemyActionComplete)
    {
        EnemyAiAction bestEnemyAiAction = null;
        BaseAction bestBaseAction = null;
        foreach (BaseAction baseAction in enemyUnit.GetBaseActionArray())
        {
            if (!enemyUnit.CanSpendActionPointsToTakeAction(baseAction))
            {
                // canot afford this action
                continue;
            }
            EnemyAiAction testEnemyAiAction = baseAction.GetBestEnemyAiAction();
            if (
                bestEnemyAiAction == null ||
                (
                testEnemyAiAction != null &&
                testEnemyAiAction.actionValue > bestEnemyAiAction.actionValue
                )
            )
            {
                bestEnemyAiAction = testEnemyAiAction;
                bestBaseAction = baseAction;
            }
        }
        if (!enemyUnit.TrySpendActionPointsToTakeAction(bestBaseAction))
        {
            return false;
        }

        bestBaseAction
            .TakeAction(bestEnemyAiAction.gridPosition, onEnemyActionComplete);
        return true;
    }
}
