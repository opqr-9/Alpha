using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float health;
    public float maxHealth=100;
    public HealthTracker healthTracker;
    private void Start()
    {
        UnitSelectionManager.instance.allUnitsList.Add(gameObject);
        health=maxHealth;
        UpdateHealthUI();
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
