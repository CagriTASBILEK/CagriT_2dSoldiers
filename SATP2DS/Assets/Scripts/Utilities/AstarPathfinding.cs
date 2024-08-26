using System.Collections.Generic;
using UnityEngine;

public class AstarPathfinding
{
    private GridCellControl[,] grid;

    public AstarPathfinding(GridCellControl[,] grid)
    {
        this.grid = grid;
    }

    public List<GridCellControl> FindPath(Vector2Int start, Vector2Int end)
    {
        GridCellControl startCell = grid[start.x, start.y];
        GridCellControl endCell = grid[end.x, end.y];
        
        if (!endCell.IsEmpty)
        {
            endCell = FindClosestEmptyCell(end);
            if (endCell == null)
            {
                return null;
            }
        }

        List<GridCellControl> openSet = new List<GridCellControl> { startCell };
        HashSet<GridCellControl> closedSet = new HashSet<GridCellControl>();

        startCell.GCost = 0;
        startCell.HCost = GetHeuristic(startCell, endCell);

        while (openSet.Count > 0)
        {
            GridCellControl currentCell = GetLowestFCostCell(openSet);
            if (currentCell == endCell)
            {
                return RetracePath(startCell, endCell);
            }

            openSet.Remove(currentCell);
            closedSet.Add(currentCell);

            foreach (GridCellControl neighbor in GetNeighbors(currentCell))
            {
                if (!neighbor.IsEmpty || closedSet.Contains(neighbor))
                {
                    continue;
                }

                float newGCost = currentCell.GCost + GetDistance(currentCell, neighbor);
                if (newGCost < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newGCost;
                    neighbor.HCost = GetHeuristic(neighbor, endCell);
                    neighbor.Parent = currentCell;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null; // No path found
    }
    public GridCellControl FindClosestEmptyCell(Vector2Int start)
    {
        Queue<GridCellControl> queue = new Queue<GridCellControl>();
        HashSet<GridCellControl> visited = new HashSet<GridCellControl>();

        GridCellControl startCell = grid[start.x, start.y];
        queue.Enqueue(startCell);
        visited.Add(startCell);

        while (queue.Count > 0)
        {
            GridCellControl current = queue.Dequeue();

            if (current.IsEmpty)
            {
                return current;
            }

            foreach (GridCellControl neighbor in GetNeighbors(current))
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
        return null; // No empty cell found
    }
    
    private List<GridCellControl> RetracePath(GridCellControl startCell, GridCellControl endCell)
    {
        List<GridCellControl> path = new List<GridCellControl>();
        GridCellControl currentCell = endCell;

        while (currentCell != startCell)
        {
            path.Add(currentCell);
            currentCell = currentCell.Parent;
        }
        path.Reverse();
        return path;
    }

    private GridCellControl GetLowestFCostCell(List<GridCellControl> cells)
    {
        GridCellControl lowest = cells[0];
        foreach (GridCellControl cell in cells)
        {
            if (cell.FCost < lowest.FCost || cell.FCost == lowest.FCost && cell.HCost < lowest.HCost)
            {
                lowest = cell;
            }
        }
        return lowest;
    }

    private float GetHeuristic(GridCellControl a, GridCellControl b)
    {
        return Vector2Int.Distance(new Vector2Int((int)a.CellObject.transform.position.x, (int)a.CellObject.transform.position.y),
                                   new Vector2Int((int)b.CellObject.transform.position.x, (int)b.CellObject.transform.position.y));
    }

    private float GetDistance(GridCellControl a, GridCellControl b)
    {
        return Vector2Int.Distance(new Vector2Int((int)a.CellObject.transform.position.x, (int)a.CellObject.transform.position.y),
                                   new Vector2Int((int)b.CellObject.transform.position.x, (int)b.CellObject.transform.position.y));
    }

    private List<GridCellControl> GetNeighbors(GridCellControl cell)
    {
        List<GridCellControl> neighbors = new List<GridCellControl>();
        Vector2Int[] directions = {
            Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPos = new Vector2Int((int)cell.CellObject.transform.position.x, (int)cell.CellObject.transform.position.y) + direction;
            if (IsInBounds(neighborPos))
            {
                neighbors.Add(grid[neighborPos.x, neighborPos.y]);
            }
        }

        return neighbors;
    }

    private bool IsInBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < grid.GetLength(0) && position.y >= 0 && position.y < grid.GetLength(1);
    }
}
