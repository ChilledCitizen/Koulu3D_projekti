using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolState : IEnenmyState
{

    private StatePattern enemy;
    private int nextWaypoint;

    public PatrolState(StatePattern statePattern)
    {
        this.enemy = statePattern;
    }

    public void OnTrigger(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        
    }

    public void ToPatrolState()
    {

    }

    public void UpdateState()
    {
        enemy.indicator.material.color = Color.green;
        Patrol();
        Look();
    }

    private void Look()
    {
        RaycastHit hit;

        if(Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sideRange) && hit.collider.CompareTag("Player"))
        {

            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }

        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.transform.forward * enemy.sideRange, Color.green);


    }

    private void Patrol()
    {
        enemy.navMeshAgent.destination = enemy.wayPoints[nextWaypoint].position;
        enemy.navMeshAgent.isStopped = false;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            nextWaypoint = (nextWaypoint + 1) % enemy.wayPoints.Length;
        }

    }
}


