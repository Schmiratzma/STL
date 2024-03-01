using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    public UnitData unitData;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        UnitControler.Instance.onUnitsCommanded += SetMovementTarget;
    }

    public void SetMovementTarget(Vector3 targetPosition)
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        navMeshAgent.SetDestination(targetPosition);
    }

    private void OnDestroy()
    {
        UnitControler.Instance.onUnitsCommanded -= SetMovementTarget;
    }
}
