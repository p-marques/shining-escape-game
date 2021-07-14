using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReaderSO _inputReader;

    [Header("Anchor")]
    [SerializeField] private IInteractableAnchorSO _currentHitInteractionAnchor;

    private void OnEnable()
    {
        _inputReader.OnInteractEvent += Interact;
    }

    private void OnDisable()
    {
        _inputReader.OnInteractEvent -= Interact;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
            _currentHitInteractionAnchor.Value = interactable;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
            _currentHitInteractionAnchor.Value = null;
    }

    private void Interact()
    {
        if (_currentHitInteractionAnchor.IsSet)
        {
            _currentHitInteractionAnchor.Value.Interact();
        }
    }
}
