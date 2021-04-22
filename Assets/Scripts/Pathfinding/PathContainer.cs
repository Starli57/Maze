using System;
using System.Collections.Generic;
using UnityEngine;

public class PathContainer : MonoBehaviour
{
    public Action<List<Vector3>> onPathUpdated;

    public List<Vector3> path { get; private set; } = null;

    public void SetPath(List<Vector3> path)
    {
        this.path = path;
        onPathUpdated?.Invoke(this.path);
    }
}
