using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{
    private Transform playerPos;
<<<<<<< Updated upstream:Assets/Scripts/ChaseBehavior.cs
    [SerializeField] private float speed;
=======
    public float speed;
>>>>>>> Stashed changes:Assets/ChaseBehavior.cs

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
<<<<<<< Updated upstream:Assets/Scripts/ChaseBehavior.cs
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);

=======
        Vector2 playerPosX = new Vector2(playerPos.position.x, 0);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPosX, speed * Time.deltaTime);
>>>>>>> Stashed changes:Assets/ChaseBehavior.cs
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
