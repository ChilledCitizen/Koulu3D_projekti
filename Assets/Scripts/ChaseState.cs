using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnenmyState{
    private StatePattern enemy;

    public ChaseState(StatePattern statePattern)
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
        
    }

    public void ToCheckState()
    {
        enemy.lastPosition = enemy.chaseTarget.position;
        enemy.currentState = enemy.checkState;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        enemy.indicator.material.color = Color.red;
        Look();
        Chase();
    }

    private void Look()
    {
        RaycastHit hit;
        Vector3 enemyToTarget = (enemy.chaseTarget.position - enemy.eyes.transform.position);

        if (Physics.Raycast(enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sideRange) && hit.collider.CompareTag("Player"))
        {

            enemy.chaseTarget = hit.transform;
            

        }
        else
        {
            
            ToCheckState();
        }

        Debug.DrawRay(enemy.eyes.transform.position, enemy.eyes.transform.forward * enemy.sideRange, Color.green);


    }

    private void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
    }


}
