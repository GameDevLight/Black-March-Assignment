using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Tile"))
        {
            TileInfo tileInfo = other.GetComponent<TileInfo>();

            if (tileInfo != null)
            {
                tileInfo.SetWalkable(false);
                
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            TileInfo tileInfo = other.GetComponent<TileInfo>();

            if (tileInfo != null)
            {
                tileInfo.SetWalkable(true);
            }
        }
    }


}
