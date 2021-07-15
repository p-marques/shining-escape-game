using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideViewDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private BoxCollider2D _boundingCollider;

    private Animator _animator;

    public bool IsOpen => !_boundingCollider.enabled;
    public bool IsDestroyed { get; private set; }
    public string CallToAction => IsOpen ? "Close" : "Open";
    public bool CanPlayerInteract(PlayerController player) => !IsDestroyed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        _boundingCollider.enabled = !_boundingCollider.enabled;

        if (_animator)
            _animator.SetBool("IsOpen", IsOpen);
    }

    public void Attack()
    {
        _animator.SetTrigger("Attacked");
    }

    public void OnDoorCompletelyDestroyed()
    {
        _boundingCollider.enabled = false;
        IsDestroyed = true;
    }
}
