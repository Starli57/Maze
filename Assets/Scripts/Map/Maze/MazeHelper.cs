
using System;
using System.Collections.Generic;

public static class MazeHelper
{
    public static int[,] directions = new int[,]
    {
        {-1, 0 },
        { 1, 0 },
        { 0,-1 },
        { 0, 1 }
    };

    public static bool IsRoad(bool[,] maze, int x, int y)
    {
        return IsPointInMaze(maze, x, y) && maze[y, x] == true;
    }

    public static bool IsWall(bool[,] maze, int x, int y)
    {
        return IsPointInMaze(maze, x, y) == false || maze[y, x] == false;
    }

    public static bool IsPointInMaze(bool[,] maze, int x, int y)
    {
        int height = maze.GetLength(0);
        int width = maze.GetLength(1);
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public static Tuple<int, int> GetClosestRoad(bool[,] maze, int x, int y)
    {
        return GetClosest(maze, x, y, true);
    }

    public static Tuple<int, int> GetClosestWall(bool[,] maze, int x, int y)
    {
        return GetClosest(maze, x, y, false);
    }
    
    private static Tuple<int, int> GetClosest(bool[,] maze, int x, int y, bool targetValue)
    {
        if (maze[y, x] == targetValue)
            return new Tuple<int, int>(x,y);

        int height = maze.GetLength(0);
        int width = maze.GetLength(1);

        HashSet<Tuple<int,int>> visited = new HashSet<Tuple<int, int>>();
        LinkedList<Tuple<int, int>> shouldVisit = new LinkedList<Tuple<int, int>>();

        shouldVisit.AddLast(new Tuple<int, int>(x, y));

        while (shouldVisit.Count > 0)
        {
            Tuple<int, int> point = shouldVisit.First.Value;
            x = point.Item1;
            y = point.Item2;

            visited.Add(point);
            shouldVisit.RemoveFirst();

            if (maze[y,x] == targetValue)
                return point;

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int newX = x + directions[i, 0];
                int newY = y + directions[i, 1];

                if (newX < 0 || newY < 0 || newX >= width || newY >= height)
                    continue;

                Tuple<int, int> newPoint = new Tuple<int, int>(newX, newY);

                if (visited.Contains(newPoint) == false)
                    shouldVisit.AddLast(newPoint);
            }
        }

        return null;
    }
}
