using System.Threading.Tasks;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private PathContainer _pathContainer;
    private float _screenCastLength = 200;

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

    private void Update()
    {
        if (InputManager.HasClick())
        {
            RaycastHit hit;
            if (Physics.Raycast(DependenciesContainer.Instance.mazeCamera.camera.ScreenPointToRay(InputManager.GetClickPosition()), out hit, _screenCastLength))
                UpdatePath(hit.point);
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
