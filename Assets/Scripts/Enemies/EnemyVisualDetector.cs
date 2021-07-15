using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisualDetector : EnemySense
{
    private const float DETECTION_METER_MAX_VALUE = 100.0f;
    private const float MAX_DISTANCE_FILL_MULTIPLIER = 100.0f; 

    [Header("Ray Settings")]
    [SerializeField] private LayerMask _layerMask;
    [Range(1.0f, 1500.0f)]
    [SerializeField] private float _detectionRayDistance = 100f;

    [Header("Detection Settings")]
    [Range(1.0f, 50f)]
    [SerializeField] private float _detectionMeterBaseFillRate = 5f;
    [Range(1.0f, 100f)]
    [SerializeField] private float _detectionMeterDropRate = 50f;
    [Range(1.0f, 99f)]
    [SerializeField] private float _detectionSuspiciousThreshold = 25f;

    [Header("Player Stance Fill Rate Multiplier")]
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _standingIdleFillMultiplier = 1.0f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _standingWalkingFillMultiplier = 2.0f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _standingRunningFillMultiplier = 5.0f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _crouchIdleFillMultiplier = 0.5f;
    [Range(0.1f, 10.0f)]
    [SerializeField] private float _crouchWalkingFillMultiplier = 0.8f;

    private Vector3 DetectorEndPoint
    {
        get
        {
            Vector3 endPoint = transform.position;

            endPoint.x += _detectionRayDistance * transform.right.x;

            return endPoint;
        }
    }
    public float CurrentDetectionMeterValue { get; private set; }
    public bool IsSuspecting => DetectedPlayer != null && CurrentDetectionMeterValue >= _detectionSuspiciousThreshold;
    public bool PlayerFullyDetected => DetectedPlayer != null && CurrentDetectionMeterValue >= DETECTION_METER_MAX_VALUE;

    protected override void Update()
    {
        RaycastHit2D hitInfo;

        base.Update();

        hitInfo = Physics2D.Raycast(transform.position, transform.right, _detectionRayDistance, _layerMask);

        DetectedPlayer = hitInfo.collider == null ? null : hitInfo.collider.gameObject.GetComponent<PlayerController>();

        if (DetectedPlayer != null)
        {
            CalculateDetectionMeterIncrease();
        }
        else if (DetectedPlayer == null && CurrentDetectionMeterValue > 0f)
        {
            CurrentDetectionMeterValue -= _detectionMeterDropRate * Time.deltaTime;

            if (CurrentDetectionMeterValue < 0f)
                CurrentDetectionMeterValue = 0f;
        }
    }

    private void CalculateDetectionMeterIncrease()
    {
        CurrentDetectionMeterValue += GetFillRate() * Time.deltaTime;

        if (CurrentDetectionMeterValue > DETECTION_METER_MAX_VALUE)
            CurrentDetectionMeterValue = DETECTION_METER_MAX_VALUE;
    }

    private float GetFillRate()
    {
        float multiplier;

        switch (DetectedPlayer.CurrentStance)
        {
            case PlayerStance.StandingWalking:
                multiplier = _standingWalkingFillMultiplier;
                break;
            case PlayerStance.StandingRunning:
                multiplier = _standingRunningFillMultiplier;
                break;
            case PlayerStance.CrouchedIdle:
                multiplier = _crouchIdleFillMultiplier;
                break;
            case PlayerStance.CrouchWalking:
                multiplier = _crouchWalkingFillMultiplier;
                break;
            default:
                multiplier = _standingIdleFillMultiplier;
                break;
        }

        return _detectionMeterBaseFillRate * multiplier * GetDistanceFillMultiplier();
    }

    private float GetDistanceFillMultiplier()
    {
        float delta, maxDelta;
            
        delta = DetectedPlayer.transform.position.x - transform.position.x;
        maxDelta = DetectorEndPoint.x - transform.position.x;

        return (1 - Mathf.Clamp(delta / maxDelta, 0.0f, 1.0f)) * MAX_DISTANCE_FILL_MULTIPLIER;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, DetectorEndPoint);
    }
}
