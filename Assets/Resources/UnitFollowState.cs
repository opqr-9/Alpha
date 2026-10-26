using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;

    public float attackDistance = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.GetComponent<AttackController>();
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackController.attackTarget == null)
        {
            animator.SetBool("isFollowing", false);
            return;
        }

        agent.SetDestination(attackController.attackTarget.position);
        animator.transform.LookAt(attackController.attackTarget.position);

        // float distance = Vector3.Distance(animator.transform.position, attackController.attackTarget.position);
        // if(distance <= attackDistance)
        // {
        //     animator.SetBool("IsAttacking", true);
        // }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //agent.SetDestination(animator.transform.position);
    }
}
