using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamPointsManager : MonoBehaviour
{
    [Range(0f, 30f)]
    [SerializeField] private float _roamPointChangeWaitTime = 5.0f;
    [Range(0f, 2f)]
    [SerializeField] private float _roamPointChangeWaitTimeVariance = 1f;

    public float[] Points { get; private set; }
    public float PointChangeWaitTime
    {
        get
        {
            if (_roamPointChangeWaitTimeVariance == 0f)
                return _roamPointChangeWaitTime;

            return Random.Range(RoamPointChangeWaitTimeMin, RoamPointChangeWaitTimeMax);
        }
    }
    private float RoamPointChangeWaitTimeMin => _roamPointChangeWaitTime - _roamPointChangeWaitTimeVariance;
    private float RoamPointChangeWaitTimeMax => _roamPointChangeWaitTime + _roamPointChangeWaitTimeVariance;

    private void Awake()
    {
        Transform[] transforms = GetComponentsInChildren<Transform>();
        
        Points = new float[transforms.Length - 1];

        // Ignoring self transform
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] = transforms[i + 1].position.x;
        }

        if (Points.Length < 2)
            Debug.LogError("EnemyRoamPointsManager has less than 2 points.");
    }
}
