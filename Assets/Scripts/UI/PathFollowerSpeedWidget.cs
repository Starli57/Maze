using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Airplane
{
	public class PathFollowerSpeedWidget : MonoBehaviour
	{
		[SerializeField] private Slider _speedSlider;
		[SerializeField] private PathFollower _pathFollower;

        private void Awake()
        {
            _speedSlider.value = _pathFollower.Speed;
        }

        private void OnEnable()
        {
            _speedSlider.onValueChanged.AddListener(OnSpeedSliderValueChanged);
        }

        private void OnDisable()
        {
            _speedSlider.onValueChanged.RemoveListener(OnSpeedSliderValueChanged);
        }

        private void OnSpeedSliderValueChanged(float speed)
        {
            _pathFollower.Speed = speed;
        }
    }
}
