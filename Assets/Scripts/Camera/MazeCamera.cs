
using UnityEngine;

public class MazeCamera : MonoBehaviour
{
    [SerializeField] private float _sidesOffset = 10;
    [SerializeField] private Vector3 _positionOffset = -Vector3.forward;

    [Space]
    [SerializeField] MazeConfiguration _defaultConfiguration;

    private float _heightOffset = 0;

    private Map _mapBuilder;
    private Quaternion _defaultRotation;

    public void UpdateCameraHeight(float heightOffset)
    {
        _heightOffset = heightOffset;
        UpdatePosition();
    }

    private void Awake()
    {
        _mapBuilder = FindObjectOfType<Map>();
        _heightOffset = _defaultConfiguration.cameraHeight;
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
    }

    private void UpdatePosition()
    {
        if (_mapBuilder != null && _mapBuilder.map == null)
            return;

        int mazeHeight = _mapBuilder.map.GetLength(0);
        int mazeWidth = _mapBuilder.map.GetLength(1);

        Vector3 targetPosition = new Vector3(
            mazeWidth / 2, _heightOffset, mazeHeight / 2);

        Quaternion rotationDiff = Quaternion.Euler(transform.rotation.eulerAngles.x - _defaultRotation.eulerAngles.x, 0, 0);

        transform.position = rotationDiff * targetPosition + _positionOffset;
    }
}
