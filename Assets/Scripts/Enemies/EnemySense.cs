using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySense : MonoBehaviour
{
    private const float TIME_ELAPSED_CAP = 180f;

    private PlayerController _detectedPlayer;

    public PlayerController DetectedPlayer 
    {
        get => _detectedPlayer;
        protected set
        {
            if (value == null && _detectedPlayer != null)
            {
                LastKnownPosition = _detectedPlayer.transform.position;
            }
            else
            {
                TimeElapsedSinceLastDetection = 0f;
            }

            _detectedPlayer = value;
        }
    }
    public Vector3? LastKnownPosition { get; protected set; }
    public float TimeElapsedSinceLastDetection { get; protected set; }

    protected virtual void Update()
    {
        if (DetectedPlayer == null && TimeElapsedSinceLastDetection < TIME_ELAPSED_CAP)
        {
            TimeElapsedSinceLastDetection += Time.deltaTime;
        }
    }
}
