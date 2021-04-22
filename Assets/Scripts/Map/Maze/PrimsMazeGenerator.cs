using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimsMazeGenerator : IMazeAlgorithm
{
    public bool[,] Generate(int width, int height)
    {        
        bool[,] maze = new bool[height, width];

        int x = UnityEngine.Random.Range(0, width / 2) * 2;
        int y = UnityEngine.Random.Range(0, height / 2) * 2;

        HashSet<Tuple<int, int>> needConnectPoints = new HashSet<Tuple<int, int>>();
        needConnectPoints.Add(new Tuple<int, int>(x, y));

        while (needConnectPoints.Count > 0)
        {
            var point = HashSetUtilities.GetAndRemoveRandomElement(needConnectPoints);

            x = point.Item1;
            y = point.Item2;

            maze[y, x] = true;

            Connect(maze, x, y);
            AddVisitPoints(maze, needConnectPoints, x, y);
        }

        return maze;
    }

    private void Connect(bool[,] maze, int x, int y)
    {
        int[,] directions = new int[,]
        {
            {-1, 0 },
            { 1, 0 },
            { 0,-1 },
            { 0, 1 }
        };

        ShuffleUtility.Shuffle(directions);

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            // Нужно направление умножать на 2
            // чтоб оставлять стенку размером в 1 ячейку
            int neighborX = x + directions[i, 0] * 2;
            int neighborY = y + directions[i, 1] * 2;

            if (MazeHelper.IsRoad(maze, neighborX, neighborY))
            {
                int connectorX = x + directions[i, 0];
                int connectorY = y + directions[i, 1];
                maze[connectorY, connectorX] = true;

                return;
            }
        }
    }

    private void AddVisitPoints(bool[,] maze, HashSet<Tuple<int, int>> points, int x, int y)
    {
        if (MazeHelper.IsPointInMaze(maze, x - 2, y) && MazeHelper.IsRoad(maze, x - 2, y) == false)
            points.Add(new Tuple<int, int>(x - 2, y));

        if (MazeHelper.IsPointInMaze(maze, x + 2, y) && MazeHelper.IsRoad(maze, x + 2, y) == false)
            points.Add(new Tuple<int, int>(x + 2, y));

        if (MazeHelper.IsPointInMaze(maze, x, y - 2) && MazeHelper.IsRoad(maze, x, y - 2) == false)
            points.Add(new Tuple<int, int>(x, y - 2));

        if (MazeHelper.IsPointInMaze(maze, x, y + 2) && MazeHelper.IsRoad(maze, x, y + 2) == false)
            points.Add(new Tuple<int, int>(x, y + 2));
    }
}
