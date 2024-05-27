using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Pathfinding pathfinding;
    private bool isMoving = false;
    private int currentPathIndex;
    private List<TileInfo> path;

    private EnemyAI enemyAI;

    void Start()
    {
        pathfinding = GetComponent<Pathfinding>();
        enemyAI = FindObjectOfType<EnemyAI>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();
                if (tileInfo != null && tileInfo.isWalkable)
                {
                    TileInfo startTile = GetCurrentTile();
                    path = pathfinding.FindPath(startTile, tileInfo);
                    if (path != null && path.Count > 0)
                    {
                        currentPathIndex = 0;
                        StartCoroutine(MoveAlongPath());
                    }
                }
            }
        }
    }

    private TileInfo GetCurrentTile()
    {
        Vector3 position = transform.position;
        int x = Mathf.RoundToInt(position.x / 1.05f);
        int y = Mathf.RoundToInt(position.z / 1.05f);
        return FindObjectOfType<GameGrid>().gameGrid[x, y].GetComponent<TileInfo>();
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
        enemyAI.StartEnemyTurn();
    }
}
