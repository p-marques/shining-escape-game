using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] spots;
    private int randSpot;
    public float speed;

    //timer stuff
    private float waitTime;
    public float startWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        randSpot = Random.Range(0, spots.Length);  
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, spots[randSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, spots[randSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randSpot = Random.Range(0, spots.Length);
            }
        }
        else
            waitTime -= Time.deltaTime;
    }
}
