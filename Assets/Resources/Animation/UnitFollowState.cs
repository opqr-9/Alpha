using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;

    public float attackDistance = 2f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.GetComponent<AttackController>();
        agent = animator.GetComponent<NavMeshAgent>();
        // animator.SetBool("isMoving",true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackController.attackTarget == null)
        {
            animator.SetBool("isFollowing", false);
            agent.ResetPath();
            return;
        }

        agent.SetDestination(attackController.attackTarget.position);
        animator.transform.LookAt(attackController.attackTarget.position);

        if(Vector3.Distance(animator.transform.position, attackController.attackTarget.position) <= attackDistance)
        {
            animator.SetBool("isAttacking", true);
            agent.ResetPath();
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("isMoving",false);
    }
}
