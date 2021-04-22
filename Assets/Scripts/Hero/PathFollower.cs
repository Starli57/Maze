using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private PathContainer _pathContainer;

    private List<Vector3> _path = null;

    private void Awake()
    {
        _pathContainer = GetComponent<PathContainer>();
    }

    private void OnEnable()
    {
        _pathContainer.onPathUpdated += OnPathUpdated;
    }

    private void OnDisable()
    {
        _pathContainer.onPathUpdated -= OnPathUpdated;
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        if (_path != null && _path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, _path[0], Time.deltaTime * _speed);
            if (IsInPlace(transform.position, _path[0]))
                _path.RemoveAt(0);
        }
    }

    private void OnPathUpdated(List<Vector3> path)
    {
        _path = path == null ? null : new List<Vector3>(path);
    }

    private bool IsInPlace(Vector3 from, Vector3 to)
    {
        float accuracy = 0.001f;
        return Vector3.Distance(from, to) <= accuracy;
    }
}
