using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;

    [SerializeField]
    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private bool isBusy;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug
                .LogError("There is more than one UnitActionSystem " +
                transform +
                " - " +
                Instance);
            Destroy (gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (TryHandleUnitSelection())
            {
                return;
            }

            GridPosition mouseGridPosition =
                LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (
                selectedUnit
                    .GetMoveAction()
                    .IsValidActionGridPosition(mouseGridPosition)
            )
            {
                SetBusy();
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (
            Physics
                .Raycast(ray,
                out RaycastHit raycastHit,
                float.MaxValue,
                unitLayerMask)
        )
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit (unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return this.selectedUnit;
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }
}
