using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    [SerializeField] private float maxRange;

    [SerializeField] private Animator anim;

    public CheckHidden check;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //raycast range
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, maxRange);

        //check if found player
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player")) 
            {
                    anim.SetBool("isChasing", true);
                    Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            }
            
        }
        else
            Debug.DrawLine(transform.position, transform.position + transform.right * maxRange, Color.green);
    }
}
