
using UnityEngine;

public class MazeCamera : MonoBehaviour
{
    [SerializeField] private float _sidesOffset = 10;
    [SerializeField] private Vector3 _positionOffset = -Vector3.forward;

    private Map _mapBuilder;
    private Quaternion _defaultRotation;

    private void Awake()
    {
        _mapBuilder = FindObjectOfType<Map>();
        _defaultRotation = Quaternion.Euler(90, 0, 0);    
    }

    private void OnEnable()
    {
        _mapBuilder.onMapChanged += AdjustCam;

        AdjustCam();
    }

    private void OnDisable()
    {
        _mapBuilder.onMapChanged -= AdjustCam;
    }

    private void AdjustCam()
    {
        UpdatePosition();
        UpdateZoom();
    }

    private void UpdatePosition()
    {
        if (_mapBuilder.map == null)
            return;

        int mazeHeight = _mapBuilder.map.GetLength(0);
        int mazeWidth = _mapBuilder.map.GetLength(1);

        Vector3 targetPosition = new Vector3(
            mazeWidth / 2, transform.position.y, mazeHeight / 2);

        Quaternion rotationDiff = Quaternion.Euler(transform.rotation.eulerAngles.x - _defaultRotation.eulerAngles.x, 0, 0);

        transform.position = rotationDiff * targetPosition + _positionOffset;
    }

    private void UpdateZoom()
    {
        if (_mapBuilder.map == null)
            return;

        float screenAspect = (float)Screen.width / (float)Screen.height;

        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        float camHeight = 2 * camHalfHeight;
        float camWidth = 2 * camHalfWidth;

        float targetHeight = _mapBuilder.map.GetLength(0) + _sidesOffset;
        float targetWidth = _mapBuilder.map.GetLength(1) + _sidesOffset;

        Camera.main.orthographicSize *= Mathf.Max(targetWidth / camWidth, targetHeight / camHeight);
    }
}
