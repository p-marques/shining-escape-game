using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{

    //Timer stuff
    private float waitTime;
    private Transform target;
    deathZone checkDeath;
    [SerializeField] private float startWaitTime;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        while(animator.GetBool("isChasing") == false)
        {
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;

                animator.SetBool("isPatrolling", true);
                animator.SetBool("isIdle", false);
                waitTime = startWaitTime;
            }
            else
                waitTime -= Time.deltaTime;
        }
    }
}
