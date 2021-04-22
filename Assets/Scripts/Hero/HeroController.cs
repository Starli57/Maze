using System.Threading.Tasks;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Map _mapBuilder;
    private PathContainer _pathContainer;
    private Pathfinder _pathfinder;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;

        _mapBuilder = FindObjectOfType<Map>();
        _pathfinder = FindObjectOfType<Pathfinder>();

        _pathContainer = GetComponent<PathContainer>();
    }

    private void OnEnable()
    {
        _mapBuilder.onMapChanged += ResetPath;
    }

    private void OnDisable()
    {
        _mapBuilder.onMapChanged -= ResetPath;
    }

    private void Update()
    {
        if (InputManager.HasClick())
        {
            RaycastHit hit;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(InputManager.GetClickPosition()), out hit, 100))
                UpdatePath(hit.point);
        }
    }

    private void UpdatePath(Vector3 targetPosition)
    {
        _pathfinder.UpdatePath(transform.position, targetPosition, _pathContainer.SetPath);
    }

    private void ResetPath()
    {
        _pathContainer.SetPath(null);
    }
}
