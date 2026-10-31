using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public float health;
    public float maxHealth=100;
    public HealthTracker healthTracker;
    
    NavMeshAgent agent;
    Animator animator;

    private void Start()
    {
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);
        health=maxHealth;
        UpdateHealthUI();
        agent=GetComponent<NavMeshAgent>();
        animator=GetComponent<Animator>();
    }

    void Update()
    {
        if(!agent.hasPath||agent.remainingDistance<=agent.stoppingDistance)
        {
            animator.SetBool("isMoving",false);
        }
        else
        {
            animator.SetBool("isMoving",true);
        }
    }

    private void OnDestroy()
    {
        UnitSelectionManager.instance.allUnitsList.Remove(gameObject);
    }

    private void UpdateHealthUI()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }
        healthTracker.UpdateSliderValue(health,maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health-=damage;
        UpdateHealthUI();
    }
}
