using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionScriptUI : MonoBehaviour
{
    [SerializeField]
    private Transform actionButtonPrefab;

    [SerializeField]
    private Transform actionButtonContainerTransform;

    private List<ActionButtonUI> actionButtonUIList;

    private void Awake()
    {
        actionButtonUIList = new List<ActionButtonUI>();
    }

    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChange +=
            UnitActionSystem_OnSelectedUnitChange;
        UnitActionSystem.Instance.OnSelectedActionChange +=
            UnitActionSystem_OnSelectedActionChange;

        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform buttonTransform in actionButtonContainerTransform)
        {
            Destroy(buttonTransform.gameObject);
            actionButtonUIList.Clear();
        }
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        foreach (BaseAction baseAction in selectedUnit.GetBaseActionArray())
        {
            Transform actionButtonTransform =
                Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            ActionButtonUI actionButtonUI =
                actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction (baseAction);
            actionButtonUIList.Add (actionButtonUI);
        }
    }

    private void UnitActionSystem_OnSelectedUnitChange(
        object sender,
        EventArgs e
    )
    {
        CreateUnitActionButtons();
        UpdateSelectedVisual();
    }

    private void UnitActionSystem_OnSelectedActionChange(
        object sender,
        EventArgs e
    )
    {
        UpdateSelectedVisual();
    }

    private void UpdateSelectedVisual()
    {
        foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
        {
            actionButtonUI.UpdateSelectedVisual();
        }
    }
}
