using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : StateMachineBehaviour
{

    [SerializeField] public Transform[] moveSpots;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    private int randSpot;


    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randSpot = Random.Range(0, moveSpots.Length);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, moveSpots[randSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(animator.transform.position, moveSpots[randSpot].position) < distance) 
        {
            randSpot = Random.Range(0, moveSpots.Length);
            animator.SetBool("isPatrolling", false);
            animator.SetBool("isIdle", true);
        } 


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isPatrolling", false);
        animator.SetBool("isIdle", true);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
