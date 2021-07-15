using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackExitPoint : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JackController jack = collision.gameObject.GetComponent<JackController>();

        if (jack)
        {
            _animator.SetTrigger("Open");
            Destroy(jack.gameObject);
        }
    }
}
