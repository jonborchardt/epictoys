using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

    [Serializable]
    public struct GridVisualTypeMaterial
    {
        public GridVisualType gridVisualType;

        public Material material;
    }

    public enum GridVisualType
    {
        White,
        Blue,
        Red,
        RedSoft,
        Yellow
    }

    [SerializeField]
    private GridVisualTypeMaterial[] gridVisualTypeMaterialList;

    [SerializeField]
    private Transform gridSystemVisualSinglePrefab;

    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug
                .LogError("There is more than one GridSystemVisual " +
                transform +
                " - " +
                Instance);
            Destroy (gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        gridSystemVisualSingleArray =
            new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(),
            LevelGrid.Instance.GetHeight()];
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                var gridSystemVisualSingleTransform =
                    GameObject
                        .Instantiate(gridSystemVisualSinglePrefab,
                        LevelGrid.Instance.GetWorldPosition(gridPosition),
                        Quaternion.identity);
                gridSystemVisualSingleArray[x, z] =
                    gridSystemVisualSingleTransform
                        .GetComponent<GridSystemVisualSingle>();
            }
        }

        UnitActionSystem.Instance.OnSelectedActionChange +=
            UnitActionSystem_OnSelectedActionChange;
        LevelGrid.Instance.OnAnyUnitMovePosition +=
            LevelGrid_OnAnyUnitMovePosition;

        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        foreach (var item in gridSystemVisualSingleArray)
        {
            item.Hide();
        }
    }

    public void ShowGridPositionRange(
        GridPosition gridPosition,
        int maxRange,
        GridVisualType gridVisualType
    )
    {
        List<GridPosition> gridPositionList = new List<GridPosition>();
        for (int x = -maxRange; x <= maxRange; x++)
        {
            for (int z = -maxRange; z <= maxRange; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition =
                    gridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (!isOffsetGridPositionIsInRange(offsetGridPosition, maxRange)
                )
                {
                    continue;
                }

                gridPositionList.Add (testGridPosition);
            }
        }

        ShowGridPositionList (gridPositionList, gridVisualType);
    }

    // todo, move this to a shared util, it is don in the shoot action also
    private bool
    isOffsetGridPositionIsInRange(GridPosition offsetGridPosition, int range)
    {
        if (Vector3.Magnitude(offsetGridPosition.ToVector3()) > range)
        {
            return false;
        }
        return true;
    }

    public void ShowGridPositionList(
        List<GridPosition> gridPositionList,
        GridVisualType gridVisualType
    )
    {
        Material material = GetGridVisualTypeMaterial(gridVisualType);
        foreach (var gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z]
                .Show(material);
        }
    }

    public void UpdateGridVisual()
    {
        HideAllGridPositions();
        BaseAction selectedAction =
            UnitActionSystem.Instance.GetSelectedAction();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        GridVisualType gridVisualType;
        switch (selectedAction)
        {
            default:
            case MoveAction moveAction:
                gridVisualType = GridVisualType.White;
                break;
            case ShootAction shootAction:
                gridVisualType = GridVisualType.Red;
                ShowGridPositionRange(selectedUnit.GetGridPosition(),
                shootAction.GetMaxShootDistance(),
                GridVisualType.RedSoft);
                break;
            case SpinAction spinAction:
                gridVisualType = GridVisualType.Blue;
                break;
        }
        ShowGridPositionList(selectedAction.GetValidGridPositionList(),
        gridVisualType);
    }

    public void LevelGrid_OnAnyUnitMovePosition(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }

    public void UnitActionSystem_OnSelectedActionChange(
        object sender,
        EventArgs e
    )
    {
        UpdateGridVisual();
    }

    private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
    {
        foreach (GridVisualTypeMaterial
            gridVisualTypeMaterial
            in
            gridVisualTypeMaterialList
        )
        {
            if (gridVisualTypeMaterial.gridVisualType == gridVisualType)
            {
                return gridVisualTypeMaterial.material;
            }
        }
        Debug
            .LogError("Could not find GridVisualTypeMaterial for GridVisualType " +
            gridVisualType);
        return null;
    }
}
