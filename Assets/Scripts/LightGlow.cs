using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightGlow : MonoBehaviour
{
    [SerializeField] private float _cadence;
    [SerializeField] private float _alternateIntensity;

    private Light2D _light;
    private float _originalIntensity;
    private float _timer;

    private float NextIntensity => _light.intensity == _originalIntensity ? _alternateIntensity : _originalIntensity;

    private void Awake()
    {
        _light = GetComponent<Light2D>();

        if (!_light)
            Debug.LogError("LightGlow has no light!");
        else
            _originalIntensity = _light.intensity;

        _timer = 0.0f;
    }

    private void Update()
    {
        if (!_light) return;

        if (_timer <= 0.0f)
        {
            _timer = _cadence;
            
            _light.intensity = NextIntensity;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }
}
