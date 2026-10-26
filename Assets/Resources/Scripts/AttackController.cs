using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Transform attackTarget;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            attackTarget = other.transform;
        }
    }
    private void OTriggerStay(Collider other)
    {
        if(attackTarget==null&&other.CompareTag("Enemy"))
        {
            attackTarget=other.transform;
        }
    }
}
