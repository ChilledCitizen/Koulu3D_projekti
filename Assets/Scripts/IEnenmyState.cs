using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnenmyState {

    void UpdateState();

    void OnTrigger(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

	
}
