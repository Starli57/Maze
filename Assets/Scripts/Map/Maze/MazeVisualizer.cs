using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeVisualizer : MonoBehaviour
{
    public void Visualise(bool[,] maze)
    {
        DestroyOldMeshes();

        BuildWalls(maze);
        BuildWallsAround(maze);
    }

#pragma warning disable 649
    [SerializeField] private GameObject _wallPrefab;
#pragma warning restore 649

    private void BuildWalls(bool[,] maze)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
            for (int j = 0; j < maze.GetLength(1); j++)
                if (maze[i, j] == false)
                    Instantiate(_wallPrefab, new Vector3(j, 0, i), Quaternion.identity, transform);
    }

    private void BuildWallsAround(bool[,] maze)
    {
        for (int i = -1; i <= maze.GetLength(0); i++)
        {
            Instantiate(_wallPrefab, new Vector3(-1, 0, i), Quaternion.identity, transform);
            Instantiate(_wallPrefab, new Vector3(maze.GetLength(1), 0, i), Quaternion.identity, transform);
        }

        for (int j = -1; j <= maze.GetLength(1); j++)
        {
            Instantiate(_wallPrefab, new Vector3(j, 0, -1), Quaternion.identity, transform);
            Instantiate(_wallPrefab, new Vector3(j, 0, maze.GetLength(0)), Quaternion.identity, transform);
        }
    }

    private void DestroyOldMeshes()
    {
        foreach (Transform child in transform)        
            Destroy(child.gameObject);
    }
}
