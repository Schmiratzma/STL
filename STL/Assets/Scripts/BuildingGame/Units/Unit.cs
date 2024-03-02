using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerClickHandler
{
    NavMeshAgent navMeshAgent;

    public UnitData unitData;

    bool selected = false;

    [SerializeField] Material selectedMaterial;
    [SerializeField] Material unSelectedMaterial;

    [SerializeField] MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = !selected;

        if (selected)
        {
            UnitControler.Instance.onUnitsCommanded += SetMovementTarget;
            meshRenderer.material = selectedMaterial;
        }
        else
        {
            UnitControler.Instance.onUnitsCommanded -= SetMovementTarget;
            meshRenderer.material = unSelectedMaterial;
        }
    }
}
