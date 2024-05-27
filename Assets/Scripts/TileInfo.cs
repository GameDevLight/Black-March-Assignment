using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public Vector2Int gridPosition;

    public bool isWalkable =true;


    public int GCost;
    public int HCost;
    public int FCost { get { return GCost + HCost; } }
    public TileInfo Parent;

    private List<TileInfo> neighbors;

    public void SetWalkable(bool value)
    {
        isWalkable = value;
    }

    public void SetNeighbors(List<TileInfo> neighborTiles)
    {
        neighbors = neighborTiles;
    }

    public List<TileInfo> GetNeighbors()
    {
        return neighbors;
    }
}
