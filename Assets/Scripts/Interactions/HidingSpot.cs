using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private VoidEventChannelSO _toggleHideEvent;

    private Animator _animator;
    private bool _isBeingUsed;

    public string CallToAction => _isBeingUsed ? "Get Out" : "Hide";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CanPlayerInteract(PlayerController player)
    {
        return !player.IsBeingChased;
    }

    public void Interact()
    {
        if (_toggleHideEvent)
        {
            _isBeingUsed = !_isBeingUsed;

            if (_animator)
                _animator.SetTrigger("Interact");

            _toggleHideEvent.RaiseEvent();
        }
        else
            Debug.LogError("HidingSpot missing event!");
    }
}
