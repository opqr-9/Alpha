using UnityEngine;

public class UnitIdleState : StateMachineBehaviour
{
    AttackController attackController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       attackController = animator.GetComponent<AttackController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(attackController.attackTarget != null)
        {
            animator.SetBool("isFollowing", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
