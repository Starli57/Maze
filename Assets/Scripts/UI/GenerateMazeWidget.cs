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
#pragma warning restore 649

    private Map _mapBuilder;

    private void Awake()
    {
        _mapBuilder = FindObjectOfType<Map>();

        _widthSlider.value = DependenciesContainer.Instance.mazeConfig.width;
        _heightSlider.value = DependenciesContainer.Instance.mazeConfig.height;
        _gapsSlider.value = DependenciesContainer.Instance.mazeConfig.gapsChance;
        _cameraHeightSlider.value = DependenciesContainer.Instance.mazeConfig.cameraHeight;
    }

    private void LateUpdate()
    {
        DependenciesContainer.Instance.mazeCamera.UpdateCameraHeight(_cameraHeightSlider.value);
    }
}
