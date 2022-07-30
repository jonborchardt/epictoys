using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }

    [SerializeField]
    private Transform gridDebugObjectPrefab;

    private int width;

    private int height;

    private float cellSize;

    private GridSystem<PathNode> gridSystem;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug
                .LogError("There is more than one Pathfinding " +
                transform +
                " - " +
                Instance);
            Destroy (gameObject);
            return;
        }
        Instance = this;

        gridSystem =
            new GridSystem<PathNode>(10, //todo: we should only set the with of our grid once
                10,
                2f,
                (GridSystem<PathNode> g, GridPosition gridPosition) =>
                    new PathNode(gridPosition));
        gridSystem.CreateDebugObjects (gridDebugObjectPrefab);
    }
}
