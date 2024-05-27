using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    private int height = 10;
    private int width = 10;
    private float GridSpace = 1.05f;

    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private Transform cameraTransfrom;

    public GameObject[,] gameGrid; 


    void Start()
    {
        createGrid();//Creates the grid when the game starts
        SetTileNeighbors();
    }

    //Grid genration logic
    private void createGrid()
    {
        gameGrid = new GameObject[height,width];

        if (gridCellPrefab == null)//Null Error check
        {
            Debug.LogError("ERROR: Grid Cell Prefab on the Game grid is not assigned");
        }


        //Grid generation based on height and width
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                gameGrid[x, y] = Instantiate(gridCellPrefab, new Vector3(x * GridSpace,0, y * GridSpace), Quaternion.identity);
                gameGrid[x, y].transform.parent = transform;
                TileInfo tileInfo = gameGrid[x, y].GetComponent<TileInfo>();
                tileInfo.gridPosition = new Vector2Int(x, y);
                gameGrid[x,y].gameObject.name = "Grid Space ( X: " + x.ToString() + ",Y: " + y.ToString() + ")";//Grid info text
            }

        }

        cameraTransfrom.position = new Vector3((float)width/2,0, (float)height / 2);//Camera transform reset to the centre of the grid.

    }

    private void SetTileNeighbors()
    {
        for (int y = 0; y < width; y++)
        {
            for (int x = 0; x < height; x++)
            {
                List<TileInfo> neighbors = new List<TileInfo>();

                if (x > 0) neighbors.Add(gameGrid[x - 1, y].GetComponent<TileInfo>());
                if (x < width - 1) neighbors.Add(gameGrid[x + 1, y].GetComponent<TileInfo>());
                if (y > 0) neighbors.Add(gameGrid[x, y - 1].GetComponent<TileInfo>());
                if (y < height - 1) neighbors.Add(gameGrid[x, y + 1].GetComponent<TileInfo>());

                gameGrid[x, y].GetComponent<TileInfo>().SetNeighbors(neighbors);
            }
        }
    }


}

