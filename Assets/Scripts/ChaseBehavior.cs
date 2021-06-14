using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehavior : StateMachineBehaviour
{

    private Transform playerPos;
    [SerializeField] private float speed;
<<<<<<< HEAD:Assets/Scripts/ChaseBehavior.cs
<<<<<<< HEAD:Assets/Scripts/ChaseBehavior.cs
=======
    Animator player;
>>>>>>> parent of dce448a (Merge branch 'hugo' into main):Assets/ChaseBehavior.cs
=======
    Animator player;
>>>>>>> parent of dce448a (Merge branch 'hugo' into main):Assets/ChaseBehavior.cs

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
<<<<<<< HEAD:Assets/Scripts/ChaseBehavior.cs
<<<<<<< HEAD:Assets/Scripts/ChaseBehavior.cs
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);

=======

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(playerPos.position.x, animator.transform.position.y), speed * Time.deltaTime);

>>>>>>> parent of dce448a (Merge branch 'hugo' into main):Assets/ChaseBehavior.cs
=======

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, new Vector2(playerPos.position.x, animator.transform.position.y), speed * Time.deltaTime);

>>>>>>> parent of dce448a (Merge branch 'hugo' into main):Assets/ChaseBehavior.cs
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
