using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public bool isDead = false;
    [SerializeField] private GameObject retryScreen;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("dead");
            retryScreen.SetActive(true);
            isDead = true;
        }
            
    }

}
