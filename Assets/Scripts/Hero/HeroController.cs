using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PathContainer))]
public class HeroController : MonoBehaviour
{
    public void SetTargetPosition(Vector3 targetPosition)
    {
        UpdatePath(targetPosition);
    }

    private PathContainer _pathContainer;

    private void Awake()
    {
        _pathContainer = GetComponent<PathContainer>();
    }

    private void OnEnable()
    {
        DependenciesContainer.Instance.mapBuilder.onMapChanged += ResetPath;
    }

    private void OnDisable()
    {
        if (DependenciesContainer.Instance != null)
        {
            DependenciesContainer.Instance.mapBuilder.onMapChanged -= ResetPath;
        }
    }

    private void UpdatePath(Vector3 targetPosition)
    {
        DependenciesContainer.Instance.pathfinder.UpdatePath(transform.position, targetPosition, _pathContainer.SetPath);
    }

    private void ResetPath()
    {
        _pathContainer.SetPath(null);
    }
}
