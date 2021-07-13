using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiningCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float _walkSpeed = 200f;
    [Range(1.1f, 2f)]
    [SerializeField] protected float _runSpeedMultiplier = 1.5f;
    [Range(0.1f, 0.9f)]
    [SerializeField] protected float _crouchSpeedMultiplier = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float _drag = 0.9f;

    [Header("Movement Actions")]
    [SerializeField] protected Move2DActionsSO _move2DActions;

    public Rigidbody2D Rigidbody { get; protected set; }
    public Animator Animator { get; protected set; }
    public bool IsRunning { get; protected set; }
    public bool IsCrouched { get; protected set; }

    protected virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        if (!Rigidbody)
            Debug.LogError("No rigidbody component on character!");

        if (!Animator)
            Debug.LogError("No animator component on character!");
    }

    public void Move(float horizontalAxis)
    {
        float moveSpeed = _walkSpeed;

        if (IsRunning)
            moveSpeed *= _runSpeedMultiplier;
        else if (IsCrouched)
            moveSpeed *= _crouchSpeedMultiplier;

        _move2DActions.Move(this, horizontalAxis, moveSpeed,
            _drag);
    }
}
