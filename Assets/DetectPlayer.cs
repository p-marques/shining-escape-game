using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    //timer stuff
    private float waitTime;
    public float startWaitTime;

    //booleans
    bool isSearching = false;

    [SerializeField] private Animator jack;
    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            waitTime = startWaitTime;
            isSearching = false;
            jack.SetBool("isChasing", true);
            jack.SetBool("isPatrolling", false);
            Debug.Log("found you");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isSearching = true;
    }

    void FixedUpdate()
    {
        if (isSearching == true && jack.GetBool("isChasing"))
        {
            if (waitTime <= 0)
            {
                isSearching = false;
                jack.SetBool("isChasing", false);
                jack.SetBool("isPatrolling", true);
                Debug.Log("egh");
            }
        }
        else
            waitTime -= Time.deltaTime;
    }
}
