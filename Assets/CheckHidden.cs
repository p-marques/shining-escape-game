using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHidden : MonoBehaviour
{
    PlayerController playerC;
    [SerializeField] GameObject player;
    public bool isHiding = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isHiding = true;
        else
            isHiding = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isHiding = false;

    }

}
