using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera camera;
    NavMeshAgent agent;
    public LayerMask groundLayer;

    private void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
