using UnityEngine;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    private TileInfo[,] grid;
    private int gridSizeX, gridSizeY;

    public void InitializeGrid(TileInfo[,] grid, int width, int height)
    {
        this.grid = grid;
        this.gridSizeX = width;
        this.gridSizeY = height;
    }

    public List<TileInfo> FindPath(TileInfo startTile, TileInfo endTile)
    {
        List<TileInfo> openSet = new List<TileInfo> { startTile };
        HashSet<TileInfo> closedSet = new HashSet<TileInfo>();

        while (openSet.Count > 0)
        {
            TileInfo currentTile = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].FCost < currentTile.FCost || (openSet[i].FCost == currentTile.FCost && openSet[i].HCost < currentTile.HCost))
                {
                    currentTile = openSet[i];
                }
            }

            openSet.Remove(currentTile);
            closedSet.Add(currentTile);

            if (currentTile == endTile)
            {
                return RetracePath(startTile, endTile);
            }

            foreach (TileInfo neighbor in currentTile.GetNeighbors())
            {
                if (!neighbor.isWalkable || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentTile.GCost + GetDistance(currentTile, neighbor);
                if (newMovementCostToNeighbor < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = newMovementCostToNeighbor;
                    neighbor.HCost = GetDistance(neighbor, endTile);
                    neighbor.Parent = currentTile;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private List<TileInfo> RetracePath(TileInfo startTile, TileInfo endTile)
    {
        List<TileInfo> path = new List<TileInfo>();
        TileInfo currentTile = endTile;

        while (currentTile != startTile)
        {
            path.Add(currentTile);
            currentTile = currentTile.Parent;
        }
        path.Reverse();

        return path;
    }

    private int GetDistance(TileInfo tileA, TileInfo tileB)
    {
        int dstX = Mathf.Abs(tileA.gridPosition.x - tileB.gridPosition.x);
        int dstY = Mathf.Abs(tileA.gridPosition.y - tileB.gridPosition.y);
        return dstX + dstY;
    }
}
