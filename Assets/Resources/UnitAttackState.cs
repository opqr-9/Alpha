using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;

    public float stopAttackDistance=1.2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.GetComponent<AttackController>();
        agent = animator.GetComponent<NavMeshAgent>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackController.attackTarget!=null&&Vector3.Distance(animator.transform.position,attackController.attackTarget.transform.position)<stopAttackDistance)
        {
            animator.transform.LookAt(attackController.attackTarget.position);
        }
        else
        {
            animator.SetBool("isAttacking",false);
        }
    }
}
