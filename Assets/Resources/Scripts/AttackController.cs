using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform attackTarget;
    
    public int damage=1;
    public float attackSpeed=0.75f;

    private void OnTriggerEnter(Collider other)
    {
        if(attackTarget==null&&!other.CompareTag(tag))
        {
            attackTarget = other.transform;
        }
    }
    private void OTriggerStay(Collider other)
    {
        if(attackTarget==null&&!other.CompareTag(tag))
        {
            attackTarget=other.transform;
        }
    }
}
