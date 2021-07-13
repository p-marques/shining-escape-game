using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerNoiseGenerator playerNoise = collision.gameObject.GetComponent<PlayerNoiseGenerator>();

        if (playerNoise)
            Debug.Log("Player noise detected!");
    }
}
