using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckState : IEnenmyState {
    private StatePattern enemy;

    public CheckState(StatePattern statePattern)
    {
        this.enemy = statePattern;
    }

    public void OnTrigger(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        enemy.indicator.material.color = Color.blue;
        Look();
        Check();



    }

    private void Look()
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sideRange) && hit.collider.CompareTag("Player"))
        {

            enemy.chaseTarget = hit.transform;
            ToChaseState();

        }
        

        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.transform.forward * enemy.sideRange, Color.green);


    }

    private void Check()
    {

        enemy.navMeshAgent.destination = enemy.lastPosition;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            ToAlertState();
        }

       


    }

    public void ToCheckState()
    {
        //ei
    }
}

