using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance { get; private set; }

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
    }

    private void Update()
    {
        UpdateGridVisual();
    }

    public void HideAllGridPositions()
    {
        foreach (var item in gridSystemVisualSingleArray)
        {
            item.Hide();
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach (var gridPosition in gridPositionList)
        {
            gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void UpdateGridVisual()
    {
        HideAllGridPositions();
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        ShowGridPositionList(selectedUnit
            .GetMoveAction()
            .GetValidGridPositionList());
    }
}
