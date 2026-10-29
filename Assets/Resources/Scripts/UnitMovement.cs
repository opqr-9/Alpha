using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera camera;
    NavMeshAgent agent;
    public LayerMask groundLayer;
    Animator animator;

    private void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                animator.SetBool("isMoving",true);
                agent.SetDestination(hit.point);
            }
        }

        if(agent.hasPath==false||agent.remainingDistance<=agent.stoppingDistance)
        {
            animator.SetBool("isMoving",false);
        }
        else
        {
            animator.SetBool("isMoving",true);
        }
    }
}
