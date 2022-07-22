using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;

    public event EventHandler OnSelectedActionChange;

    public event EventHandler<bool> OnBusyChanged;

    public event EventHandler OnActionStarted;

    [SerializeField]
    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private bool isBusy;

    private BaseAction selectedAction;

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

    void Start()
    {
        SetSelectedUnit (selectedUnit);
    }

    void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandleUnitSelection())
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition =
                LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                return;
            }
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }

            SetBusy();
            selectedAction.TakeAction (mouseGridPosition, ClearBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
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
                    if (unit == selectedUnit)
                    {
                        // unit already selected
                        return false;
                    }
                    SetSelectedUnit (unit);
                    return true;
                }
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return this.selectedUnit;
    }

    private void SetBusy()
    {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
