using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundDetector : EnemySense
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerNoiseGenerator playerNoise = collision.gameObject.GetComponent<PlayerNoiseGenerator>();

        if (playerNoise)
            DetectedPlayer = playerNoise.PlayerController;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerNoiseGenerator playerNoise = collision.gameObject.GetComponent<PlayerNoiseGenerator>();

        if (playerNoise)
            DetectedPlayer = null;
    }
}
