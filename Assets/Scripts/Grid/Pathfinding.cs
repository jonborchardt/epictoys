using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance { get; private set; }

    private const int MoveStraightCost = 10;

    private const int MoveDiagonalCost = 14;

    [SerializeField]
    private Transform gridDebugObjectPrefab;

    [SerializeField]
    private LayerMask obsticlesLayerMask;

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
    }

    public void Setup(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridSystem =
            new GridSystem<PathNode>(width,
                height,
                cellSize,
                (GridSystem<PathNode> g, GridPosition gridPosition) =>
                    new PathNode(gridPosition));

        gridSystem.CreateDebugObjects (gridDebugObjectPrefab);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPositon =
                    LevelGrid.Instance.GetWorldPosition(gridPosition);
                float raycastOffsetDistance = 5f;
                if (
                    Physics
                        .Raycast(worldPositon +
                        Vector3.down * raycastOffsetDistance,
                        Vector3.up,
                        raycastOffsetDistance * 2,
                        obsticlesLayerMask)
                )
                {
                    GetNode(x, z).SetIsWalkable(false);
                }
            }
        }
    }

    public List<GridPosition>
    FindPath(GridPosition startGridPosition, GridPosition endGridPosition)
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closedList = new List<PathNode>();
        PathNode startNode = gridSystem.GetGridObject(startGridPosition);
        PathNode endNode = gridSystem.GetGridObject(endGridPosition);
        openList.Add (startNode);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                PathNode pathNode =
                    gridSystem.GetGridObject(new GridPosition(x, z));

                pathNode.SetGCost(int.MaxValue);
                pathNode.SetHCost(0);
                pathNode.SetCameFromPathNode(null);
            }
        }

        startNode.SetGCost(0);
        startNode
            .SetHCost(CalculateDistance(startGridPosition, endGridPosition));

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostPathNode(openList);
            if (currentNode == endNode)
            {
                // reached final node
                return CalculatePath(endNode);
            }
            openList.Remove (currentNode);
            closedList.Add (currentNode);

            foreach (PathNode neighborNode in GetNeighborList(currentNode))
            {
                if (closedList.Contains(neighborNode))
                {
                    continue;
                }
                if (!neighborNode.IsWalkable())
                {
                    closedList.Add (neighborNode);
                    continue;
                }
                int tentitiveGCost =
                    currentNode.GetGCost() +
                    CalculateDistance(currentNode.GetGridPosition(),
                    neighborNode.GetGridPosition());
                if (tentitiveGCost < neighborNode.GetGCost())
                {
                    neighborNode.SetCameFromPathNode (currentNode);
                    neighborNode.SetGCost (tentitiveGCost);
                    neighborNode
                        .SetHCost(CalculateDistance(neighborNode
                            .GetGridPosition(),
                        endGridPosition));

                    if (!openList.Contains(neighborNode))
                    {
                        openList.Add (neighborNode);
                    }
                }
            }
        }

        // no path found
        return null;
    }

    public int
    CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
    {
        GridPosition gridPositionDistance = gridPositionA - gridPositionB;
        int xDistance = Mathf.Abs(gridPositionDistance.x);
        int yDistance = Mathf.Abs(gridPositionDistance.z);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) +
        MoveStraightCost * remaining;
    }

    private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
    {
        PathNode lowestPathNode = pathNodeList[0];
        foreach (PathNode pathNode in pathNodeList)
        {
            if (pathNode.GetFCost() < lowestPathNode.GetFCost())
            {
                lowestPathNode = pathNode;
            }
        }
        return lowestPathNode;
    }

    private PathNode GetNode(int x, int z)
    {
        return gridSystem.GetGridObject(new GridPosition(x, z));
    }

    private List<PathNode> GetNeighborList(PathNode currentNode)
    {
        List<PathNode> neighborList = new List<PathNode>();
        GridPosition gridPosition = currentNode.GetGridPosition();

        if (gridPosition.x - 1 >= 0)
        {
            // Left
            neighborList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));
            if (gridPosition.z - 1 >= 0)
            {
                // LeftDown
                neighborList
                    .Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < height)
            {
                // LeftUp
                neighborList
                    .Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
            }
        }
        if (gridPosition.x + 1 < width)
        {
            // Right
            neighborList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));

            if (gridPosition.z - 1 >= 0)
            {
                // RightDown
                neighborList
                    .Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < height)
            {
                // RightUp
                neighborList
                    .Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
            }
        }
        if (gridPosition.z - 1 >= 0)
        {
            // Down
            neighborList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
        }
        if (gridPosition.z + 1 < height)
        {
            // Up
            neighborList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
        }

        return neighborList;
    }

    private List<GridPosition> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeList = new List<PathNode>();
        pathNodeList.Add (endNode);
        PathNode currentNode = endNode;
        while (currentNode.GetCameFromPathNode() != null)
        {
            pathNodeList.Add(currentNode.GetCameFromPathNode());
            currentNode = currentNode.GetCameFromPathNode();
        }
        pathNodeList.Reverse();
        List<GridPosition> gridPositionList = new List<GridPosition>();
        foreach (PathNode pathNode in pathNodeList)
        {
            gridPositionList.Add(pathNode.GetGridPosition());
        }
        return gridPositionList;
    }
}
