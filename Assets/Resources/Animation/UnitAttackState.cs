using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : StateMachineBehaviour
{
    NavMeshAgent agent;
    AttackController attackController;

    public float stopAttackDistance=1.5f;
    public float damage;
    public float attackSpeed;
    public float nextAttackTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController = animator.GetComponent<AttackController>();
        agent = animator.GetComponent<NavMeshAgent>();
        damage=attackController.damage;
        nextAttackTime=attackSpeed=attackController.attackSpeed;
        attackController.muzzleEffect.gameObject.SetActive(true); 
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackController.attackTarget!=null&&Vector3.Distance(animator.transform.position,attackController.attackTarget.transform.position)<stopAttackDistance)
        {
            animator.transform.LookAt(attackController.attackTarget.position);
            Attack();
        }
        else
        {
            animator.SetBool("isAttacking",false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackController.muzzleEffect.gameObject.SetActive(false);
    }

    private void Attack()
    {
        nextAttackTime-=Time.deltaTime;
        if(nextAttackTime<=0)
        {
            attackController.attackTarget.GetComponent<Unit>().TakeDamage(damage);
            SoundManager.instance.PlayInfantryAttackSound();
            nextAttackTime+=attackSpeed;
        }
    }

}
