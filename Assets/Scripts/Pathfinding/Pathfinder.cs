using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public List<Vector3> UpdatePath(Vector3 from, Vector3 to, Action<List<Vector3>> onPathUpdated = null)
    {
        if (_map == null)
            return null;

        int targetX = Mathf.RoundToInt(to.x);
        int targetY = Mathf.RoundToInt(to.z);

        if (MazeHelper.IsWall(_map, targetX, targetY))
            return null;

        int fromX = Mathf.RoundToInt(from.x);
        int fromY = Mathf.RoundToInt(from.z);

        if (MazeHelper.IsWall(_map, fromX, fromY))
            return null;

        var path = GetPath(fromX, fromY, targetX, targetY);
        if (path != null & path.Count > 0)
            onPathUpdated?.Invoke(path);

        return path;
    }
        
    private bool[,] _map { get { return _mapBuilder.map; } }
    private Map _mapBuilder;

    private void Awake()
    {
        _mapBuilder = FindObjectOfType<Map>();
    }

    private List<Vector3> GetPath(int fromX, int fromY, int toX, int toY)
    {
        Node from = new Node(fromX, fromY);
        Node target = null;

        var visited = new HashSet<Tuple<int, int>>();
        var shouldVisit = new LinkedList<Node>();

        shouldVisit.AddLast(from);

        while(shouldVisit.Count > 0)
        {
            var first = shouldVisit.First;
            int x = first.Value.x;
            int y = first.Value.y;

            shouldVisit.RemoveFirst();
            visited.Add(new Tuple<int, int>(x,y));

            if (x == toX && y == toY)
            {
                target = first.Value;
                break;
            }

            AddVisitPoints(visited, shouldVisit, x, y, first.Value);
        }

        return BuildPath(target);
    }

    private List<Vector3> BuildPath(Node target)
    {
        List<Vector3> path = new List<Vector3>();
        while (target != null && target.parent != null)
        {
            path.Insert(0, new Vector3(target.x, 0, target.y));
            target = target.parent;
        }

        return path;
    }

    private void AddVisitPoints(HashSet<Tuple<int, int>> visited, LinkedList<Node> shouldVisit, int x, int y, Node parent)
    {
        var directions = MazeHelper.directions;
        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = x + directions[i, 0];
            int newY = y + directions[i, 1];

            bool isRoad = MazeHelper.IsRoad(_map, newX, newY);
            bool wasVisited = visited.Contains(new Tuple<int, int>(newX, newY));

            if (isRoad && wasVisited == false)
                shouldVisit.AddLast(new Node(newX, newY, parent));
        }
    }

    private class Node
    {
        public int x;
        public int y;
        public Node parent;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Node(int x, int y, Node parent)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
        }
    }
}
