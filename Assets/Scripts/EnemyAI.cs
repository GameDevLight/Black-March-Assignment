using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour, IAI
{
    private Pathfinding pathfinding;
    private Transform playerTransform;
    private bool isMoving = false;
    private int currentPathIndex;
    private List<TileInfo> path;

    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartEnemyTurn()
    {
        if (!isMoving)
        {
            MoveTowards(GetAdjacentTargetTile());
        }
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        if (isMoving) return;

        TileInfo startTile = GetCurrentTile();
        TileInfo endTile = GetTileAtPosition(targetPosition);
        if (endTile != null && endTile.isWalkable)
        {
            path = pathfinding.FindPath(startTile, endTile);
            if (path != null && path.Count > 0)
            {
                currentPathIndex = 0;
                StartCoroutine(MoveAlongPath());
            }
        }
    }

    private Vector3 GetAdjacentTargetTile()
    {
        Vector3 playerPosition = playerTransform.position;
        TileInfo playerTile = GetTileAtPosition(playerPosition);
        List<TileInfo> adjacentTiles = GetAdjacentTiles(playerTile);

        foreach (TileInfo tile in adjacentTiles)
        {
            if (tile.isWalkable)
            {
                return tile.transform.position;
            }
        }

        return transform.position;
    }

    private List<TileInfo> GetAdjacentTiles(TileInfo playerTile)
    {
        List<TileInfo> adjacentTiles = new List<TileInfo>();
        Vector2Int[] directions = {
            new Vector2Int(0, 1), // Up
            new Vector2Int(1, 0), // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0) // Left
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPos = playerTile.gridPosition - direction;
            if (IsWithinGrid(neighborPos))
            {
                adjacentTiles.Add(FindObjectOfType<GameGrid>().gameGrid[neighborPos.x, neighborPos.y].GetComponent<TileInfo>());
            }
        }

        return adjacentTiles;
    }

    private bool IsWithinGrid(Vector2Int position)
    {
        return position.x >= 0 && position.x < 10 && position.y >= 0 && position.y < 10;
    }

    private TileInfo GetCurrentTile()
    {
        Vector3 position = transform.position;
        int x = Mathf.RoundToInt(position.x / 1.05f);
        int y = Mathf.RoundToInt(position.z / 1.05f);
        return FindObjectOfType<GameGrid>().gameGrid[x, y].GetComponent<TileInfo>();
    }

    private TileInfo GetTileAtPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / 1.05f);
        int y = Mathf.RoundToInt(position.z / 1.05f);
        if (IsWithinGrid(new Vector2Int(x, y)))
        {
            return FindObjectOfType<GameGrid>().gameGrid[x, y].GetComponent<TileInfo>();
        }
        return null;
    }

    IEnumerator MoveAlongPath()
    {
        isMoving = true;

        while (currentPathIndex < path.Count)
        {
            Vector3 targetPosition = path[currentPathIndex].transform.position;
            targetPosition.y = 0.5f;
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
                yield return null;
            }
            currentPathIndex++;
        }

        isMoving = false;
    }
}
