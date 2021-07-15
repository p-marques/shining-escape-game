using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO _onPlayerDiedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player && player.IsDetectable)
        {
            _onPlayerDiedEvent.RaiseEvent();
            Destroy(player.gameObject);
        }
    }
}
