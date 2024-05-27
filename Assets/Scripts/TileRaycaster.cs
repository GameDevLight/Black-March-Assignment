using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileRaycaster : MonoBehaviour
{
    public Text tileInfoText;

    [SerializeField] private LayerMask isGridLayer;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;


        Vector3 worldpos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit hit;

        if (Physics.Raycast(worldpos,Camera.main.transform.forward, out hit,300f,isGridLayer))
        {
            TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
            if (tileInfo != null)
            {
                tileInfoText.text = $"Tile Position: {tileInfo.gridPosition.ToString()} Is Walkable = "+tileInfo.isWalkable;
            }
            else
            {
                Debug.LogError("ERROR:TileInfo component not found on hit object.");
            }
        }
        
    }

}
