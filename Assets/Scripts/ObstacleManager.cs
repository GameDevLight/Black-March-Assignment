using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    void Start()
    {
        PlaceObstacles();

    }

    void PlaceObstacles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int index = i * 10 + j;
                if (obstacleData.obstacles[index])
                {
                    Instantiate(obstaclePrefab, new Vector3(i * 1.05f, 0.25f, j * 1.05f), Quaternion.identity);

                }
            }
        }
    }

}
