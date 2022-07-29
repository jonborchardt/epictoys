using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler<OnShootEventArgs> OnShoot;

    private UnitAnimator unitAnimator;

    public class OnShootEventArgs : EventArgs
    {
        public Unit TargetUnit;

        public Unit ShootingUnit;
    }

    private enum State
    {
        Aiming,
        Shooting,
        Cooloff
    }

    private State state;

    private float totalSpinAmount;

    private int maxShootDistance = 6;

    private float stateTimer;

    private Unit targetUnit;

    private bool canShootBullet;

    protected override void Awake()
    {
        base.Awake();

        unitAnimator = this.unit.GetComponent<UnitAnimator>();
    }

    protected virtual void Start()
    {
        base.Start();
        unitAnimator.OnWeaponRelease += UnitAnimator_OnWeaponRelease;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        stateTimer -= Time.deltaTime;

        switch (state)
        {
            case State.Aiming:
                Vector3 aimDirection =
                    (targetUnit.GetWorldPosition() - transform.position)
                        .normalized;
                float rotateSpeed = 10;
                transform.forward =
                    Vector3
                        .Lerp(transform.forward,
                        aimDirection,
                        rotateSpeed * Time.deltaTime);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer <= 0f)
        {
            NextState();
        }
    }

    private void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.1f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                // no longes setting timer here,
                // instead going after animation weapon release, see below
                // state = State.Cooloff;
                // float cooloffStateTime = 0.5f;
                // stateTimer = cooloffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }

    private void Shoot()
    {
        OnShoot?
            .Invoke(this,
            new OnShootEventArgs {
                TargetUnit = targetUnit,
                ShootingUnit = unit
            });
    }

    // called after animation triggers release
    private void UnitAnimator_OnWeaponRelease(object sender, EventArgs e)
    {
        targetUnit.Damage(40);

        // was in case State.Shooting, see above
        state = State.Cooloff;
        float cooloffStateTime = 0.5f;
        stateTimer = cooloffStateTime;
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidGridPositionList()
    {
        return GetValidGridPositionList(unit.GetGridPosition());
    }

    public List<GridPosition>
    GetValidGridPositionList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
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

    // todo: refactor this based on gridsystemvisual
    private bool isOffsetGridPositionIsInRange(GridPosition offsetGridPosition)
    {
        if (Vector3.Magnitude(offsetGridPosition.ToVector3()) > maxShootDistance
        )
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
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
        canShootBullet = true;

        ActionStart (onActionComplete);
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public int GetMaxShootDistance()
    {
        return maxShootDistance;
    }

    public override EnemyAiAction GetEnemyAiAction(GridPosition gridPosition)
    {
        Unit targetUnit =
            LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        return new EnemyAiAction {
            gridPosition = gridPosition,
            // target hurt units first
            actionValue =
                100 +
                Mathf.RoundToInt((1 - targetUnit.GetHealthNormalized()) * 100)
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidGridPositionList(gridPosition).Count;
    }
}
