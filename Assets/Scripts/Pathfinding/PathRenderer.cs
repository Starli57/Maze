using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{    
    private LineRenderer _lineRenderer;
    private PathContainer _pathContainer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _pathContainer = GetComponent<PathContainer>();
    }
        
    private void OnEnable()
    {
        ShowPath(_pathContainer.path);

        _pathContainer.onPathUpdated += ShowPath;
    }

    private void OnDisable()
    {
        _pathContainer.onPathUpdated -= ShowPath;
    }

    private void ShowPath(List<Vector3> path)
    {
        if (path == null)
        {
            _lineRenderer.positionCount = 0;
            return;
        }

        _lineRenderer.positionCount = path.Count;
        _lineRenderer.SetPositions(path.ToArray());
    }
}
