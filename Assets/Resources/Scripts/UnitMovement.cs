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
    DirectionIndicator directionIndicator;
    LineRenderer lineRenderer;
    private void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
        directionIndicator=GetComponent<DirectionIndicator>();
        lineRenderer=GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(!agent.hasPath||agent.remainingDistance<=agent.stoppingDistance)
        {
            directionIndicator.ChangeIndicatorVisibility(false);
        }
        else
        {
            directionIndicator.ChangeIndicatorVisibility(true);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                agent.SetDestination(hit.point);
                directionIndicator.DrawLine(hit);
            }
        }
    }

    void OnDisable()
    {
        directionIndicator.ChangeIndicatorVisibility(false);
    }
}
