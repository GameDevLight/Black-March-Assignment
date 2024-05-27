using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    private bool[] toggles = new bool[100];

    public override void OnInspectorGUI()
    {
        ObstacleData data = (ObstacleData)target;

        for (int i = 0; i < 10; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < 10; j++)
            {
                int index = i * 10 + j;
                toggles[index] = GUILayout.Toggle(data.obstacles[index], $"{i},{j}");
                data.obstacles[index] = toggles[index];
            }
            GUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(data);
        }
    }
}