using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour, IInteractable
{
    [SerializeField] private VoidEventChannelSO _toggleHideEvent;

    private bool _isBeingUsed;

    public string CallToAction => _isBeingUsed ? "Get Out" : "Hide";

    public void Interact()
    {
        if (_toggleHideEvent)
        {
            _isBeingUsed = !_isBeingUsed;

            _toggleHideEvent.RaiseEvent();
        }
        else
            Debug.LogError("HidingSpot missing event!");
    }
}
