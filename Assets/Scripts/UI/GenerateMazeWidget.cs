using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMazeWidget : MonoBehaviour
{
    public void OnMazeGeneratePressed()
    {
        _mapBuilder.GenerateMaze(
            (int)_widthSlider.value, 
            (int)_heightSlider.value, 
            _gapsSlider.value);
    }
    
#pragma warning disable 649
    [SerializeField] private Slider _widthSlider;
    [SerializeField] private Slider _heightSlider;
    [SerializeField] private Slider _gapsSlider;
    [SerializeField] private Slider _cameraHeightSlider;

    [Space]
    [SerializeField] private MazeCamera _camera;

    [Space]
    [SerializeField] private MazeConfiguration _defaultConfiguration;
#pragma warning restore 649

    private Map _mapBuilder;

    private void Awake()
    {
        _mapBuilder = FindObjectOfType<Map>();

        _widthSlider.value = _defaultConfiguration.width;
        _heightSlider.value = _defaultConfiguration.height;
        _gapsSlider.value = _defaultConfiguration.gapsChance;
        _cameraHeightSlider.value = _defaultConfiguration.cameraHeight;
    }

    private void LateUpdate()
    {
        _camera.UpdateCameraHeight(_cameraHeightSlider.value);
    }
}
