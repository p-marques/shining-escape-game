using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject character;
    [SerializeField] private Transform moveTo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(character.tag == "Player")
            character.transform.position = moveTo.transform.position;
    }
}
