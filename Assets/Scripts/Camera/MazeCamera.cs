
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MazeCamera : MonoBehaviour
{
    public new Camera camera { get; private set; }

    [SerializeField] private float _sidesOffset = 10;
    [SerializeField] private Vector3 _positionOffset = -Vector3.forward;

    private float _heightOffset = 0;

    private Quaternion _defaultRotation;

    public void UpdateCameraHeight(float heightOffset)
    {
        _heightOffset = heightOffset;
        UpdatePosition();
    }

    private void Awake()
    {
        camera = GetComponent<Camera>();

        _heightOffset = DependenciesContainer.Instance.mazeConfig.cameraHeight;
        _defaultRotation = Quaternion.Euler(90, 0, 0);    
    }

    private void OnEnable()
    {
        DependenciesContainer.Instance.mapBuilder.onMapChanged += AdjustCam;

        AdjustCam();
    }

    private void OnDisable()
    {
        if (DependenciesContainer.Instance != null)
        {
            DependenciesContainer.Instance.mapBuilder.onMapChanged -= AdjustCam;
        }
    }

    private void AdjustCam()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (DependenciesContainer.Instance.mapBuilder.map == null)
            return;

        int mazeHeight = DependenciesContainer.Instance.mapBuilder.map.GetLength(0);
        int mazeWidth = DependenciesContainer.Instance.mapBuilder.map.GetLength(1);

        Vector3 targetPosition = new Vector3(
            mazeWidth / 2, _heightOffset, mazeHeight / 2);

        Quaternion rotationDiff = Quaternion.Euler(transform.rotation.eulerAngles.x - _defaultRotation.eulerAngles.x, 0, 0);

        transform.position = rotationDiff * targetPosition + _positionOffset;
    }
}
