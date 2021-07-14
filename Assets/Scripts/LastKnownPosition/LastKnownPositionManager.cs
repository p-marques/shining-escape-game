using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastKnownPositionManager : MonoBehaviour
{
    [Header("Sprite Renderer")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite _visualLastKnownPositionSprite;
    [SerializeField] private Sprite _soundLastKnownPositionSprite;

    [Header("Events")]
    [SerializeField] private LastKnownPositionEventChannelSO _eventChannel;
    [SerializeField] private VoidEventChannelSO _onStoppedInvestigating;

    private void OnEnable()
    {
        _eventChannel.OnEventRaised += OnShowLastKnownPosition;
        _onStoppedInvestigating.OnEventRaised += Hide;
    }

    private void OnDisable()
    {
        _eventChannel.OnEventRaised -= OnShowLastKnownPosition;
        _onStoppedInvestigating.OnEventRaised -= Hide;
    }

    private void OnShowLastKnownPosition(LastKnownPositionInfo lastKnownPosition)
    {
        if (!_spriteRenderer || 
            !_soundLastKnownPositionSprite || 
            !_visualLastKnownPositionSprite)
            return;

        _spriteRenderer.transform.position = lastKnownPosition.Position;

        if (lastKnownPosition.Mode == LastKnownPositionMode.Sound)
        {
            _spriteRenderer.sprite = _soundLastKnownPositionSprite;
        }
        else
        {
            _spriteRenderer.sprite = _visualLastKnownPositionSprite;
        }
    }

    private void Hide()
    {
        if (!_spriteRenderer)
            return;

        _spriteRenderer.sprite = null;
    }
}
