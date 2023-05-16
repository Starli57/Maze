
using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class HeroInputManager : MonoBehaviour
{
    private HeroController _heroController;

    private void Awake()
    {
        _heroController = GetComponent<HeroController>();
    }

    private void Update()
    {
        var clickPosition = CheckMouseClickPosition();
        if (clickPosition.HasValue) SetHeroPositionByClick(clickPosition.Value);
    }

    private Vector3? CheckMouseClickPosition()
    {
        if (Input.GetMouseButtonDown(0))
            return Input.mousePosition;

        if (Input.touchCount > 0)
            return Input.touches[0].position;

        return null;
    }

    private void SetHeroPositionByClick(Vector3 clickPosition)
    {
        RaycastHit hit;
        var camera = DependenciesContainer.Instance.mazeCamera.camera;

        if (Physics.Raycast(camera.ScreenPointToRay(clickPosition), out hit))
            _heroController.SetTargetPosition(hit.point);
    }
}
