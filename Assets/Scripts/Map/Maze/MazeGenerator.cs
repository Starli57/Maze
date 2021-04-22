using System;
using System.Collections.Generic;

public class MazeGenerator
{    
    public MazeGenerator(IMazeAlgorithm algorithm)
    {
        _generationAlgorithm = algorithm;
    }

    public bool[,] Generate(int width, int height, float gapsChance)
    {
        bool[,] maze = _generationAlgorithm.Generate(width, height);
        AddGaps(maze, gapsChance);

        return maze;
    }

    private IMazeAlgorithm _generationAlgorithm = new PrimsMazeGenerator();
    
    private void AddGaps(bool[,] maze, float gapsChance)
    {
        int shortCutsTarget = (int)(maze.Length * gapsChance);
        int shortcuts = 0;

        if (shortCutsTarget <= 0)
            return;

        List<Tuple<int, int>> allWalls = new List<Tuple<int, int>>();
        for (int i = 0; i < maze.GetLength(0); i++)
            for (int j = 0; j < maze.GetLength(1); j++)
                if (maze[i, j] == false)
                    allWalls.Add(new Tuple<int, int>(i, j));

        ShuffleUtility.Shuffle(allWalls);

        for (int i = 0; i < allWalls.Count && shortcuts < shortCutsTarget; i++)
        {
            int x = allWalls[i].Item2;
            int y = allWalls[i].Item1;

            if (CanBeShortCut(maze, x, y))
            {
                maze[y, x] = true;
                shortcuts++;
            }
        }
    }

    private bool CanBeShortCut(bool[,] maze, int x, int y)
    {
        if (MazeHelper.IsRoad(maze, x, y))
            return false;

        bool isLeftRoad = MazeHelper.IsRoad(maze, x - 1, y);
        bool isRightRoad = MazeHelper.IsRoad(maze, x + 1, y);
        if (isLeftRoad && isRightRoad)
            return true;

        bool isTopRoad = MazeHelper.IsRoad(maze, x, y + 1);
        bool isBottomRoad = MazeHelper.IsRoad(maze, x, y - 1);
        if (isTopRoad && isBottomRoad)
            return true;

        return false;
    }
    
}
