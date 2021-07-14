using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyDetection : MonoBehaviour
{
    [Header("Senses")]
    [SerializeField] private EnemySoundDetector _soundDetector;
    [SerializeField] private EnemyVisualDetector _visualDetector;

    [Header("Image Refs")]
    [SerializeField] private Image _detectionMeterBackground;
    [SerializeField] private Image _detectionMeterForeground;
    [SerializeField] private Image _soundDetectedIndicator;

    [Header("Settings")]
    [Range(0.5f, 3f)]
    [SerializeField] private float _soundDetectedIndicatorShowTime = 2f;

    private float _soundIndicatorTimer;

    private void Awake()
    {
        if (!_detectionMeterBackground || !_detectionMeterForeground ||
            !_soundDetectedIndicator)
            return;

        _detectionMeterBackground.enabled = false;
        _detectionMeterForeground.enabled = false;
        _soundDetectedIndicator.enabled = false;
    }

    private void Update()
    {
        if (!_soundDetector || !_visualDetector) return;

        if (_soundDetector.DetectedPlayer && !_soundDetectedIndicator.enabled)
        {
            _soundDetectedIndicator.enabled = true;
            _soundIndicatorTimer = _soundDetectedIndicatorShowTime;
        }

        if (_soundIndicatorTimer > 0.0f)
        {
            _soundIndicatorTimer -= Time.deltaTime;
        }
        else if (_soundIndicatorTimer <= 0.0f)
        {
            _soundDetectedIndicator.enabled = false;
        }

        if (_visualDetector.CurrentDetectionMeterValue > 0.0f)
        {
            if (!_detectionMeterBackground.enabled ||
                !_detectionMeterForeground.enabled)
            {
                _detectionMeterBackground.enabled = true;
                _detectionMeterForeground.enabled = true;
            }

            _detectionMeterForeground.fillAmount = 
                _visualDetector.CurrentDetectionMeterValue / 100.0f;
        }
        else if (_visualDetector.CurrentDetectionMeterValue <= 0.0f)
        {
            _detectionMeterBackground.enabled = false;
            _detectionMeterForeground.enabled = false;
        }
    }
}
