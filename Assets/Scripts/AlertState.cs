using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnenmyState {
    private readonly StatePattern enemy;
    private float searchTimer;

    public AlertState(StatePattern statePattern)
    {
        this.enemy = statePattern;
    }

    public void OnTrigger(Collider other)
    {
        
        
    }

    public void ToAlertState()
    {
        //ei
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTimer = 0f;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTimer = 0f;
        
    }

    public void UpdateState()
    {
        enemy.indicator.material.color = Color.yellow;
        Look();
        Search();


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

    private void Search()
    {
        enemy.navMeshAgent.isStopped = true;

        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;

        if(searchTimer > enemy.searchDur)
        {
            ToPatrolState();
        }
    }

   
}
